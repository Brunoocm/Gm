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
        FindObjectOfType<ScriptAudioManager>().Play("outono");

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //
    }

    void StartAutumn()
    {
        anchoredEnemy.GetComponent<DistanceJoint2D>().enabled = true;
        anchoredEnemy.GetComponent<DistanceJoint2D>().connectedBody = rb;

        if (anchoredEnemy.GetComponent<SlimeEnemy>() != null)
        {
            anchoredEnemy.GetComponent<SlimeEnemy>().jumping = false;
        }

        Invoke("EndAutumn", 3);
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

            StartAutumn();
        }
    }
}
