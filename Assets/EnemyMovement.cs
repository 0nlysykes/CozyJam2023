using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
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
        bool sad = animator.GetBool("Sad");
        if (Input.GetKeyUp(KeyCode.P))
            animator.SetBool("Sad", !sad);
        if (sad)
        {
            moveSpeed = baseSpeed / 2;
        }
        else
        {
            moveSpeed = baseSpeed;
        }


        bool IsSlowed = animator.GetBool("IsSlowed");
        if (Input.GetKeyUp(KeyCode.O))
            animator.SetBool("IsSlowed", !IsSlowed);
        if (IsSlowed)
        {
            moveSpeed = moveSpeed / 4;
        }






        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("IsMoving", movement.magnitude);

    }


    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }



}
