using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Menu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;

    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        if (playerInput.actions["Pause"].triggered)
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
