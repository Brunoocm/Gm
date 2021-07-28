using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutumnPower : MonoBehaviour
{
    public ParticleSystem particle;

    void Start()
    {
        Destroy(gameObject, 6);
    }

    void Update()
    {
        //
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem particlesystem = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(particlesystem, 1.5f);

        //
    }
}
