using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    // Waypoints properties
    public GameObject Waypoints;
    private int waypointsListSize = 0;
    private int waypointIndex = 0; // to keep track of which waypoint the enemy is on
    private int finalwaypointIndex;
    private List<Transform> childWayPoints = new List<Transform>();
    // 

    // enemy properties
    public float progression = 0f;
    public float enemySpeed;
    public int enemyHealth;
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
            Debug.Log(child.gameObject.name); //debug list out names
            waypointsListSize++; //increment waypoint list size
            if (child != null)
            {
                childWayPoints.Add(child);
            }
        }

        //set starting position then increment to the next waypoint for the ProgressEnemy function
        transform.position = childWayPoints[waypointIndex].transform.position;
        waypointIndex++;

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

        //physically move the enemy along the path
        ProgressEnemy();
    }

    public void takeDamage(int damagetoTake)
    {
        enemyHealth -= damagetoTake;

        //check for death. Fade if health reaches 0
        StartCoroutine(FadeTo(0, 5)); //fades in five seconds
    }

    private void EnemyExit()
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
        if (newColor.a > 0 && desiredAlpha == 0)
        {
            newColor.a = 0;
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
        }
    }

    public IEnumerator slowDown(){
        if(!slowed){
            enemySpeed = enemySpeed * .6f;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(5);
        enemySpeed = enemySpeed / .6f;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
