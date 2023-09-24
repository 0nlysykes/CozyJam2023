using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareStationScript : MonoBehaviour
{
    private float fireRate = .1f;
    public bool isUpgraded = false;

    private float timer = 5;
    public GameObject scareBomb;

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

    // when the player mouses over the turret they should get to see the area of effect
    private void OnMouseEnter() {
        gameObject.transform.Find("TargetingRange").gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnMouseExit() {
        gameObject.transform.Find("TargetingRange").gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
