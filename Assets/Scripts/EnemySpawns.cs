using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DefeatedWaypoint;
    public GameObject WaypointsParent;
    public GameObject bannerCanvas;
    public float totalRoundTime;
    private float SpawnTimer = 0f;
    public int roundNumber = 0;

    // small enemy spawner details
    public GameObject SmallChild;
    public float[] smallChildSpawnRates = {2,1.8f,1.6f,1.4f,1.2f,1f};//new float[6];
    public float smallTimeInterval;
    private float x = 1;

    // teen enemy spawner details
    public GameObject Teenager;
    public float[] teenagerSpawnRates = {6, 5.6f, 5.2f, 4.8f, 4.4f, 4f};// new float[6];
    public float teenTimeInterval;
    private float y = 1;

    // pillowsack enemy spawner details
    public GameObject PillowsackKid;
    public float[] pillowsackSpawnRates = {10, 9f, 8f, 7f, 6f, 5f};//new float[6];
    public float pillowsackTimeInterval;
    private float z = 1;
    public int enemyCount = 0;

    void Start()
    {
        SmallChild.gameObject.GetComponent<EnemyScript>().Waypoints = WaypointsParent;
        Teenager.gameObject.GetComponent<EnemyScript>().Waypoints = WaypointsParent;
        PillowsackKid.gameObject.GetComponent<EnemyScript>().Waypoints = WaypointsParent;
        SmallChild.gameObject.GetComponent<EnemyScript>().DefeatedWaypoint = DefeatedWaypoint;
        Teenager.gameObject.GetComponent<EnemyScript>().DefeatedWaypoint = DefeatedWaypoint;
        PillowsackKid.gameObject.GetComponent<EnemyScript>().DefeatedWaypoint = DefeatedWaypoint;

        // this will make all the waypoints on the path invisible
        foreach (Transform child in WaypointsParent.transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        nextRound();
    }

    private void FixedUpdate()
    {
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
                GameObject smallChild = Instantiate(SmallChild, WaypointsParent.transform.GetChild(0).transform.position - new Vector3(0, 0, 2), Quaternion.identity);
                x++; // this variable keeps track of intervals passed
                enemyCount++;
            }
            // spawn check for teenagers
            if (SpawnTimer >= teenTimeInterval * y)
            {
                GameObject teenager = Instantiate(Teenager, WaypointsParent.transform.GetChild(0).transform.position - new Vector3(0, 0, 2), Quaternion.identity);
                y++; // this variable keeps track of intervals passed
                enemyCount++; //UNCOMMENT THIS YOU FOOLS DFDS FJSDBV ILSFBVFSVB SPVHYSDBP VSCBDVP SDVBSDPIVBSDPVBSDPIUVBSDPUVBSDFIVBSDOIVBSCIVBSCDFHUVBSDVIUBDSVUHBSDPVUIBSCFVPUSIBCVPISDUBVSDPIVUBSDPIVUBSDPVUBHSDPVUBSDVPUSDBVSDPVBSD
            }
            // spawn check for pillowsack kids
            if (SpawnTimer >= pillowsackTimeInterval * z)
            {
                GameObject pillowsackKid = Instantiate(PillowsackKid, WaypointsParent.transform.GetChild(0).transform.position - new Vector3(0, 0, 2), Quaternion.identity);
                z++; // this variable keeps track of intervals passed
                enemyCount++; //UNCOMMENT THIS YOU FOOLS DFDS FJSDBV ILSFBVFSVB SPVHYSDBP VSCBDVP SDVBSDPIVBSDPVBSDPIUVBSDPUVBSDFIVBSDOIVBSCIVBSCDFHUVBSDVIUBDSVUHBSDPVUIBSCFVPUSIBCVPISDUBVSDPIVUBSDPIVUBSDPVUBHSDPVUBSDVPUSDBVSDPVBSD
            }
        } else {
            //Time has exceeded the round timer, so the round ends
            nextRound();
        }


    }

    private void nextRound(){
        roundNumber++;
        if(roundNumber > 6){
            if(enemyCount == 0){
                StartCoroutine(bannerCanvas.GetComponent<BannerScript>().Victory());
                enemyCount--; //this makes it so the coroutine does not run again
            }
        } else {
            SpawnTimer = 0;
            x = 1; 
            y = 1; 
            z = 1;
            smallTimeInterval = smallChildSpawnRates[roundNumber - 1];
            teenTimeInterval = teenagerSpawnRates[roundNumber - 1];
            pillowsackTimeInterval = pillowsackSpawnRates[roundNumber - 1];
            StartCoroutine(bannerCanvas.GetComponent<BannerScript>().RoundEnd(roundNumber));
        }
    }

    
}
