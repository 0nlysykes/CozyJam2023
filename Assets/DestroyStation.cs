using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStation : MonoBehaviour
{
    // cursor image for upgrade button
    [SerializeField] public Texture2D destroyCursor;
    public bool destroyingStation = false;
    //Cancel prompt is the same as it is in NewStationFromClick, it tells the user how to leave upgrade mode
    public GameObject cancelPrompt;
    public GameObject destroyTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyingStation){
            //spawningStation = false;
            checkForClick();
        }
    }

    public void DestroyTime(){
        cancelPrompt.GetComponent<TMPro.TextMeshProUGUI>().text = "Right Click to Cancel Demolish";
        Cursor.SetCursor(destroyCursor, new Vector2(.5f*destroyCursor.width, .5f*destroyCursor.height), CursorMode.Auto);
        destroyingStation = true;
        foreach (Transform child in transform){
            child.gameObject.SetActive(false);
        }
        cancelPrompt.gameObject.SetActive(true);
        Update();
    }

    private void checkForClick(){
        if (Input.GetMouseButtonDown(0) && destroyTarget != null) // if the player clicks, try to upgrade the station the mouse is over
        {
            Destroy(destroyTarget);
            foreach (Transform child in transform){
                child.gameObject.SetActive(true);
            }
            cancelDestroy(); 
            
            //else if covers if game is paused, deleting the upgrade UI if it is
        } else if(Input.GetMouseButtonDown(1)){
            foreach (Transform child in transform){
                child.gameObject.SetActive(true);
            }
            cancelDestroy();
        } else if (GameObject.Find("PauseCanvas").GetComponent<PauseScript>().isPaused){
            cancelDestroy();
        }
    }

    public void cancelDestroy(){
        if(destroyingStation){
            destroyingStation = false;
            cancelPrompt.gameObject.SetActive(false);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

}
