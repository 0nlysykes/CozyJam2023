using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseScript : MonoBehaviour
{
    public int HouseHP;
    Sprite currentHouseSprite; // keeps track of current sprite
    public List<Sprite> houseSpriteList = new List<Sprite>(); //element 0 is most damaged... max size entry is the starting house

    // Start is called before the first frame update
    void Start()
    {
        currentHouseSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    // collision based on tag since house can still collide with stations
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            HouseHP--; // house takes damage
            if (HouseHP > 0) // check if house has HP remaining
            {
                HouseDamage();
            }
            else if (HouseHP == 0) // final damage sprite transition and trigger loss condition
            {
                HouseDamage();
                // Trigger loss Condition
                StartCoroutine(LossState());
            }
            other.gameObject.GetComponent<EnemyScript>().takeDamage(50);
        }
    }

    // house sprite transition happens here
    private void HouseDamage() 
    {
        currentHouseSprite = houseSpriteList[HouseHP];
        gameObject.GetComponent<SpriteRenderer>().sprite = currentHouseSprite;
    }

    IEnumerator LossState()
    {
        GameObject.Find("StationUICanvas").SetActive(false);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);
    }
}