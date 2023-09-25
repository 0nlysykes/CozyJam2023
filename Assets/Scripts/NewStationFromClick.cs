using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStationFromClick : MonoBehaviour
{
    public GameObject TurretToSpawn;
    private int x = 0;
    private bool spawningTurret = false;
    private GameObject newTurret; // this is to keep track of the turret we are spawning from the button click
    private Vector3 mousePos; // to keep track of the mouse position 
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hi");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawningTurret == true)
        {
            getMousePosition();
            followMouse();
            checkForMouseClick();
        }
    }

    private void getMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void checkForMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            getMousePosition();
            newTurret.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
            spawningTurret = false;
        }
    }
    public void SpawnNew()
    {
        Debug.Log("it's firing");
        //if (Input.GetMouseButtonDown(0))
        //{
        Debug.Log("definitely firing");
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        newTurret = Instantiate(TurretToSpawn, currentPosition, Quaternion.identity);
        Debug.Log("definitely firing 2");

        spawningTurret = true;
        Update();
        //}Debug.Log("Hi");
        //Debug.Log("Hi");
    }

    private void followMouse()
    {
        newTurret.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
        //x++;
        //if(x > 10000000000)
        //{
        //    Debug.Log(x);
        //    spawningTurret = false;
        //}
        
    }
}
