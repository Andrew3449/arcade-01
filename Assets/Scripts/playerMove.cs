using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
// using UnityEngine.TestTools.Constraints;


public class Example : MonoBehaviour
{
    Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isFalling = false;
    private bool isDead = false;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
     private int jumpCount = 0;
    float hInput; // horizontal input


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        if ((Input.GetButtonDown("Jump") && isGrounded) || (Input.GetButtonDown("Jump") && jumpCount < 2 && isFalling == true))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            isGrounded = false;
            jumpCount += 1;
        }
        print(jumpCount);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
        if (rb.velocity.y < 0)
        {
            isFalling = true;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "DeathBox")
        {
            isDead = true;
            isGrounded = true;
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            isGrounded = true;
            jumpCount = 0; 
        }
    }
}
