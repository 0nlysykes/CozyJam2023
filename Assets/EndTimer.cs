using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EndTimer : MonoBehaviour
{
    public GameObject EndTimeLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //EndTimeLabel.GetComponent<TMPro.TextMeshProUGUI>().text = TimeSpan.FromSeconds(TimerScript.timer).ToString(@"mm\:ss");
        //TimerScript.timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
