using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BasicAttack : MonoBehaviour
{
    public float bulletSpeed;
    public float cdReload;
    public bool canAttack;
    public GameObject Bullet;
    public Animator anim;
    public Transform shootPos;

    private float cdTimer;
    private PlayerInput playerInput;

    private void Awake()
    {
        canAttack = true;
    }
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (!canAttack)
            return;

        cdTimer += Time.deltaTime;
        if (playerInput.actions["Shoot"].triggered)
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
        GameObject d = Instantiate(Bullet, shootPos.position, Quaternion.identity);
        d.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;

        FindObjectOfType<ScriptAudioManager>().Play("estilingue"); 

        anim.SetTrigger("Shoot");
    }
}
