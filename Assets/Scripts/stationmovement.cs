using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stationmovement : MonoBehaviour
{
    public float baseSpeed = 1f;
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        //Input
        bool full = animator.GetBool("full");
        if (Input.GetKeyUp(KeyCode.J))
            animator.SetBool("full", !full);

        bool halffull = animator.GetBool("halffull");
        if (Input.GetKeyUp(KeyCode.K))
            animator.SetBool("halffull", !halffull);
        


        bool empty = animator.GetBool("empty");
        if (Input.GetKeyUp(KeyCode.L))
            animator.SetBool("empty", !empty);
       

        







    }



}
