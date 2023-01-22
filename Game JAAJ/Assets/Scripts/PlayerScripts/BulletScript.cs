using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Stats")]
    public int dano;
    public float speed;
    public float lifeTime;
    public float angle;

    [Header("Area")]
    public bool AreaDamage;
    public float radius;

    [Header("Spawn On Death")]
    public bool spawnOnDeath;
    public GameObject[] bulletOnDeath;

    [Header("VFX")]
    public ParticleSystem particle;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
    }

    private void OnDestroy()
    {
        particles();
        if (spawnOnDeath && bulletOnDeath != null)
        {
            for (int i = 0; i < bulletOnDeath.Length; i++)
            {
                GameObject d = Instantiate(bulletOnDeath[i], transform.position, Quaternion.identity);
                float bSpeed = d.GetComponent<BulletScript>().speed;
                float bAngle = d.GetComponent<BulletScript>().angle;
                //d.GetComponent<Rigidbody2D>().angularVelocity = bAngle;
                d.GetComponent<Rigidbody2D>().velocity = (transform.right * bSpeed) + (transform.up * bAngle);
                d.transform.right = transform.right.normalized;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       

        if (other.gameObject.CompareTag("SlimeBoss"))
        {
            other.gameObject.GetComponent<SlimeBoss>().Dano(dano);
            particles();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("MiniSlime"))
        {
            other.gameObject.GetComponent<SlimeEnemy>().Dano(dano);
            particles();
            Destroy(gameObject);
        }  
        if (other.gameObject.CompareTag("Golem"))
        {
            other.gameObject.GetComponent<GolemBoss>().Dano(dano);
            particles();
            Destroy(gameObject);
        }  
        if (other.gameObject.CompareTag("Shield"))
        {
            other.gameObject.transform.parent.GetComponent<UltimoBoss>().DanoShield(dano);
            particles();
            Destroy(gameObject);
        } 
        if (other.gameObject.CompareTag("UltimoBoss"))
        {
            other.gameObject.transform.GetComponent<UltimoBoss>().Dano(dano);
            particles();
            Destroy(gameObject);
        } 
        if (other.gameObject.CompareTag("Wall"))
        {
            particles();
            Destroy(gameObject);
        }

    }

    void particles()
    {
        ParticleSystem particlesystem = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(particlesystem, 1.5f);
    }
}
