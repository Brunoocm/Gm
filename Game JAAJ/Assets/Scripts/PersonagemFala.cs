using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class PersonagemFala : MonoBehaviour
{
    public TextMeshProUGUI display;
    public string[] sentences;
    private int index;

    public GameObject falaPersonagem;

    public UnityEvent m_MyEvent;

    public UnityEvent acaba; 

    public UnityEvent atacar;

    public static bool finishTutorial;
    void Start()
    {
        if (!finishTutorial)
        {
            m_MyEvent.Invoke();

            falaPersonagem.SetActive(true);

            StartCoroutine(Type());
        }

    }

    void Update()
    {
        if (!finishTutorial)
        {
            if (display.text == sentences[index])
            {

                //if (index == 2 && SkillClock.skillInverno)
                //{
                //    NextSentence();
                //}
                if (index == 4)
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        NextSentence();
                        atacar.Invoke();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        NextSentence();
                    }
                }
                //else if (index > 4)
                //{
                //    if (Input.GetKeyDown(KeyCode.X))
                //    {
                //        NextSentence();
                //    }
                //}
            }
        }
    }

    void NextSentence()
    {
        if (!finishTutorial)
        {
            if (index < sentences.Length - 1)
            {
                index++;
                display.text = "";
                StartCoroutine(Type());
            }
            else
            {
                display.text = "";
                acaba.Invoke();
                finishTutorial = true;
            }
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            display.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            NextSentence();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            falaPersonagem.SetActive(false);
        }
    }
}
