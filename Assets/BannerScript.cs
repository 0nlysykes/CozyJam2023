using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BannerScript : MonoBehaviour
{
    public GameObject bannerText;
    public GameObject pauseCanvas;
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
        switch(roundNumber){
            case 1:
                bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "6:00 PM";
                break;
            case 2:
                bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "7:00 PM";
                break;
            case 3:
                bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "8:00 PM";
                break;
            case 4:
                bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "9:00 PM";
                break;
            case 5:
                bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "10:00 PM";
                break;
            case 6:
                bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "11:00 PM";
                break;
            default:
                bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                break;
        }
        StartCoroutine(FadeTo(1,1));
        Time.timeScale = 0;
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
    }

    public IEnumerator Victory()
    {
        StartCoroutine(FadeTo(1,2));
        // Other victory animations and sounds can go here
        bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "Midnight!\nYou Win!";
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
        float alpha = textField.color.a;
        Color newColor = Color.white;
        for (float t = 0.0f; t < 1.0f; t += Time.unscaledDeltaTime / desiredTime)
        {
            newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, desiredAlpha, t));
            textField.color = newColor;
            yield return null;
        }
        if (newColor.a > 0 && desiredAlpha == 0)
        {
            newColor.a = 0;
            textField.color = newColor;
        }
    }
}
