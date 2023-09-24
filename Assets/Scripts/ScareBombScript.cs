using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareBombScript : MonoBehaviour
{
    private Vector3 scaleChange = new Vector3(.025f, .025f, 0f);
    private float timer;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(gameObject.transform.localScale.x < 0.5 && gameObject.transform.localScale.y < 0.5){
            gameObject.transform.localScale += scaleChange;
        }
        
        if(timer > 1f){
            StartCoroutine(FadeTo(0, .5f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            other.gameObject.GetComponent<EnemyScript>().takeDamage(100);
        }
    }

    IEnumerator FadeTo(float desiredAlpha, float desiredTime)
    {
        float alpha = gameObject.GetComponent<SpriteRenderer>().color.a;
        Color newColor = Color.white;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / desiredTime)
        {
            newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, desiredAlpha, t));
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
        Destroy(gameObject);
    }
}