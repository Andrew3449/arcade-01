using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.TestTools.Constraints;


public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Player pl;
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
        pl = GetComponent<Player>();
    }

    void Update()
    {
        if (isDead)
        {
            Destroy(pl);
            SceneManager.LoadScene("DeathScreen");
        }
        else
        {
            hInput = Input.GetAxis("Horizontal");
            flip();
            if ((Input.GetButtonDown("Jump") && isGrounded) || (Input.GetButtonDown("Jump") && jumpCount < 2 && isFalling == true))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                isGrounded = false;
                jumpCount += 1;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
        if (rb.velocity.y < 0)
        {
            isFalling = true;
        }
    }

    private void flip()
    {
        if (hInput < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "DeathBox")
        {
            isDead = true;
            isGrounded = true;
        }
        else if (collision.collider.name != "WinBox")
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
}
