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

        Invoke("EndWinter", 5);
    }

    void Update()
    {
        //
    }

    void Freeze()
    {
        if(frostEnemy != null)
        {
            frostEnemy.GetComponent<Animator>().speed = 0;

            if(frostEnemy.GetComponent<SlimeEnemy>() != null)
            {
                frostEnemy.GetComponent<SlimeEnemy>().jumping = false;
            }

            frostEnemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            //change material
        }
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

        if (collision.gameObject.CompareTag("MiniSlime") || collision.gameObject.CompareTag("SlimeBoss") || collision.gameObject.CompareTag("Golem"))
        {
            frostEnemy = collision.gameObject;

            Freeze();
        }
    }
}
