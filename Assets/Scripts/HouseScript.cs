using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseScript : MonoBehaviour
{
    public int HouseHP;
    Sprite currentHouseSprite; // keeps track of current sprite
    public List<Sprite> houseSpriteList = new List<Sprite>(); //element 0 is most damaged... max size entry is the starting house
    [SerializeField] public AudioSource booAudio; 
    [SerializeField] public AudioClip boo;

    // Start is called before the first frame update
    void Start()
    {
        currentHouseSprite = gameObject.GetComponentInParent<SpriteRenderer>().sprite;
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
        gameObject.GetComponentInParent<SpriteRenderer>().sprite = currentHouseSprite;
        booAudio.PlayOneShot(boo, 0.5f);
    }

    IEnumerator LossState()
    {
        GameObject.Find("StationUICanvas").GetComponent<NewStationFromClick>().CancelSpawn();
        GameObject.Find("StationUICanvas").GetComponent<UpgradeStationScript>().cancelUpgrade();
        GameObject.Find("StationUICanvas").SetActive(false);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LossScene");
    }
}
