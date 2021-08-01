using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UltimoBoss : MonoBehaviour
{
    [Header("Main")]
    public int health;
    public Slider slider;
    public bool isIdle;
    public bool isLava;
    public bool isChuva;

    private bool isRandom;
    private int randomNum;
    [SerializeField]private float timer;

    [Header("Poder Lava")]
    public GameObject[] lavaObj;
    public float timerBtw;
    private float timerbaseBtw;
    private int numObj;

    [Header("Poder Chuva")]
    public Transform[] chuvaObj;
    public Transform[] sombraObj;
    public GameObject bullet;
    public GameObject sombra;
    public float timerChuva;
    private float timerBaseChuva;

    [Header("Protection")]
    public GameObject shield;
    public int shieldHealth;
    public float timerCD;

    private int shieldBaseHealth;
    private float timerBaseCD;
    BoxCollider2D coll;

    private void Awake()
    {
        shieldBaseHealth = shieldHealth;
        timerBaseCD = timerCD;
        timerBaseChuva = timerChuva;
        timerbaseBtw = timerBtw;
        numObj = lavaObj.Length;
    }
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        timerBtw = 0;


        slider.maxValue = health;
    }

    void Update()
    {
        AttackLava();
        AttackChuva();

        if (health <= 0)
        {
            Destroy(gameObject);

            SceneManager.LoadScene("FINAL");
        }
        else
        {
            slider.value = health;
        }

        if (shieldHealth >= 0)
        {
            shield.SetActive(true);
            coll.enabled = false;

        }
        else
        {
            timerCD -= Time.deltaTime;

            shield.SetActive(false);
            coll.enabled = true;
            if (timerCD <= 0)
            {               
                shieldHealth = shieldBaseHealth;
                timerCD = timerBaseCD;
            }
        }

        if(!isRandom)
        {
            RandomAttack();
        }
        else
        {
            timer += Time.deltaTime;
        }

        if (timer >= 10)
        {
            isLava = false;
            isChuva = false;
            isRandom = false;
            timer = 0; 
        }

    }

    void RandomAttack()
    {
        randomNum = Random.Range(0, 2);
        if (randomNum == 0)
        {
            isLava = true;
            isRandom = true;
        }
        else if (randomNum == 1)
        {
            isChuva = true;
            isRandom = true;
        }

    }

    void AttackLava()
    {
        if (isLava)
        {
            timerBtw -= Time.deltaTime;

            if (numObj >= 0 && timerBtw <= 0)
            {
                if(numObj < lavaObj.Length) lavaObj[numObj].SetActive(false);

                lavaObj[numObj-1].SetActive(true);

                numObj--;
                timerBtw = timerbaseBtw;
            }
            else if(numObj == 0)
            {
                numObj = lavaObj.Length;
                isLava = false;
                timerBtw = 0;
            } 

            
        }
        
    }

    void AttackChuva()
    {
        if (isChuva)
        {
            timerChuva -= Time.deltaTime;
            if (timerChuva <= 0)
            {
                int num = Random.Range(0, chuvaObj.Length - 1);

                GameObject bolaFogo = Instantiate(bullet, chuvaObj[num].position, Quaternion.identity);
                GameObject s = Instantiate(sombra, sombraObj[num].position, Quaternion.identity);
                bolaFogo.GetComponent<Rigidbody2D>().velocity = transform.up * -15;
                Destroy(bolaFogo, 1.25f);
                Destroy(s, 1.3f);

                timerChuva = timerBaseChuva;
                
            }
        }
    }

    public void DanoShield(int dano)
    {
        shieldHealth -= dano;
    } 
    public void Dano(int dano)
    {
        health -= dano;
    }
}
