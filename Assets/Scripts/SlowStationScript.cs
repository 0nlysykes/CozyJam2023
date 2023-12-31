using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowStationScript : MonoBehaviour
{
    public Animator animator; //grab animator
    public RuntimeAnimatorController upgradedAnimator;
    public Sprite upgradedSprite;
    private float timer = 3;

    // Variables that can be altered by upgrading the station
    public float fireRate;
    public GameObject slowBomb;
    // End of Variables that can be altered by upgrading the station

    // Variables that determine HOW MUCH BETTER the station gets by upgrading, base values + these values (USE THIS FOR BALANCING SCRIPT)
    public float upgradeFireRate;
    public GameObject upgradedSlowBomb;
    //public Vector3 upgradeArea = Vector3.one;
    // End of section

    // Start is called before the first frame update
    void Start()
    {
        setScale();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1/fireRate){
            StartCoroutine(activationAnimation());
            timer = 0;
        }
    }

    void slowAttack(){
        Instantiate(slowBomb, new Vector3(transform.position.x, transform.position.y, transform.position.z+.1f), Quaternion.identity);
    }

    public void setScale(){
        Vector3 bombScale = new Vector3(slowBomb.GetComponent<SlowBombScript>().maxScale.x/ transform.localScale.x, slowBomb.GetComponent<SlowBombScript>().maxScale.y/ transform.localScale.y, 0);
        transform.GetChild(0).transform.localScale = bombScale;
    }
    // Slow station upgrade increase the fire rate and slow effect severity of the slow bomb
    public void upgrade(){
        //Faster fire rate
        fireRate += upgradeFireRate;
        //More slow
        //bigger slowing area NOT IMPLEMENTED BECAUSE SLOW IS NOW FREEZE
        //transform.GetChild(0).localScale += upgradeArea;
        slowBomb = upgradedSlowBomb;

        setScale();
        //Change look of station
        gameObject.GetComponent<SpriteRenderer>().sprite = upgradedSprite;
        animator.runtimeAnimatorController = upgradedAnimator;

        //Station is now upgraded
        gameObject.GetComponent<StationUniversalProperties>().isUpgraded = true;
    }

    IEnumerator activationAnimation(){
		slowAttack();
        animator.SetBool("active", true);
        yield return new WaitForSeconds(2);
        animator.SetBool("active", false);
    }
    // // when the player mouses over the turret they should get to see the area of effect
    // private void OnMouseEnter() {
    //     gameObject.transform.Find("TargetingRange").gameObject.GetComponent<SpriteRenderer>().enabled = true;
    // }

    // private void OnMouseExit() {
    //     gameObject.transform.Find("TargetingRange").gameObject.GetComponent<SpriteRenderer>().enabled = false;
    // }
}
