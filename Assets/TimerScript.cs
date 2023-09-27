using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerScript : MonoBehaviour
{
    public static float timer = 0;
    public GameObject textToChange;
    public GameObject roundCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(timer);
        textToChange.GetComponent<TMPro.TextMeshProUGUI>().text = (roundCounter.GetComponent<EnemySpawns>().roundNumber + 5).ToString() +":"+ time.ToString(@"ss") + " PM";
    }
}
