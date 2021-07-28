using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class SkillClock : MonoBehaviour
{
    public float speedPonteiro;
    public static bool skillPrimavera, skillOutono, skillInverno; 

    private float timerPonteiro;

    Transform ponteiro;
    void Start()
    {
        ponteiro = GameObject.Find("PonteiroPivot").GetComponent<Transform>();
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (timerPonteiro <= 120) 
            {
                skillPrimavera = true; 
                skillOutono = false; 
                skillInverno = false;

            }
            else if (timerPonteiro <= 240) 
            {
                skillPrimavera = false;
                skillOutono = true;
                skillInverno = false;



            }
            else if (timerPonteiro <= 360)
            {
                skillPrimavera = false;
                skillOutono = false;
                skillInverno = true;


            }
        }       
    }
}
