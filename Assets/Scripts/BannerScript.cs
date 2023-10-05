using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BannerScript : MonoBehaviour
{
    public GameObject bannerText;
    public GameObject subText;
    public GameObject pauseCanvas;
    public GameObject roundCounter;
    public float roundNum;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mainMenu(){
        SceneManager.LoadScene(0);
    }

    public void backYard(){
        SceneManager.LoadScene(2);
    }

    public IEnumerator RoundEnd(float roundNumber){
        bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = (roundNumber + 5).ToString() + ":00 PM";
        if(7-roundNumber == 1){
            subText.GetComponent<TMPro.TextMeshProUGUI>().text = "1 hour until midnight";
            roundCounter.GetComponent<TMPro.TextMeshProUGUI>().text = "1 hour until midnight";
        } else {
            subText.GetComponent<TMPro.TextMeshProUGUI>().text = (7-roundNumber).ToString() + " hours until midnight";
            if(roundNumber == 1){
                subText.GetComponent<TMPro.TextMeshProUGUI>().text += "\nTricks and Treats give points";
            }
            roundCounter.GetComponent<TMPro.TextMeshProUGUI>().text = (7 - roundNumber).ToString() +" hours until midnight";
        }
        StartCoroutine(FadeTo(1,1));
        Time.timeScale = 0;
        //------------------------------------------------------------
        foreach (Transform child in GameObject.Find("StationUICanvas").transform){
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in GameObject.Find("HUDCanvas").transform)
        {
            child.gameObject.SetActive(false);
        }
        //------------------------------------------------------------
        float startTime = Time.realtimeSinceStartup;
        // Loop runs until 3 seconds of real time has passed
        while (Time.realtimeSinceStartup - startTime < 3)
        {
            if (pauseCanvas.GetComponent<PauseScript>().isPaused) //pauseScript.GetComponent<PauseScript>().isPaused == true
            {
                startTime += Time.unscaledDeltaTime;
            }
            yield return null;
        }
        StartCoroutine(FadeTo(0,1));
        Time.timeScale = 1;
        roundNum = 7 - roundNumber; // keep track for loss screen
        //------------------------------------------------------------
        foreach (Transform child in GameObject.Find("StationUICanvas").transform){
            if(child.name != "CancelPrompt")
                child.gameObject.SetActive(true);
        }
        foreach (Transform child in GameObject.Find("HUDCanvas").transform)
        {
            
            child.gameObject.SetActive(true);
        }
        //------------------------------------------------------------
    }

    public IEnumerator Victory()
    {
        StartCoroutine(FadeTo(1,2));
        // Other victory animations and sounds can go here
        bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "Midnight!\nYou Win!";
        roundCounter.GetComponent<TMPro.TextMeshProUGUI>().text = "Midnight! Great Job!";
        subText.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        GameObject.Find("StationUICanvas").SetActive(false);
        yield return new WaitForSeconds(3);
        foreach(Transform child in bannerText.transform){
            child.gameObject.SetActive(true);
        }
        
    }


    // This fade to works regardless of if the timescale for the game is 0,
    //  so text can fade in when the game is stopped
    IEnumerator FadeTo(float desiredAlpha, float desiredTime)
    {
        TMP_Text textField = bannerText.GetComponent<TMP_Text>();
        TMP_Text subTextField = subText.GetComponent<TMP_Text>();
        float alpha = textField.color.a;
        Color newColor = Color.white;
        for (float t = 0.0f; t < 1.0f; t += Time.unscaledDeltaTime / desiredTime)
        {
            newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, desiredAlpha, t));
            textField.color = newColor;
            subTextField.color = newColor;
            yield return null;
        }
        if (newColor.a > 0 && desiredAlpha == 0)
        {
            newColor.a = 0;
            textField.color = newColor;
            subTextField.color = newColor;
        }
    }
}
