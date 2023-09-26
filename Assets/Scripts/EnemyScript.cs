using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour
{

    // Waypoints properties
    public GameObject Waypoints;
    public GameObject DefeatedWaypoint;
    private int waypointsListSize = 0;
    private int waypointIndex = 1; // to keep track of which waypoint the enemy is on
    private int finalwaypointIndex;
    private List<Transform> childWayPoints = new List<Transform>();
    // 

    // enemy properties
    public float progression = 0f;
    public float enemySpeed;
    public int enemyHealth;
    public int pointValue;
    private bool slowed = false;
    //

    //private Transform[] childWaypoints;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(FadeTo(0, 5));

        //get the size of waypoints list
        foreach (Transform child in Waypoints.transform)
        {
            //Debug.Log(child.gameObject.name); //debug list out names
            waypointsListSize++; //increment waypoint list size
            if (child != null)
            {
                childWayPoints.Add(child);
            }
        }

        //set starting position then increment to the next waypoint for the ProgressEnemy function
        //transform.position = childWayPoints[waypointIndex].transform.position;
        //waypointIndex++;

        //grab final waypoint index
        finalwaypointIndex = childWayPoints.Count - 1;

        //Move the enemy along waypoints

    }

    private void ProgressEnemy()
    {
        // check if the enemy has reached the final waypoint
        if (waypointIndex <= finalwaypointIndex)
        {
            //move the enemy in stages (by a factor of its speed times the amount of time elapsed for this current frame
            transform.position = Vector2.MoveTowards(transform.position, childWayPoints[waypointIndex].transform.position, enemySpeed * Time.deltaTime);

            //Check whether the enemy has reached the next waypoint
            if (transform.position == childWayPoints[waypointIndex].transform.position)
            {
                waypointIndex++;
            }

        }
        //draw a line between current waypoint and next waypoint
    }

    private void FixedUpdate()
    {
        //track the enemey progression along the path
        progression += Time.deltaTime * enemySpeed;

        if (enemyHealth > 0)
        {
            //physically move the enemy along the path
            ProgressEnemy();
        } else
        {
            EnemyExit();
        }
        
    }

    public void takeDamage(int damagetoTake)
    {
        enemyHealth -= damagetoTake;

        //check for death. Fade if health reaches 0
        if(enemyHealth <= 0)
        {
            progression = 0;
            GameObject.Find("PlayerCurrency").GetComponent<MoneyScript>().changeValue(pointValue);
            StartCoroutine(FadeTo(0, 5)); //fades in five seconds
        }
    }

    private void EnemyExit()
    {
        transform.position = Vector2.MoveTowards(transform.position, DefeatedWaypoint.transform.position, enemySpeed * Time.deltaTime);
    }

    IEnumerator FadeTo(float desiredAlpha, float desiredTime)
    {
        float alpha = gameObject.GetComponent<SpriteRenderer>().color.a;
        Color newColor = Color.white;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / desiredTime)
        {
            newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, desiredAlpha, t));
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
        Destroy(gameObject); // destroy the enemy as soon as it has faded completely
    }

    public void slowDown(){
        StartCoroutine(SlowCoroutine()); 
    }

    IEnumerator SlowCoroutine(){
        if(!slowed){
            enemySpeed = enemySpeed * .6f;
            slowed = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        yield return new WaitForSeconds(3);
        if(slowed){
            enemySpeed = enemySpeed / .6f;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            slowed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
