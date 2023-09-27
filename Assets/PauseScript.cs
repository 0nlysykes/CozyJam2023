using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PauseScript : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] private float savedTimeScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        savedTimeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !isPaused)
        {
            savedTimeScale= Time.timeScale;
            //transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("BannerText").GetComponent<TMP_Text>().enabled = false;
            GameObject.Find("StationUICanvas").transform.GetChild(0).gameObject.SetActive(false);
            // foreach (Transform child in GameObject.Find("StationButtons").transform){
            //     child.gameObject.SetActive(false);
            // }
            //AudioListener.pause = true;
            Time.timeScale = 0; 
            isPaused = true;

        } 
        else if (Input.GetButtonDown("Pause") && isPaused)
        {
            Unpause();
        }
        // Set children to be visible/invisible
        foreach (Transform child in transform)
            child.gameObject.SetActive(isPaused);
    }

    public void Unpause()
    {
        GameObject.Find("BannerText").GetComponent<TMP_Text>().enabled = true;
        GameObject.Find("StationUICanvas").transform.GetChild(0).gameObject.SetActive(true);
        //AudioListener.pause = false;
        Time.timeScale = savedTimeScale;
        isPaused = false;
    }
    
}
