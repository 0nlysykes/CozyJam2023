using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveToMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void quitGame(){
        Application.Quit();
    }

    public void retryGame(){
        SceneManager.LoadScene(MainMenuScript.whichYard); //MainMenuScript.whichYard
    }
}
