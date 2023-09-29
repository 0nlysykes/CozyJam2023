using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareStationScript : MonoBehaviour
{
    // Variables that can be altered by upgrading the station
    public float fireRate = .1f;
    public GameObject scareBomb;
    // End of Variables that can be altered by upgrading the station

    // Variables that determine HOW MUCH BETTER the station gets by upgrading, base values + these values (USE THIS FOR BALANCING SCRIPT)
    public float upgradeFireRate = .1f;
    public GameObject upgradedScareBomb;
    //public Vector3 upgradeArea = Vector3.one;
    // End of section

    private float timer = 5;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1/fireRate){
            scareAttack();
            timer = 0;
        }
    }

    void scareAttack(){
        Instantiate(scareBomb, new Vector3(transform.position.x, transform.position.y, transform.position.z+.5f), Quaternion.identity);
    }

    //Scare station upgrade increases the fire rate, and size of the scare bomb
    public void upgrade(){
        //Faster fire rate
        fireRate += upgradeFireRate;
        //More slow
        //bigger slowing area NOT IMPLEMENTED BECAUSE SLOW IS NOW FREEZE
        //transform.GetChild(0).localScale += upgradeArea;
        scareBomb = upgradedScareBomb;

        //Change look of station
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

        //Station is now upgraded
        gameObject.GetComponent<StationUniversalProperties>().isUpgraded = true;
    }

    // // when the player mouses over the turret they should get to see the area of effect
    // private void OnMouseEnter() {
    //     gameObject.transform.Find("TargetingRange").gameObject.GetComponent<SpriteRenderer>().enabled = true;
    // }

    // private void OnMouseExit() {
    //     gameObject.transform.Find("TargetingRange").gameObject.GetComponent<SpriteRenderer>().enabled = false;
    // }
}
