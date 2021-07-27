using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GolemBoss : MonoBehaviour
{
    [Header("Vida")]
    public int health;
    public float timerChange;
    private float timerChangeBase;
    private bool isDead;

    [Header("Soco")]
    [SerializeField] private bool attack;
    public float timerAir;
    public float speedAir;
    public GameObject Soco;
    public GameObject Soco2;
    private bool soco1 = true;
    private bool soco2;

    private float timerAirBase;
    private bool following;
    bool ativo;

    Vector3 newPos;


    Transform playerPos;
    Animator anim;


    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        timerAirBase = timerAir;
        timerChangeBase = timerChange;

    }


    void Update()
    {
        if (!isDead)
        {
            AttackFunction();
        }

        if(health <= 0)
        {
            Destroy(gameObject);
            isDead = true;
        }
    }

    void AttackFunction()
    {
        if (attack)
        {
            if (timerAir > 0)
            {
                timerAir -= Time.deltaTime;

                following = true;
            }
            else
            {
                following = false;
            }

            if (following)
            {
                if (soco1)
                {
                    Soco.transform.position =
                        Vector3.MoveTowards(Soco.transform.position, new Vector3(playerPos.position.x, Soco.transform.position.y, Soco.transform.position.z), speedAir * Time.deltaTime);
                    newPos = Soco.transform.position;
                  
                    if(!ativo)
                    {
                        anim.SetTrigger("SocoTrigger");
                        ativo = true;
                    }
                }
                if (soco2)
                {
                    Soco2.transform.position =
                        Vector3.MoveTowards(Soco2.transform.position, new Vector3(playerPos.position.x, Soco2.transform.position.y, Soco2.transform.position.z), speedAir * Time.deltaTime);
                    newPos = Soco2.transform.position;

                    if (!ativo)
                    {
                        anim.SetTrigger("SocoTrigger2");
                        ativo = true;
                    }
                }

                //anim.SetBool("Attack", true);
            }
            else
            {
                if (soco1)
                {
                    

                    soco1 = false;
                    soco2 = true;
                }
                else if (soco2)
                {

                    soco1 = true;
                    soco2 = false;
                }

                attack = false;
                ativo = false;
            }
        }
        else
        {
            timerAir = timerAirBase;

            timerChange -= Time.deltaTime;
            if (timerChange <= 0)
            {
                attack = true;
                timerChange = timerChangeBase;
            }


        }
    }

    public void Dano(int dano)
    {
        health -= dano;
    }


}
