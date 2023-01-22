using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public int numJumps;
    public float speed;
    public float jump;
    public float jumping;
    public float velocidadeAr;
    public float radius;
    public bool canMove;

    public Transform groundPos;
    public LayerMask layerGround;

    private int m_numJumps;
    private float timerJump;
    private float move;
    private bool isJumping;
    private bool isGrounded;
    private bool isRight;
    private bool isDoubleJump;

    private PlayerInput playerInput;
   
    Rigidbody2D rb;
    Animator anim;
    
    private void Awake()
    {
        canMove = true;
        m_numJumps = numJumps;
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timerJump = jumping;
    }

    void Update()
    {
        if (!canMove)
            return;

        Jump();

        Flip();
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

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

    public void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, radius, layerGround);

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
            isDoubleJump = false;
            numJumps = m_numJumps;
        }
        else
        {
            anim.SetBool("Jump", true);
        }

        if (numJumps <= 0)
            return;

        if (isGrounded && playerInput.actions["Jump"].WasPressedThisFrame())
        {
            anim.SetBool("Jump", true);
            isJumping = true;
            timerJump = jumping;
            rb.velocity = Vector2.up * jump;
            FindObjectOfType<ScriptAudioManager>().Play("jump");
        }
        else if (isJumping && playerInput.actions["Jump"].WasPressedThisFrame())
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
                //isJumping = false;
            }
        }

        if (isJumping && playerInput.actions["Jump"].WasReleasedThisFrame())
        {
            numJumps--;
        }
    }

    public IEnumerator JumpTutorial()
    {
        while (timerJump > 0)
        {
            anim.SetBool("Jump", true);
            rb.velocity = Vector2.up * jump;
            timerJump -= Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(jumping);
        anim.SetBool("Jump", false);
        isJumping = false;
    }

}
