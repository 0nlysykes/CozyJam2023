using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyScript : MonoBehaviour
{
    public int moneyTotal = 1000;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().text = moneyTotal.ToString(); // throw new player currency up to UI
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This is here so that the moneyTotal used by other scripts is READ ONLY
    // the value that get shown on screen is only changeable with changeValue()
    public int getValue(){
        return moneyTotal;
    }

    public void changeValue(int input){
        moneyTotal = moneyTotal + input;
        gameObject.GetComponent<TMP_Text>().text = moneyTotal.ToString();
    }
}
