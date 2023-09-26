using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BannerScript : MonoBehaviour
{
    public GameObject bannerText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mainMenu(){
        SceneManager.LoadScene(0);
    }

    public void backYard(){
        SceneManager.LoadScene(2);
    }

    // public IEnumerator RoundEnd(float timer){
    //     bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "Old Time";
    //     StartCoroutine(FadeTo(1,1));
    //     yield return new WaitForSeconds(3);

    // }

    public IEnumerator Victory()
    {
        StartCoroutine(FadeTo(1,2));
        // Other victory animations and sounds can go here
        bannerText.GetComponent<TMPro.TextMeshProUGUI>().text = "You Win!";
        GameObject.Find("StationUICanvas").SetActive(false);
        yield return new WaitForSeconds(3);
        foreach(Transform child in bannerText.transform){
            child.gameObject.SetActive(true);
        }
        
    }

    IEnumerator FadeTo(float desiredAlpha, float desiredTime)
    {
        TMP_Text textField = bannerText.GetComponent<TMP_Text>();
        float alpha = textField.color.a;
        Color newColor = Color.white;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / desiredTime)
        {
            newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, desiredAlpha, t));
            textField.color = newColor;
            yield return null;
        }
        if (newColor.a > 0 && desiredAlpha == 0)
        {
            newColor.a = 0;
            textField.color = newColor;
        }
    }
}
