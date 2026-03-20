using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuButton : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private bool paused;
    private PlayerInputActions playerInputActions; 

    private void OnEnable()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pauseMenu.SetActive(false);
        playerInputActions.Player.Escape.performed += EscapeOnperformed;
    }

    public void EscapeOnperformed(InputAction.CallbackContext obj)
    {
        if (!paused)
        {
            paused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

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
