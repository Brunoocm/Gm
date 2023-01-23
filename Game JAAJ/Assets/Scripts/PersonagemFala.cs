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

    [TextArea(7, 20)]
    public string[] sentences;

    private int index;

    public GameObject falaPersonagem;

    public UnityEvent m_MyEvent;

    public UnityEvent acaba; 

    public static bool finishTutorial;

    private bool playetAction;
    private PlayerInput playerInput;
    private SpecialAttack specialAttack;
    private Movement movement;
    private SkillClock skillClock;
    void Start()
    {
        skillClock = FindObjectOfType<SkillClock>();
        movement = FindObjectOfType<Movement>();
        specialAttack = FindObjectOfType<SpecialAttack>();
        playerInput = GetComponent<PlayerInput>();

        if (!finishTutorial)
        {
            movement.canMove = false;
            specialAttack.canAttack = false;
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
                if (playerInput.actions["Interact"].triggered && !playetAction)
                {
                    NextSentence();
                }
                if (index == 3)
                {
                    PrimaveraAbility();
                }
                if (index == 5)
                {
                    OutonoAbility();
                }
                if (index == 7)
                {
                    InvernoAbility();
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
                specialAttack.canAttack = true;
                acaba.Invoke();
                finishTutorial = true;
            }
        }
    }

    void PrimaveraAbility()
    {
        playetAction = true;
        skillClock.timerPonteiro = 300;

        if(playerInput.actions["Shoot"].triggered)
        {
            NextSentence();
            specialAttack.Shoot();
            playetAction = false;
        }
    } 
    void OutonoAbility()
    {
        playetAction = true;
        skillClock.timerPonteiro = 60;
        movement.Jump();

        if(movement.numJumps == 0)
        {
            NextSentence();
            skillClock.timerPonteiro = 180;
            playetAction = false;
        }
    }
    void InvernoAbility()
    {
        playetAction = true;
        skillClock.timerPonteiro = 180;

        if (playerInput.actions["Shoot"].triggered)
        {
            NextSentence();
            specialAttack.Shoot();
            playetAction = false;
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
