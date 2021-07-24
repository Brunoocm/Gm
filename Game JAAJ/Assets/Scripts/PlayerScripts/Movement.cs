using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jump;
    public float jumping;

    public float radius;

    public Transform groundPos;
    public LayerMask layerGround;

    private float timerJump;
    private float move;
    private bool isJumping;
    private bool isGrounded;
    private bool isRight;
    private bool isLeft;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timerJump = jumping;
    }

    void Update()
    {
        Jump();

        Flip();
    }

    void FixedUpdate()
    {
        move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    void Flip()
    {
        if (!isRight && Input.GetKey(KeyCode.D) || !isRight && Input.GetKeyDown(KeyCode.RightArrow))
        {
            //transform.Translate(Vector2.right * speed * Time.deltaTime);
            isRight = true;
            isLeft = false;
            transform.eulerAngles = new Vector2(0, 0);
        }
        //Move Left
        if (!isLeft && Input.GetKey(KeyCode.A) || !isLeft && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isRight = false;
            isLeft = true;
            //transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180); //flip the character on its x axis
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, radius, layerGround);

        if (isGrounded && Input.GetKeyDown(KeyCode.W) || isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping = true;
            timerJump = jumping;
            rb.velocity = Vector2.up * jump;
        }

        if (isJumping && Input.GetKey(KeyCode.W) || isJumping && Input.GetKey(KeyCode.UpArrow))
        {
            if (timerJump > 0)
            {
                rb.velocity = Vector2.up * jump;
                timerJump -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }
    }
}
