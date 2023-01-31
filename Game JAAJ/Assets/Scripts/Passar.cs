using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Passar : MonoBehaviour
{
    public string bossRoom;

    public GameObject SalaEsquerda;
    public GameObject SalaMeio;  
    public GameObject SalaDireita;

    public GameObject LuzEsquerda;
    public GameObject LuzDireita;
    public GameObject Hatch;
    public GameObject HatchAberto;

    public Camera cam;

    public GameObject paredes;

    public static bool primeiroBoss; 
    public static bool segundoBoss; 
    public static bool terceiroBoss; 

    void Start()
    {
     
    }

    void Update()
    {
        if(primeiroBoss)
        {
            LuzEsquerda.SetActive(true);
        }
        if(segundoBoss)
        {
            LuzDireita.SetActive(true);
        }
        if(segundoBoss && primeiroBoss)
        {
            Hatch.SetActive(false);
            HatchAberto.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "EsquerdaCollider")
        {
            cam.transform.position = new Vector3 (SalaEsquerda.transform.position.x, SalaEsquerda.transform.position.y, cam.transform.position.z);
            if(!primeiroBoss)
            {
                paredes.SetActive(true);               
                //HealthSlime.SetActive(true);
            }
            SalaEsquerda.SetActive(true);
        }
        else if(other.name == "MeioCollider")
        {
            cam.transform.position = new Vector3(SalaMeio.transform.position.x, SalaMeio.transform.position.y, cam.transform.position.z);

        }
        else if(other.name == "DireitaCollider")
        {
            cam.transform.position = new Vector3(SalaDireita.transform.position.x, SalaDireita.transform.position.y, cam.transform.position.z);
            if (!segundoBoss)
            {
                paredes.SetActive(true);
            }
            SalaDireita.SetActive(true);
        }
        else if(other.name == "HatchTrigger")
        {
            SceneManager.LoadScene(bossRoom);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "EsquerdaCollider")
        {
            if (primeiroBoss)
            {
                paredes.SetActive(false);
            }
        }
        if (other.name == "DireitaCollider")
        {
            if (segundoBoss)
            {
                paredes.SetActive(false);
            }
        }
    }
}
