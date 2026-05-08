using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuButton : MonoBehaviour
{
    // PANEL activation is managed by the UI Manager
    // Below, there is only methods for buttons behaviour 

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
