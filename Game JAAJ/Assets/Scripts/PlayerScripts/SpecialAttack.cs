using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float powerSpeed, powerCooldown;
    public GameObject autumnPower, winterPower, springPower;

    float cooldownTime;
    Animator anim;
    GameObject seasonPower;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(cooldownTime >= powerCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Shoot();
            }
        }
        else
        {
            cooldownTime += Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject b = Instantiate(SeasonPower(), GetComponent<BasicAttack>().shootPos.position, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().velocity = transform.right * powerSpeed;


        anim.SetTrigger("Power");

        cooldownTime = 0;
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
