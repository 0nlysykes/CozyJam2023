using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public static int whichYard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void frontYard(){
        whichYard = 1;
        SceneManager.LoadScene(1);
    }

    public void backYard(){
        whichYard = 2;
        SceneManager.LoadScene(2);
    }

    public void exitGame(){
        Application.Quit();
    }
}
