using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int dano;
    public ParticleSystem particle;

    void Start()
    {
        Destroy(gameObject, 6f);
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ParticleSystem particlesystem = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(particlesystem, 1.5f);

        if (other.gameObject.CompareTag("SlimeBoss"))
        {
            other.gameObject.GetComponent<SlimeBoss>().Dano(dano);
        
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("MiniSlime"))
        {
            other.gameObject.GetComponent<SlimeEnemy>().Dano(dano);

            Destroy(gameObject);
        }  
        if (other.gameObject.CompareTag("Golem"))
        {
            other.gameObject.GetComponent<GolemBoss>().Dano(dano);

            Destroy(gameObject);
        }

    }
}
