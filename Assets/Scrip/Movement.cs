﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{

    [SerializeField] private float Movement_Speed;
    [SerializeField] private Rigidbody2D PlayerRb;
    private float Dir;
     // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        if (Dir > 0)
        {
            transform.localScale = new Vector3(-1, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(1, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Dir = Input.GetAxis("Horizontal") * Movement_Speed;
        
    }

    void FixedUpdate()
    {
            PlayerRb.velocity = new Vector2(Dir * Time.deltaTime, PlayerRb.velocity.y);
    }

}
    


     
   
   
