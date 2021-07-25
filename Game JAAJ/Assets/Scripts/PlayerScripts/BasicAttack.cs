using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject Bullet;

    Transform shootPos;
    void Start()
    {
        shootPos = GameObject.Find("ShootPos").GetComponent<Transform>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {

            GameObject d = Instantiate(Bullet, shootPos.position, Quaternion.identity);
            d.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;

        }
    }
}
