using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public int health;
    public float speed;

    [SerializeField] private bool isRight;
    private bool jumping = true;
    private bool died;


    Animator anim;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!died)
        {
            Attack();
        }

        if(health <=0)
        {
            Destroy(gameObject);
            died = true;
        }
    }

    void Attack()
    {
        if(jumping)
        {
            if (isRight)
            {
                rb.velocity = transform.right * speed;
            }
            else
            {
                rb.velocity = transform.right * speed;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            if (isRight)
            {
                isRight = false;
                transform.eulerAngles = new Vector2(0, 180);
            }
            else
            {
                isRight = true;
                transform.eulerAngles = new Vector2(0, 0);
            }
        }

    }

    public void Dano(int dano)
    {
        health -= dano;
    }

}
