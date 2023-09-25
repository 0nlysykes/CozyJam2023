using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewStationFromClick : MonoBehaviour
{
    // Objects to grab
    public GameObject StationToSpawn; //direct reference to prefab for station 
    public GameObject PlayerCurrency; // direct reference to text box for currency
    //public GameObject PlayerCurrency; // direct reference to text box for currency
    private int stationCost; // will grab a cost reference to the universal script for station properties

    // Other
    private string playerCur;
    private int playerCurrency;
    private bool validCurrency = false; // will keep track of whether player has enough currency
    private bool spawningStation = false; // keeps track of whether a station is ACTIVELY being spawned (player is dragging it on scene)
    private GameObject newstation; // this is to keep track of the station we are spawning from the button click
    private Vector3 mousePos; // to keep track of the mouse position 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawningStation == true)
        {
            getMousePosition();
            followMouse();
            checkForMouseClickToPlace();
        }
    }

    private void getMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void checkForMouseClickToPlace()
    {
        if (Input.GetMouseButtonDown(0)) // if the player clicks, then drop the station on that spot
        {
            getMousePosition();
            newstation.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
            spawningStation = false;
            enableScripts(); // reactivate the station's script's once placed
        }
    }
    public void SpawnNew()
    {
        CheckForValidCurrency();
        if (validCurrency == true) // assure player has enough currency to purchase station
        {
            Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newstation = Instantiate(StationToSpawn, currentPosition, Quaternion.identity); //spawn station on the mouse
            spawningStation = true;
            enableScripts(); // deactivate the station's script's once button is pressed
            Update(); // kick off update to track object alongside the cursor
        }
        else
        {
            ///////////////// TODO need to write in some kind of message here for invalid currency
        }

    }

    void disableScripts()
    {
        MonoBehaviour[] scripts = newstation.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }

    void enableScripts()
    {
        MonoBehaviour[] scripts = newstation.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }

    private void followMouse()
    {
        newstation.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0); // keep following the mouse cursor       
    }

    private void CheckForValidCurrency()
    {
        Debug.Log(PlayerCurrency.GetComponent<TMP_Text>().text);
        playerCur = PlayerCurrency.GetComponent<TMP_Text>().text;

        // Debug.Log(PlayerCurrency.GetComponent<Text>().text);
        int.TryParse(playerCur, out playerCurrency); // grab cost string from text box and parse to int
        //playerCurrency = int.Parse(PlayerCurrency.text);
        Debug.Log("player currency: " + playerCurrency);


        stationCost = StationToSpawn.GetComponent<StationUniversalProperties>().cost; //get the cost of the station
        Debug.Log("station cost: " + stationCost);
        if (playerCurrency > stationCost) // check if player has enough currency
        {
            playerCurrency = playerCurrency - stationCost; // subtract from player currency
            validCurrency = true;
            UpdateCurrencyUI();
        }
        else
        {
            validCurrency = false;
        }
    }

    private void UpdateCurrencyUI()
    {
        playerCur = playerCurrency.ToString();
        PlayerCurrency.GetComponent<TMP_Text>().text = playerCur; // throw new player currency up to UI
    }
}
