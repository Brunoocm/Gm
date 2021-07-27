using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public float timerCd;

    private float timerCdBase;
    [SerializeField]private float timerAlpha;
    private bool getHit;
    private bool loop;

    SpriteRenderer sprite;
    Color alpha;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        alpha = sprite.GetComponent<SpriteRenderer>().color;

        timerCdBase = timerCd;

    }

    void Update()
    {      
        if(getHit)
        {
            timerCd -= Time.deltaTime;
            if(timerCd <= 0)
            {
                getHit = false;
                timerCd = timerCdBase;
            }
         
            if(!loop && timerAlpha <= 1.5f && timerAlpha > 0.4f)
            {
                timerAlpha -= Time.deltaTime * 1.5f;
            }
            else
            {
                loop = true;
            }
            if(loop)
            {
                timerAlpha += Time.deltaTime * 1.5f;
            }
            if(loop && timerAlpha >= 1)
            {
                loop = false;
            }

            alpha.a = timerAlpha;
            sprite.color = alpha;
        }
        else
        {
            alpha.a = 1f;
            sprite.color = alpha;
        }

     
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Soco"))
        {
            if (!getHit)
            {
                health--;
                getHit = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
    }
}
