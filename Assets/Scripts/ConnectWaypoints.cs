using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectWaypoints : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Waypoint") != null)
        {
            Debug.Log("it exists");
        }
        if (GameObject.Find("Waypoint2") != null)
        {
            Debug.Log("Waypoint 2 exists");
        }

        //string[] x = "Variable1/Variable2".split('/');
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
