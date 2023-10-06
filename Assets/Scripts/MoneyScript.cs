using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    public int moneyTotal = 1000;
    private int candyCost;
    private int slowCost;
    private int scareCost;
    public GameObject candyStationButton;
    public GameObject slowStationButton;
    public GameObject scareStationButton;
    // Start is called before the first frame update
    void Start()
    {
        GameObject balanceScript = GameObject.Find("EventSystem");
        moneyTotal = balanceScript.GetComponent<BalanceScript>().startingPoints;
        gameObject.GetComponent<TMP_Text>().text = moneyTotal.ToString(); // throw new player currency up to UI

        // Set coin values for the other things
        candyCost = balanceScript.GetComponent<BalanceScript>().candyStationCost;
        slowCost = balanceScript.GetComponent<BalanceScript>().slowStationCost;
        scareCost = balanceScript.GetComponent<BalanceScript>().scareStationCost;
        candyStationButton.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = candyCost.ToString();
        slowStationButton.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = slowCost.ToString();
        scareStationButton.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = scareCost.ToString();
        changeValue(0);
    }

    // Update is called once per frame
    void FixedUpdate()
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

        // gray out buttons for candy station cost
        if(moneyTotal < candyCost){
            candyStationButton.GetComponent<Image>().color = Color.gray;
            candyStationButton.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
        } else {
            candyStationButton.GetComponent<Image>().color = Color.white;
            candyStationButton.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        // gray out buttons for slow station cost
        if(moneyTotal < slowCost){
            slowStationButton.GetComponent<Image>().color = Color.gray;
            slowStationButton.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
        } else {
            slowStationButton.GetComponent<Image>().color = Color.white;
            slowStationButton.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        // gray out buttons for scare station cost
        if(moneyTotal < scareCost){
            scareStationButton.GetComponent<Image>().color = Color.gray;
            scareStationButton.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
        } else {
            scareStationButton.GetComponent<Image>().color = Color.white;
            scareStationButton.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
    }
}
