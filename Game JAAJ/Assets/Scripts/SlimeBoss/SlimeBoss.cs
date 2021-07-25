using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    [Header("Idle")]
    [SerializeField] private bool idle;

    [Header("Spawn")]
    [SerializeField] private bool spawnEnemy;
    public GameObject SlimePrefab;
    public float timerSpawn;
    private float timerBase;
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
        SpawnFunction();
        JumpFunction();
    }

    void SpawnFunction()
    {
        if(spawnEnemy)
        {
            timerSpawn -= Time.deltaTime;

            if (timerSpawn <= 0)
            {
                GameObject slime = Instantiate(SlimePrefab, spawnPos.position, Quaternion.identity);
                timerSpawn = timerBase;
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
