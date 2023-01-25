using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float speed;

    [SerializeField] private bool isRight;
    [HideInInspector] public bool jumping = true;
    private bool died;
    private bool isFreezed;


    EnemyHealth enemyHealth;
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (enemyHealth.health <= 0)
        {
            Destroy(gameObject);
            died = true;
        }

        if (isFreezed)
            return;

        if (!died)
        {
            Attack();
        }

        

    }
    public IEnumerator Freeze(float duration)
    {
        if (!isFreezed)
        {
            isFreezed = true;
            anim.speed = 0;
            rb.bodyType = RigidbodyType2D.Static;
            yield return new WaitForSeconds(duration);
            rb.bodyType = RigidbodyType2D.Dynamic;
            anim.speed = 1;
            isFreezed = false;
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
                Flip();         
            }
            else
            {
                isRight = true;
                Flip();
            }
        }

        void Flip()
        {
            Vector3 lTemp = transform.localScale;
            lTemp.x *= -1;
            transform.localScale = lTemp;
        }
    }

    public void Dano(int dano)
    {
        enemyHealth.health -= dano;
    }
   
}
