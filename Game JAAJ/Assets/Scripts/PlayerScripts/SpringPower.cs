using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpringPower : MonoBehaviour
{
    public ParticleSystem particle;

    Rigidbody2D rb;
    PlayerHealth healthScript;
    void Start()
    {
        FindObjectOfType<ScriptAudioManager>().Play("primavera");

        rb = GetComponent<Rigidbody2D>();
        healthScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        //
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("MiniSlime") || other.gameObject.CompareTag("SlimeBoss") || other.gameObject.CompareTag("Golem"))
        {
            if(healthScript.health < 6) healthScript.health++;
        }
    }
}
