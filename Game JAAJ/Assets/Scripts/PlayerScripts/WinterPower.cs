using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterPower : MonoBehaviour
{
    public ParticleSystem particle;

    Rigidbody2D rb;
    GameObject frostEnemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //
    }

    void Freeze()
    {
        FindObjectOfType<ScriptAudioManager>().Play("inverno");

        if (frostEnemy != null)
        {
            frostEnemy.GetComponent<Animator>().speed = 0;

            if(frostEnemy.GetComponent<SlimeEnemy>() != null)
            {
                frostEnemy.GetComponent<SlimeEnemy>().jumping = false;
            }

            frostEnemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            //change material
        }

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    void EndWinter()
    {
        if (frostEnemy != null)
        {
               frostEnemy.GetComponent<Animator>().speed = 1;

            if (frostEnemy.GetComponent<SlimeEnemy>() != null)
            {
                frostEnemy.GetComponent<SlimeEnemy>().jumping = true;
            }

            frostEnemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            //change material
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem particlesystem = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(particlesystem, 1.5f);

        if (collision.gameObject.CompareTag("MiniSlime"))
        {
            frostEnemy = collision.gameObject;

            Freeze();
        }
    }
}
