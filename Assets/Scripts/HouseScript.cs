using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Debug.Log("trigger loss condition");
            }
        }
    }

    // house sprite transition happens here
    private void HouseDamage() 
    {
        currentHouseSprite = houseSpriteList[HouseHP];
        gameObject.GetComponent<SpriteRenderer>().sprite = currentHouseSprite;
    }
}
