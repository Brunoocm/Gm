using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    [Header("Idle")]
    [SerializeField] private bool idle;
    public float timerBetweenSet;

    private float timerBetween;
    private int count;

    [Header("Spawn")]
    [SerializeField] private bool spawnEnemy;
    public int slimesPerScene;
    public float timerSpawn;
    public GameObject SlimePrefab;

    private float timerBase;
    private int numSlimes;
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


    }

    void Update()
    {
        ChangeStages();

        IdleFunction();
        SpawnFunction();
        JumpFunction();

        ArraySlimes = GameObject.FindGameObjectsWithTag("MiniSlime");
        for (int i = 0; i < ArraySlimes.Length; i++)
        {
            numSlimes = i;
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

                timerBetween = 0;
            }
        }
        //else if(spawnEnemy)
        //{
        //    if (timerBetween >= 10)
        //    {
        //        spawnEnemy = false;
        //        jumpAttack = true;

        //        timerBetween = 0;
        //    }
        //}
        //else if(jumpAttack)
        //{
        //    if (timerBetween >= 10)
        //    {
        //        idle = true;
        //        jumpAttack = false;

        //        timerBetween = 0;
        //    }
        //}


    }

    void IdleFunction()
    {

    }

    void SpawnFunction()
    {
        if(spawnEnemy)
        {
            timerSpawn -= Time.deltaTime;

            if (timerSpawn <= 0 && numSlimes < slimesPerScene)
            {
                GameObject slime = Instantiate(SlimePrefab, spawnPos.position, Quaternion.identity);
                
                timerSpawn = timerBase;
            }
            if(numSlimes >= slimesPerScene)
            {
                spawnEnemy = false;
                jumpAttack = true;

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

                jumpAttack = false;
                idle = true;
            }
        }
        else
        {
            anim.SetBool("Attack", false);
            timerAir = timerAirBase;
            print("oi");
        }
    }
}
