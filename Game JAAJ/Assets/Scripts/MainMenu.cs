using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }
}
