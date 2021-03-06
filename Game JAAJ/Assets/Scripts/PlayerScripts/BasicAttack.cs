using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject Bullet;

    public Animator anim;
    public Transform shootPos;
    void Start()
    {

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

        FindObjectOfType<ScriptAudioManager>().Play("estilingue"); 

        anim.SetTrigger("Shoot");
    }
}
