using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyStationScript : MonoBehaviour
{
    public int ammo = 50;
    private float fireRate = 1;

    // damage is however much satisfaction a candy bar is worth
    private int damage = 1;
    public bool isUpgraded = false;

    private float timer = 1;
    private bool targetsLocated = false;
    private Transform currentTarget = null;

    private List<Transform> enemies = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        
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
            // deliver delicious candy to targeted child
            candyAttack();
        }
    }


    void candyAttack(){
        // when station gives candy:
        //  -ammo decrements by one
        //  -damage is dealt to currentTarget
        //  -timer is reset
        ammo -= 1;
        currentTarget.gameObject.GetComponent<EnemyScript>().takeDamage(damage);
        timer = 0;
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
        } else {
            targetsLocated = false;
        }
    }

    // Add enemy to list of enemies in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){     
            enemies.Add(other.transform);
            targetsLocated = true;
        }
    }

    // Remove enemy from list of enemies in range
    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform == currentTarget){
            currentTarget = null;
        }

        enemies.Remove(other.transform);
    }
}
