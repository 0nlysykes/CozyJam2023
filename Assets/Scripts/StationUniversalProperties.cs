using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class StationUniversalProperties : MonoBehaviour
{
    public int cost;
    public int costToUpgrade;
    public bool isUpgraded = false;
    public bool isEnabled = true;
    public bool isColliding = false;
    public Color originalColor;

    public GameObject upgradeObject;

    private int numberOfBlockers = 0;

    private void Start() {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        //transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //blockers.Add(other.transform);
        if(isEnabled){
            numberOfBlockers++;
            isColliding = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(isEnabled){
            numberOfBlockers--;
            if(numberOfBlockers == 0){
                isColliding = false;
                gameObject.GetComponent<SpriteRenderer>().color = originalColor;
            }
        }
    }

    // when the player mouses over the turret they should get to see the area of effect
    // NOTE since the station is a rigibody it adopts the collider of the targeting area, making it so
    //  this function runs even when the player mouses over the invisible targeting area
    private void OnMouseEnter() {
        if(upgradeObject.GetComponent<UpgradeStationScript>().upgradingStation && !isUpgraded){
            upgradeObject.GetComponent<UpgradeStationScript>().upgradeTarget = gameObject;
            upgradeObject.GetComponent<UpgradeStationScript>().PopUp("Cost to Upgrade: " + costToUpgrade);
        } else {
            gameObject.transform.Find("TargetingRange").gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnMouseExit() {
        if(upgradeObject.GetComponent<UpgradeStationScript>().upgradingStation){
            upgradeObject.GetComponent<UpgradeStationScript>().ClosePopUp();
            upgradeObject.GetComponent<UpgradeStationScript>().upgradeTarget = null;
        } else {
            gameObject.transform.Find("TargetingRange").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
