using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowStationScript : MonoBehaviour
{
    private float fireRate = .2f;
    public bool isUpgraded = false;

    private float timer = 3;
    public GameObject slowBomb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1/fireRate){
            slowAttack();
            timer = 0;
        }
    }

    void slowAttack(){
        Instantiate(slowBomb, new Vector3(transform.position.x, transform.position.y, transform.position.z+.5f), Quaternion.identity);
    }

    // // when the player mouses over the turret they should get to see the area of effect
    // private void OnMouseEnter() {
    //     gameObject.transform.Find("TargetingRange").gameObject.GetComponent<SpriteRenderer>().enabled = true;
    // }

    // private void OnMouseExit() {
    //     gameObject.transform.Find("TargetingRange").gameObject.GetComponent<SpriteRenderer>().enabled = false;
    // }
}
