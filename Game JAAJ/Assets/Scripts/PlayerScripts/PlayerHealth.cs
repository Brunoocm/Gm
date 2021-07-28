using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public float timerCd;

    private float timerCdBase;
    [SerializeField]private float timerAlpha;
    private bool getHit;
    private bool loop;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Material mat;
    Color alpha;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        alpha = sprite.GetComponent<SpriteRenderer>().color;
        rb = GetComponent<Rigidbody2D>();

        timerCdBase = timerCd;


        mat = sprite.material;
    }

    void Update()
    {
        if (getHit)
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

            mat.DisableKeyword("CONTRAST_ON");
            mat.SetFloat("_Contrast", 1f);
            mat.SetFloat("_Brightness", 0f);
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

                MaterialHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SlimeBoss") || other.gameObject.CompareTag("MiniSlime"))
        {
            if (!getHit)
            {
                health--;
                getHit = true;

                MaterialHit();
                
            }
        }
    }

    void MaterialHit()
    {
        mat.EnableKeyword("CONTRAST_ON");
        mat.SetFloat("_Contrast", 2.5f);
        mat.SetFloat("_Brightness", -0.7f);
    }
}
