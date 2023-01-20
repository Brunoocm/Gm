using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jump;
    public float jumping;
    public float velocidadeAr;

    public float radius;

    public Transform groundPos;
    public LayerMask layerGround;

    private float timerJump;
    private float move;
    private bool isJumping;
    private bool isGrounded;
    private bool isRight;

    private PlayerInput playerInput;
   
    Rigidbody2D rb;
    Animator anim;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timerJump = jumping;
    }

    void Update()
    {
        Jump();

        Flip();
    }

    void FixedUpdate()
    {
        Vector2 moveVec = playerInput.actions["Move"].ReadValue<Vector2>();
        move = moveVec.x;
        anim.SetFloat("SpeedMove", Mathf.Abs(move));
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    void Flip()
    {
        if (move > 0)
        {
            isRight = true;

            transform.eulerAngles = new Vector2(0, 0);
        }
    
        if (move < 0)
        {
            isRight = false;
 
            transform.eulerAngles = new Vector2(0, 180); 
        }

    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, radius, layerGround);

        //if (isJumping) anim.SetBool("Jump", true);
        //else anim.SetBool("Jump", false);

        //if (isGrounded) anim.SetBool("Fall", false);


        if(isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }


        if (isGrounded && playerInput.actions["Jump"].WasPressedThisFrame())
        {
            anim.SetBool("Jump", true);
            isJumping = true;
            timerJump = jumping;
            rb.velocity = Vector2.up * jump;

            FindObjectOfType<ScriptAudioManager>().Play("jump");
        }

        if (isJumping && playerInput.actions["Jump"].IsPressed())
        {
            if (timerJump > 0)
            {
                anim.SetBool("Jump", true);
                rb.velocity = Vector2.up * jump;
                timerJump -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (playerInput.actions["Jump"].WasReleasedThisFrame())
        {
            isJumping = false;
        }
    }
}
