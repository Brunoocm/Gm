using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public int health;
    public float speed;

    [SerializeField] private bool isRight;
    [HideInInspector] public bool jumping = true;
    private bool died;


    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sprite;
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
        if (jumping)
        {
            if (isRight)
            { 
                rb.velocity = transform.right * speed;
            }
            else
            {
                rb.velocity = transform.right * -speed;
            }
        }
        else 
        {
        }

       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (isRight)
            {
                isRight = false;
                //transform.eulerAngles = new Vector2(0, 180);
                Flip();

                //if (speed < 0)
                //{
                //    isRight = true;
                //}
                
            }
            else
            {
                isRight = true;
                //transform.eulerAngles = new Vector2(0, 0);
                Flip();

                //if(speed > 0)
                //{
                //    isRight = false;
                //}
            }
        }

        void Flip()
        {
            Vector3 lTemp = transform.localScale;
            lTemp.x *= -1;
            transform.localScale = lTemp;
        }

        //if (other.gameObject.CompareTag("Player"))
        //{
        //    Rigidbody2D player = other.GetComponent<Rigidbody2D>();
        //    if (player != null)
        //    {
        //        //player.isKinematic = true;
        //        Vector2 difference = player.transform.position - transform.position;
        //        difference = difference.normalized * 4;
        //        player.AddForce(difference, ForceMode2D.Force);
        //        StartCoroutine(KnockbackWait(player));

        //    }
        //}
    }

    public void Dano(int dano)
    {
        health -= dano;
    }

    //IEnumerator KnockbackWait(Rigidbody2D player)
    //{
    //    if (player != null)
    //    {
    //        yield return new WaitForSeconds(1);
    //        player.velocity = Vector2.zero;
    //        //player.isKinematic = false;
    //    }
    //}

   
}
