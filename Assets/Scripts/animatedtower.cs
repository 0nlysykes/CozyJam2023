using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatedtower : MonoBehaviour
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
        bool active = animator.GetBool("active");
        if (Input.GetKeyUp(KeyCode.Q))
            animator.SetBool("active", !active);

       









    }



}
