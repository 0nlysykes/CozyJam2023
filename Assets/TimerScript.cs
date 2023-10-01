using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerScript : MonoBehaviour
{
    //public static float timer = 0;
    public GameObject textToChange;
    public GameObject roundCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textToChange.GetComponent<TMPro.TextMeshProUGUI>().text = (7 - roundCounter.GetComponent<EnemySpawns>().roundNumber).ToString() +" hours until midnight";
    }
}
