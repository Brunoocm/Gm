using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float speed;

    [SerializeField] private bool isRight;
    private bool jumping = true;


    Animator anim;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
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
}
