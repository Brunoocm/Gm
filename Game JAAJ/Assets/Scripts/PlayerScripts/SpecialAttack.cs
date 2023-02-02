using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecialAttack : MonoBehaviour
{
    public float cdReload;
    public bool canAttack;

    public Transform shootPos;
    public GameObject autumnPower, winterPower, springPower;
    public ParticleSystem particle;

    private float cdTimer;
    Animator anim;
    GameObject seasonPower;
    private PlayerInput playerInput;

    private void Awake()
    {
        canAttack = true;
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canAttack)
            return;

        cdTimer += Time.deltaTime;
        if (playerInput.actions["Shoot"].IsPressed())
        {
            if (cdTimer >= cdReload)
            {
                Shoot();
                cdTimer = 0;
            }
        }
    }

    public void Shoot()
    {
        GameObject b = Instantiate(SeasonPower(), shootPos.position, shootPos.rotation);
        float bSpeed = b.GetComponent<BulletScript>().speed;
        float bAngle = b.GetComponent<BulletScript>().angle;
        b.transform.rotation = Quaternion.AngleAxis(bAngle, Vector3.forward);
        //b.GetComponent<Rigidbody2D>().velocity = transform.right * bSpeed;
        b.GetComponent<Rigidbody2D>().velocity = (transform.right * bSpeed) + (transform.up * bAngle);
        b.transform.right = transform.right.normalized;
        anim.SetTrigger("Shoot");
    }

    GameObject SeasonPower()
    {
        if (SkillClock.skillInverno)
        {
            return winterPower;
        }
        else if (SkillClock.skillOutono)
        {
            return autumnPower;
        }
        else if (SkillClock.skillPrimavera)
        {
            return springPower;
        }

        return null;
    }
}
