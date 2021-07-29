using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SlimeBoss : MonoBehaviour
{
    [Header("Health")]
    public int health;
    public Slider slide;

    [Header("Idle")]
    [SerializeField] private bool idle;
    public float timerBetweenSet;

    private float timerBetween;
    private bool died;

    [Header("Spawn")]
    [SerializeField] private bool spawnEnemy;
    public int slimesPerScene;
    public float timerSpawn;
    public GameObject SlimePrefab;

    private float timerBase;
    private int numSlimes;
    [SerializeField]private int count;
    GameObject[] ArraySlimes;
    Transform spawnPos;
   

    [Header("Jump")]
    [SerializeField] private bool jumpAttack;
    public float timerAir;
    public float speedAir;

    private float timerAirBase;
    private bool following;
    GameObject ShadowObj;
    Vector3 newPos;

    Transform playerPos;
    Animator anim;

    
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        spawnPos = GameObject.Find("SpawnPos").GetComponent<Transform>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        ShadowObj = GameObject.Find("ShadowFollow");

        timerBase = timerSpawn;
        timerAirBase = timerAir;

        slide.maxValue = health;
    }

    void Update()
    {
        if (!died)
        {

            ChangeStages();

            SpawnFunction();
            JumpFunction();

            ArraySlimes = GameObject.FindGameObjectsWithTag("MiniSlime");
            for (int i = 0; i < ArraySlimes.Length; i++)
            {
                numSlimes = i;
            }

            slide.value = health;

        }

        if (health <= 0)
        {
            Passar.primeiroBoss = true;
            Destroy(gameObject, 0.2f);
            died = true;
        }
    }

    void ChangeStages()
    {
       
        if (idle)
        {
            timerBetween += Time.deltaTime;

            if (timerBetween >= timerBetweenSet)
            {
                spawnEnemy = true;
                idle = false;

                count = numSlimes;

                timerBetween = 0;
            }
        }

    }

    void SpawnFunction()
    {
        if(spawnEnemy)
        {
            timerSpawn -= Time.deltaTime;

            if (timerSpawn <= 0 && count < slimesPerScene)
            {
                Instantiate(SlimePrefab, spawnPos.position, Quaternion.identity);
                count++;
                timerSpawn = timerBase;
            }
            if(numSlimes >= slimesPerScene || count >= slimesPerScene)
            {
                spawnEnemy = false;
                jumpAttack = true;

                count = numSlimes;

                timerBetween = 0;
            }
        }

    }

    void JumpFunction()
    {
        if(jumpAttack)
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
                ShadowObj.transform.position =
                    Vector3.MoveTowards(ShadowObj.transform.position, new Vector3(playerPos.position.x, ShadowObj.transform.position.y, ShadowObj.transform.position.z), speedAir * Time.deltaTime);

                newPos = ShadowObj.transform.position;

                anim.SetBool("Attack", true);

            }
            else
            {
                transform.position = new Vector3 (playerPos.position.x, transform.position.y, transform.position.z);
                ShadowObj.transform.position = newPos;        

                anim.SetTrigger("TriggerAttackFinal");

                count = numSlimes;

                jumpAttack = false;
                idle = true;
            }
        }
        else
        {
            anim.SetBool("Attack", false);
            timerAir = timerAirBase;

        }
    }

    public void Dano(int dano)
    {
        health -= dano;
    }



}
