using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //PegarBumerangueVelocidade
        if (Mathf.Abs(other.relativeVelocity.x) > 10.5f)
        {
            if (other.gameObject.CompareTag("Bumerangue"))
            {

            }

        }
        //PegarBumerangueVelocidade
    }
}
