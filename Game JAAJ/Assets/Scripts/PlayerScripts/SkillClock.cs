using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using TMPro;

public class SkillClock : MonoBehaviour
{
    public float speedPonteiro;

    public static bool skillPrimavera, skillOutono, skillInverno;

    [Header("Animacao")]
    public Animator playerAnim;
    public RuntimeAnimatorController Primavera;
    public RuntimeAnimatorController Outono;
    public RuntimeAnimatorController Inverno;

    [Header("icone")]
    public GameObject iconeImagePrimavera;
    public GameObject iconeImageOutono;
    public GameObject iconeImageInverno;


    [SerializeField]private float timerPonteiro;

    Transform ponteiro;

    void Start()
    {
        
        ponteiro = GameObject.Find("PonteiroPivot").GetComponent<Transform>();
        skillOutono = true;
    }

    void Update()
    {
        PonteiroUpdate();

        //Ponteiro.transform.Rotate(0, 0, speedPonteiro);

    }

    void PonteiroUpdate()
    {
        ponteiro.eulerAngles = new Vector3(0, 0, -timerPonteiro);

        if (timerPonteiro >= 360)
        {
            timerPonteiro = 0;
        }
        else
        {
            timerPonteiro += speedPonteiro;
        }

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
            if (timerPonteiro <= 120 && skillPrimavera  ) 
            {
                skillPrimavera = false;
                skillOutono = true;
                skillInverno = false;

                iconeImageOutono.SetActive(true);
                iconeImagePrimavera.SetActive(false);
                iconeImageInverno.SetActive(false);

                 playerAnim.runtimeAnimatorController = Outono;

            }
            else if (timerPonteiro <= 240 && timerPonteiro >= 120 && skillOutono  ) 
            {

                skillPrimavera = false;
                skillOutono = false;
                skillInverno = true;

                iconeImageInverno.SetActive(true);
                iconeImagePrimavera.SetActive(false);
                iconeImageOutono.SetActive(false);

                playerAnim.runtimeAnimatorController = Inverno;
        }
            else if (timerPonteiro <= 360 && timerPonteiro >= 240 && skillInverno  )
            {
                skillPrimavera = true;
                skillOutono = false;
                skillInverno = false;

                iconeImagePrimavera.SetActive(true);
                iconeImageOutono.SetActive(false);
                iconeImageInverno.SetActive(false);

                
                playerAnim.runtimeAnimatorController = Primavera;

            }
        //}       
    }
}
