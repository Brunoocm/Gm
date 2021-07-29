using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using TMPro;

public class SkillClock : MonoBehaviour
{
    public float speedPonteiro;
    public TextMeshProUGUI name;

    public static bool skillPrimavera, skillOutono, skillInverno; 

    private float timerPonteiro;

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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (timerPonteiro <= 120 && skillInverno) 
            {
                skillPrimavera = true; 
                skillOutono = false; 
                skillInverno = false;

                name.text = "Primavera";

                print("primavera");
            }
            else if (timerPonteiro <= 240 && timerPonteiro >= 120 && skillPrimavera) 
            {
                skillPrimavera = false;
                skillOutono = true;
                skillInverno = false;

                name.text = "Outono";

                print("outono");
            }
            else if (timerPonteiro <= 360 && timerPonteiro >= 240 && skillOutono)
            {
                skillPrimavera = false;
                skillOutono = false;
                skillInverno = true;

                name.text = "Inverno";

                print("inverno");
            }
        }       
    }
}
