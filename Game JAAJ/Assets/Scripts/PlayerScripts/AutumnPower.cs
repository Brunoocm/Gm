using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutumnPower : MonoBehaviour
{
    public ParticleSystem particle;

    Rigidbody2D rb;
    GameObject anchoredEnemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke("EndAutumn", 6);
    }

    void Update()
    {
        //
    }

    void EndAutumn()
    {
        if(anchoredEnemy != null)
        {
            anchoredEnemy.GetComponent<DistanceJoint2D>().enabled = false;
            anchoredEnemy.GetComponent<DistanceJoint2D>().connectedBody = null;

            if (anchoredEnemy.GetComponent<SlimeEnemy>() != null)
            {
                anchoredEnemy.GetComponent<SlimeEnemy>().jumping = true;
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem particlesystem = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(particlesystem, 1.5f);

        if(collision.GetComponent<DistanceJoint2D>() != null)
        {
            anchoredEnemy = collision.gameObject;

            collision.GetComponent<DistanceJoint2D>().enabled = true;
            collision.GetComponent<DistanceJoint2D>().connectedBody = rb;

            if(collision.GetComponent<SlimeEnemy>() != null)
            {
                collision.GetComponent<SlimeEnemy>().jumping = false;
            }
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            EndAutumn();
        }
    }
}
