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

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void firstYard(){
        whichYard = 1;
        SceneManager.LoadScene("MainScene");
    }

    public void secondYard()
    {
        whichYard = 2;
        SceneManager.LoadScene("MediumLevel");
    }

    public void thirdYard()
    {
        whichYard = 3;
        SceneManager.LoadScene("HardLevel");
    }

    public void stageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }


    /*public void secondYard()
    {
        whichYard = 2;
        SceneManager.LoadScene(2);
    }*/

    public void credits()
    {
        SceneManager.LoadScene("Credits");
    }

    // public void exitGame(){
        // Application.Quit();
    // }
}
