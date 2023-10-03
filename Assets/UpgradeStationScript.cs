using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStationScript : MonoBehaviour
{
    // cursor image for upgrade button
    [SerializeField] public Texture2D upgradeCursor;
    public bool upgradingStation = false;
    //Cancel prompt is the same as it is in NewStationFromClick, it tells the user how to leave upgrade mode
    public GameObject cancelPrompt;
    //popUpBox is the pop up that appears to show the player if a station is upgradeable
    public GameObject popUpBox;
    public GameObject upgradeTarget = null;
    public GameObject PlayerCurrency;

    private int upgradeCost;
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
        if(popUpBox.activeSelf){
            if(!CheckForValidCurrency()){
                popUpBox.GetComponent<Image>().color = new Color(0.7764706f, 0.3058824f, 0.172549f, 1);
            } else {
                popUpBox.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void UpgradeTime(){
        Cursor.SetCursor(upgradeCursor, Vector2.zero, CursorMode.Auto);
        upgradingStation = true;
        foreach (Transform child in transform){
            child.gameObject.SetActive(false);
        }
        cancelPrompt.gameObject.SetActive(true);
        Update();
    }

    private void checkForClick(){
        if (Input.GetMouseButtonDown(0) && upgradeTarget != null) // if the player clicks, try to upgrade the station the mouse is over
        {
            if(CheckForValidCurrency() && !upgradeTarget.GetComponent<StationUniversalProperties>().isUpgraded){
                switch(upgradeTarget.gameObject.tag){
                    case "CandyStation":
                        upgradeTarget.GetComponent<CandyStationScript>().upgrade();
                        break;
                    case "SlowStation":
                        upgradeTarget.GetComponent<SlowStationScript>().upgrade();
                        break;
                    case "ScareStation":
                        upgradeTarget.GetComponent<ScareStationScript>().upgrade();
                        break;
                    default:
                        break;
                }
                PlayerCurrency.GetComponent<MoneyScript>().changeValue(-1*upgradeCost);
                foreach (Transform child in transform){
                    child.gameObject.SetActive(true);
                }
                cancelUpgrade(); 
            }
            
            //else if covers if game is paused, deleting the upgrade UI if it is
        } else if(Input.GetMouseButtonDown(1)){
            foreach (Transform child in transform){
                child.gameObject.SetActive(true);
            }
            cancelUpgrade();
        } else if (GameObject.Find("PauseCanvas").GetComponent<PauseScript>().isPaused){
            cancelUpgrade();
        }
    }

    public bool CheckForValidCurrency(){
        upgradeCost = upgradeTarget.GetComponent<StationUniversalProperties>().costToUpgrade;
        return PlayerCurrency.GetComponent<MoneyScript>().getValue()>=upgradeCost;
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
        popUpBox.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void ClosePopUp(){
        popUpBox.GetComponent<Image>().color = Color.white;
        popUpBox.SetActive(false);
    }
}
