using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;

    public bool nao;
    private void Update()
    {
        if (!nao)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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

    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Play()
    {
        StartCoroutine(delay());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
