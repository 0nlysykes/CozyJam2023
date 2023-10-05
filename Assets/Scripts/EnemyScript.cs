using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour
{

    //general properties
    public Rigidbody2D rb; //grab rigid body
    public Animator animator; //grab animator
    [SerializeField] public AudioSource enemyAudio;
    [SerializeField] public AudioClip satisfied;
    [SerializeField] public AudioClip scared;

    // Waypoints properties
    public GameObject Waypoints;
    public GameObject DefeatedWaypoint;
    private int waypointsListSize = 0;
    private int waypointIndex = 1; // to keep track of which waypoint the enemy is on
    private int finalwaypointIndex;
    private List<Transform> childWayPoints = new List<Transform>();
    // 

    // enemy properties
    private int maxHealth;
    public float progression = 0f;
    public float enemySpeed;
    public int enemyHealth;
    public int pointValue;
    private bool slowed = false;
    private bool defeated = false;
    bool sad;

    bool IsSlowed;
    //

    //velocity/direction calculation components (used in sprite determination)
    Vector2 movement;
    private float startingPositionX = 0;
    private float startingPositionY = 0;
    private float currentPositionX = 0;
    private float currentPositionY = 0;
    private float velocityX;
    private float velocityY;

    //private Transform[] childWaypoints;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = enemyHealth; // grab health at start for tracking

        //StartCoroutine(FadeTo(0, 5));
        sad = animator.GetBool("Sad");
        IsSlowed = animator.GetBool("IsSlowed");
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
        determineVelocity();

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

    private void determineVelocity()
    {
        //get current x and y position
        currentPositionX = gameObject.GetComponent<Rigidbody2D>().position.x;
        currentPositionY = gameObject.GetComponent<Rigidbody2D>().position.y;
        //Debug.Log(" position: " + currentPositionX);

        //measure velocity for x and y
        velocityX = (currentPositionX - startingPositionX) / Time.deltaTime;
        startingPositionX = currentPositionX; // set new starting position
        velocityY = (currentPositionY - startingPositionY) / Time.deltaTime;
        startingPositionY = currentPositionY; // set new starting position


        movement = new Vector2(velocityX, velocityY);
        animator.SetFloat("Horizontal", velocityX);
        animator.SetFloat("Vertical", velocityY);
        animator.SetFloat("IsMoving", movement.magnitude);
    }

    public void takeDamage(int damagetoTake)
    {
        enemyHealth -= damagetoTake;
        if (damagetoTake > 99)
        {
            animator.SetBool("Sad", true);
            enemySpeed = enemySpeed * 2;
            enemyAudio.PlayOneShot(scared, 0.5f);
        }

        bool MidState = animator.GetBool("MidState");
        if (enemyHealth == maxHealth / 2)
            animator.SetBool("MidState", !MidState);

        //check for death. Fade if health reaches 0
        if (enemyHealth <= 0 && !defeated)
        {

            enemyAudio.PlayOneShot(satisfied, 0.5f);
            defeated = true;
            progression = 0;
            GameObject.Find("PlayerCurrency").GetComponent<MoneyScript>().changeValue(pointValue);
            GameObject.Find("EventSystem").gameObject.GetComponent<EnemySpawns>().enemyCount--;
            StartCoroutine(FadeTo(0, 1)); //fades in five seconds
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

    public void slowDown(float slowValue, float slowDuration){
        StartCoroutine(SlowCoroutine(slowValue, slowDuration)); 
    }

    IEnumerator SlowCoroutine(float slowValue, float slowDuration)
    {
        
        if (!slowed)
        {
            animator.SetBool("Sad", !sad);
            animator.SetBool("IsSlowed", !IsSlowed);
            enemySpeed = enemySpeed * slowValue;
            slowed = true;
        }
        yield return new WaitForSeconds(slowDuration);
        if(slowed)
        {
            animator.SetBool("Sad", sad);
            animator.SetBool("IsSlowed", IsSlowed);
            enemySpeed = enemySpeed / slowValue;
            slowed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
