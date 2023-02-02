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

    public ParticleSystem walkParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem doubleJumpParticle;

    private int m_numJumps;
    private float timerJump;
    private float move;
    private bool isJumping;
    private bool isGrounded;
    private bool isRight;
    [HideInInspector] public bool isDoubleJump;

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

        if (playerInput.actions["Interact"].IsPressed())
        {
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }


        if (rb.velocity.x != 0 && isGrounded)
        {
            walkParticle.Play();
            print("walking");

        }
        else
        {
            walkParticle.Stop();
            print("NOT walking");

        }

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

            if (SkillClock.skillOutono) numJumps = m_numJumps;
            else numJumps = 1;

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
            Instantiate(jumpParticle, groundPos.position, jumpParticle.transform.rotation);

            //Instantiate(jumpParticle, groundPos.position, jumpParticle.transform.rotation);
        }
        else if (isJumping && playerInput.actions["Jump"].WasPressedThisFrame())
        {
            anim.SetBool("Jump", true);
            isJumping = true;
            isDoubleJump = true;
            timerJump = jumping;
            rb.velocity = Vector2.up * jump;
            FindObjectOfType<ScriptAudioManager>().Play("jump");
            Instantiate(doubleJumpParticle, groundPos.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
        }
    }
}
