using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeStationScript : MonoBehaviour
{
    // cursor image for upgrade button
    [SerializeField] public Texture2D upgradeCursor;
    public bool upgradingStation = false;
    //Cancel prompt is the same as it is in NewStationFromClick, it tells the user how to leave upgrade mode
    public GameObject cancelPrompt;
    //popUpBox is the pop up that appears to show the player if a station is upgradeable
    public GameObject popUpBox;
    //public TMP_Text popUpText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(upgradingStation){
            //spawningStation = false;
            FollowMouse();
            checkForClick();
        }
    }

    public void UpgradeTime(){
        Cursor.SetCursor(upgradeCursor, Vector2.zero, CursorMode.Auto);
        upgradingStation = true;
        transform.GetChild(0).gameObject.SetActive(false);
        cancelPrompt.gameObject.SetActive(true);
        Update();
    }

    private void checkForClick(){
        if (Input.GetMouseButtonDown(0)) // if the player clicks, then drop the station on that spot
        {
            transform.GetChild(0).gameObject.SetActive(true);
            cancelUpgrade();
            //else if covers if game is paused, deleting the station if it is
        } else if(Input.GetMouseButtonDown(1)){
            transform.GetChild(0).gameObject.SetActive(true);
            cancelUpgrade();
        } else if (GameObject.Find("PauseCanvas").GetComponent<PauseScript>().isPaused){
            cancelUpgrade();
        }
    }

    private void cancelUpgrade(){
        upgradingStation = false;
        cancelPrompt.gameObject.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        ClosePopUp();
    }

    private void FollowMouse(){
        popUpBox.transform.position = Input.mousePosition + new Vector3(popUpBox.GetComponent<RectTransform>().rect.width/2, popUpBox.GetComponent<RectTransform>().rect.height/2, -1);
    }

    public void PopUp(string text){
        popUpBox.SetActive(true);
        //popUpText.text = text;
        popUpBox.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void ClosePopUp(){
        popUpBox.SetActive(false);
    }
}
