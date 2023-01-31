using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationVelocity : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("idle", 0, Random.Range(0, 700f));
        anim.speed = Random.Range(0.5f , 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
