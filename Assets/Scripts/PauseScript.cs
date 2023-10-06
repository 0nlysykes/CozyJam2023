using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PauseScript : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject cancelPrompt;
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
            Pause();
        } 
        else if (Input.GetButtonDown("Pause") && isPaused)
        {
            Unpause();
        }
        
    }

    public void Pause(){
        savedTimeScale= Time.timeScale;
        //transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("BannerText").GetComponent<TMP_Text>().enabled = false;
        GameObject.Find("BannerSubtext").GetComponent<TMP_Text>().enabled = false;
        //GameObject.Find("StationUICanvas").transform.GetChild(0).gameObject.SetActive(false);
        foreach (Transform child in GameObject.Find("StationUICanvas").transform){
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in GameObject.Find("HUDCanvas").transform)
        {
            child.gameObject.SetActive(false);
        }
        //AudioListener.pause = true;
        Time.timeScale = 0; 
        isPaused = true;
        // Set children to be visible/invisible
        foreach (Transform child in transform)
            if(child.name != "PauseButton")
                child.gameObject.SetActive(true);
    }

    public void Unpause()
    {
        GameObject.Find("BannerText").GetComponent<TMP_Text>().enabled = true;
        GameObject.Find("BannerSubtext").GetComponent<TMP_Text>().enabled = true;
        //GameObject.Find("StationUICanvas").transform.GetChild(0).gameObject.SetActive(true);
        foreach (Transform child in GameObject.Find("StationUICanvas").transform){
            child.gameObject.SetActive(true);
        }
        foreach (Transform child in GameObject.Find("HUDCanvas").transform)
        {
            child.gameObject.SetActive(true);
        }
        cancelPrompt.SetActive(false);
        //AudioListener.pause = false;
        Time.timeScale = savedTimeScale;
        isPaused = false;
        // Set children to be visible/invisible
        foreach (Transform child in transform)
            if(child.name != "PauseButton")
                child.gameObject.SetActive(false);
    }

    public void retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void mainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonHandler(){
        if(isPaused){
            Unpause();
        } else {
            Pause();
        }
    }
    
}
