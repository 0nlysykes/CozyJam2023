using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStationFromClick : MonoBehaviour
{
    public GameObject TurretToSpawn;
    private bool spawningTurret = false;
    private GameObject newTurret; // this is to keep track of the turret we are spawning from the button click
    private Vector3 mousePos; // to keep track of the mouse position 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void getMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
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
        while (spawningTurret == true)
        {
            getMousePosition();
            followMouse();
        }
        //}
    }

    private void followMouse()
    {
        newTurret.transform.position = mousePos;
        spawningTurret = false;
    }
}
