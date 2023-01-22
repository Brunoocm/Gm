using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.InputSystem;
public class PersonagemFala : MonoBehaviour
{
    public InputBinding.DisplayStringOptions displayStringOptions;
    public TextMeshProUGUI display;
    public TextMeshProUGUI tutorialPass;
    public string[] sentences;

    private int index;

    public GameObject falaPersonagem;

    public UnityEvent m_MyEvent;

    public UnityEvent acaba; 

    public static bool finishTutorial;

    private bool oneTime;
    private PlayerInput playerInput;
    private Movement movement;
    void Start()
    {
        movement = FindObjectOfType<Movement>();
        playerInput = GetComponent<PlayerInput>();

        if (!finishTutorial)
        {
            movement.canMove = false;
            m_MyEvent.Invoke();

            falaPersonagem.SetActive(true);

            StartCoroutine(Type());
        }

    }

    void Update()
    {
        tutorialPass.text = "Aperte " + playerInput.actions["Interact"].GetBindingDisplayString(displayStringOptions);
        if (!finishTutorial)
        {
            if (display.text == sentences[index])
            {
                if (playerInput.actions["Interact"].triggered)
                {
                    NextSentence();
                }
                if(index == 0 && !oneTime)
                {
                    StartCoroutine(movement.JumpTutorial());
                    oneTime = true;
                }

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
                movement.canMove = true;
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

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            falaPersonagem.SetActive(false);
        }
    }
}
