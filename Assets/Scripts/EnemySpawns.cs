using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DefeatedWaypoint;
    public GameObject WaypointsParent;
    public float totalRoundTime;
    private float SpawnTimer = 0f;
    public float elapsedTime = 0f;

    // small enemy spawner details
    public GameObject SmallChild;
    public float smallTimeInterval;
    private float x = 1;

    // teen enemy spawner details
    public GameObject Teenager;
    public float teenTimeInterval;
    private float y = 1;

    // pillowsack enemy spawner details
    public GameObject PillowsackKid;
    public float pillowsackTimeInterval;
    private float z = 1;

    void Start()
    {
        SmallChild.gameObject.GetComponent<EnemyScript>().Waypoints = WaypointsParent;
        Teenager.gameObject.GetComponent<EnemyScript>().Waypoints = WaypointsParent;
        PillowsackKid.gameObject.GetComponent<EnemyScript>().Waypoints = WaypointsParent;
        SmallChild.gameObject.GetComponent<EnemyScript>().DefeatedWaypoint = DefeatedWaypoint;
        Teenager.gameObject.GetComponent<EnemyScript>().DefeatedWaypoint = DefeatedWaypoint;
        PillowsackKid.gameObject.GetComponent<EnemyScript>().DefeatedWaypoint = DefeatedWaypoint;

        // this will make all the waypoints on the path invisisble
        foreach (Transform child in WaypointsParent.transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnTimer += Time.deltaTime; // keep adding time per frame to timer variable 
    }

    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime; // for tracking
        SpawnTimer += Time.deltaTime; // keep adding time per frame to timer variable 
        SpawnTime();
    }

    private void SpawnTime() //example time interval is 2 seconds and total round time is 50 sec
    {
        if (SpawnTimer <= totalRoundTime + 1) //check to assure that the round time has not elapsed with an extra second to account for last second spawns
        {
            // spawn check for small kids
            if (SpawnTimer >= smallTimeInterval * x)
            {
                GameObject smallChild = Instantiate(SmallChild, WaypointsParent.transform.GetChild(0).transform.position, Quaternion.identity);
                x++; // this variable keeps track of intervals passed
            }
            // spawn check for teenagers
            if (SpawnTimer >= teenTimeInterval * y)
            {
                GameObject teenager = Instantiate(Teenager, WaypointsParent.transform.GetChild(0).transform.position, Quaternion.identity);
                y++; // this variable keeps track of intervals passed
            }
            // spawn check for pillowsack kids
            if (SpawnTimer >= pillowsackTimeInterval * z)
            {
                GameObject pillowsackKid = Instantiate(PillowsackKid, WaypointsParent.transform.GetChild(0).transform.position, Quaternion.identity);
                z++; // this variable keeps track of intervals passed
            }
        }
    }


}
