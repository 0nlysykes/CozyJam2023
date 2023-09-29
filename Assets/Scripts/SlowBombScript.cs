using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBombScript : MonoBehaviour
{
    public Vector3 scaleChange = new Vector3(.015f, .015f, 0f);
    public float slowValue = 0.6f;
    public float slowDuration = 3;
    private float timer;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(gameObject.transform.localScale.x < 0.5 && gameObject.transform.localScale.y < 0.5){
            gameObject.transform.localScale += scaleChange;
        }
        
        if(timer > 1f){
            StartCoroutine(FadeTo(0, 1.5f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            other.gameObject.GetComponent<EnemyScript>().slowDown(slowValue, slowDuration);
        }
    }

    IEnumerator FadeTo(float desiredAlpha, float desiredTime)
    {
        Color newColor = gameObject.GetComponent<SpriteRenderer>().color;
        float alpha = newColor.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / desiredTime)
        {
            newColor.a = Mathf.Lerp(alpha, desiredAlpha, t);
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
        Destroy(gameObject);
    }
}
