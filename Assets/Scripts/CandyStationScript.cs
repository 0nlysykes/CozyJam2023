using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyStationScript : MonoBehaviour
{

    private GameObject ammoPopup;
    
    // Variables that can be altered by upgrading the station
    public float fireRate = 1;
    public int ammo = 30;
    public int maxAmmo = 30; 
    public int damage = 1;
    // Also targeting area but there isn't a variable for it, just resize it in editor
    // End of Variables that can be altered by upgrading the station

    // Variables that determine HOW MUCH BETTER the station gets by upgrading, base values + these values (USE THIS FOR BALANCING SCRIPT)
    public float upgradeFireRate = 1;
    public int upgradeMaxAmmo = 30;
    public int upgradeDamage = 1;
    public Vector3 upgradeArea = Vector3.one;
    // End of section

    private bool ammoShown = false;

    private float timer = 1;
    private bool targetsLocated = false;
    private Transform currentTarget = null;

    private List<Transform> enemies = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
        ammoPopup = GameObject.Find("AmmoPopup");
    }

    // Update is called once per frame
    // If enemylist is empty then currentTarget is null and the turret doesn't attack
    // If enemyList is not empty then it sets currentTarget based on which enemy has highest progression value
    // If currentTarget is null and the enemyList is not empty then that means every enemy is null and the list is refreshed.
    // If currentTarget is not null then turret attacks it
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 1/fireRate && targetsLocated && ammo > 0){
            // take aim at child that is furthest forward
            setTarget();
        }
        if(ammoShown){
            ammoPopup.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = ammo + "/" + maxAmmo;
        }
    }


    void candyAttack(){
        // when station gives candy:
        //  -ammo decrements by one
        //  -damage is dealt to currentTarget
        //  -timer is reset

        // how to show player it is doing that
        //  -machine does animation of giving candy
        //  -child does animation of getting candy
        //  -sound plays?
        //  -if child is satisfied short particle effect, sound effect, and child walks off the path and fades out
        //Debug.Log(enemies.Count);
        if(currentTarget.gameObject.GetComponent<EnemyScript>().enemyHealth > 0){
            ammo -= 1;
            StartCoroutine(activationAnimation());
            currentTarget.gameObject.GetComponent<EnemyScript>().takeDamage(damage);
            timer = 0;
        }
    }

    void setTarget(){
        // this variable tracks which potential target is furthest along the track
        float highestProgression = 0;

        // If the current target is null then clear all null enemies from the list
        if(currentTarget == null){
            // Remove all null values from target list as they have left the aoe or have been destroyed
            enemies.RemoveAll(eachTarget => {return eachTarget == null;});
        } else {
            highestProgression = currentTarget.gameObject.GetComponent<EnemyScript>().progression;
        }

        // Find new enemy to target
        if(enemies.Count > 0){
            foreach (Transform target in enemies){
                // it is possible that enemies that are not the current target could have been destroyed from other sources, so we check for that
                if(target == null){
                    enemies.RemoveAll(eachTarget => {return eachTarget == null;});
                } else {
                    if(target.gameObject.GetComponent<EnemyScript>().progression > highestProgression){
                        highestProgression = target.gameObject.GetComponent<EnemyScript>().progression;
                        currentTarget = target;
                    }
                }
            }
            // attack target with the candy ... I mean deliver delicious candy to targeted child
            candyAttack();
        } else {
            targetsLocated = false;
        }
    }

    public void targetingAreaCollisionEnter(Collider2D other){
        if(other.gameObject.tag == "Enemy"){     
            if(other.gameObject.GetComponent<EnemyScript>().enemyHealth > 0){
                enemies.Add(other.transform);
                targetsLocated = true;
            }
        }
    }

    public void targetingAreaCollisionExit(Collider2D other){
        if(other.transform == currentTarget){
            currentTarget = null;
        }

        enemies.Remove(other.transform);
    }

    // Candy station upgrade increases the ammo, fire rate, targeting range, and damage of the candy
    public void upgrade(){
        //Fill ammo, expand ammo
        maxAmmo += upgradeMaxAmmo;
        ammo = maxAmmo;
        //Faster fire rate
        fireRate += upgradeFireRate;
        //More damage
        damage += upgradeDamage;
        //bigger targeting area
        transform.GetChild(0).localScale += upgradeArea;

        //Change look of station
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.22f, 0.75f, 0.7f);
        
        //Station is now upgraded
        gameObject.GetComponent<StationUniversalProperties>().isUpgraded = true;
    }

    private void OnMouseEnter() {
        if(enabled){
            ammoShown = true;
            ammoPopup.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            ammoPopup.GetComponent<Image>().enabled = true;
            ammoPopup.transform.GetChild(0).gameObject.SetActive(true);
            ammoPopup.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = ammo + "/" + maxAmmo;
        }
    }

    private void OnMouseExit() {
        if(enabled){
            ammoPopup.GetComponent<Image>().enabled = false;
            ammoPopup.transform.GetChild(0).gameObject.SetActive(false);
            ammoShown = false;
        }
    }

    // this coroutine is TEMPORARY, and only serves to show that the candy machine is doing something when it fires.
    //  it will be replaced with actual animation later(tm)
    IEnumerator activationAnimation(){
        
		transform.Find("tempParticles").gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        transform.Find("tempParticles").gameObject.SetActive(false);
    }
}
