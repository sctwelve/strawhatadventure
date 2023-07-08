using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Public Fields
    public float speed = 1;

    //Private Fields
    Rigidbody2D rb;
    Animator animator;
    float horizontalValue;
    float runSpeedModifier = 2f;
    bool isRunning = false;
    bool facingRight = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        //Store the horizontal value
        horizontalValue = Input.GetAxisRaw("Horizontal");

        // If LShift is clicked enable isRunning
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning
            = true;
        // If LShift is released disable isRunning
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning
            = false;
    }

    private void FixedUpdate()
    {
        Move(horizontalValue);
    }

    void Move(float dir)
    {
        // Set value of x using dir and speed
        float xVal = dir * speed * 100 * Time.deltaTime;
        // If we are running mulitply with the running modifier
        if (isRunning)
            xVal *= runSpeedModifier;
        //Create Vec2 for the velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Set the player's velocity
        rb.velocity = targetVelocity;

        // If looking right and clicked left (flip to the left)
        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        // If looking left and clicked right (flip to rhe right)
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

        // (e idle 4 walking , 8 running)
        //Set the float xVe10city according to the x value
        //of the RigidBody2D velocity
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }
}
