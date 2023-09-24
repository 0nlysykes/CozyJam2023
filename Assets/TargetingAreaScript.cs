using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetingAreaScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        transform.parent.gameObject.GetComponent<CandyStationScript>().targetingAreaCollisionEnter(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        transform.parent.gameObject.GetComponent<CandyStationScript>().targetingAreaCollisionExit(other);
    }
}
