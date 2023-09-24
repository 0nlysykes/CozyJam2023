using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    // Start is called before the first frame update
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
        if(x < totalRoundTime + smallTimeInterval) //check to assure that the round time has not elapsed
        {
            // spawn check for small kids
            if (SpawnTimer >= smallTimeInterval * x)
            {
                GameObject smallChild = Instantiate(SmallChild, new Vector2(0, 0), Quaternion.identity);
                x++; // this variable keeps track of intervals passed
            }
            // spawn check for teenagers
            if (SpawnTimer >= teenTimeInterval * y)
            {
                GameObject teenager = Instantiate(Teenager, new Vector2(0, 0), Quaternion.identity);
                x++; // this variable keeps track of intervals passed
            }
            // spawn check for pillowsack kids
            if (SpawnTimer >= pillowsackTimeInterval * z)
            {
                GameObject pillowsackKid = Instantiate(PillowsackKid, new Vector2(0, 0), Quaternion.identity);
                x++; // this variable keeps track of intervals passed
            }
        }
    }
}
