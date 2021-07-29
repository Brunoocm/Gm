using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float powerSpeed, powerCooldown;
    public GameObject autumnPower, winterPower, springPower;
    public ParticleSystem particle;

    private bool isParticle;
    float cooldownTime;
    Animator anim;
    GameObject seasonPower;

    void Start()
    {
        anim = GetComponent<Animator>();
        cooldownTime = powerCooldown;
    }

    void Update()
    {
        if(cooldownTime >= powerCooldown)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Shoot();
            }
            if (isParticle)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
                isParticle = false;
            }
        }
        else
        {
            cooldownTime += Time.deltaTime;
            isParticle = true;

        }
    }

    void Shoot()
    {
        GameObject b = Instantiate(SeasonPower(), GetComponent<BasicAttack>().shootPos.position, GetComponent<BasicAttack>().shootPos.rotation);
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
