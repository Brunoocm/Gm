using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int health;

    [Header("BOSS")]
    public bool isBoss;
    public Slider slide;

    void Start()
    {
        if(isBoss)
        {
            slide.maxValue = health;
            slide.value = health;
        }
    }

    void Update()
    {

    }

    public void Dano(int dano)
    {
        health -= dano;
        if (isBoss)
        {
            slide.value = health;
        }
    }
}
