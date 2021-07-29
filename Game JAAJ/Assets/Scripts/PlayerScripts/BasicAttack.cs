using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject Bullet;

    Animator anim;
    [HideInInspector] public Transform shootPos;
    void Start()
    {
        shootPos = GameObject.Find("ShootPos").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject d = Instantiate(Bullet, shootPos.position, Quaternion.identity);
        d.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;

        anim.SetTrigger("Shoot");
    }
}
