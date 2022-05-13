using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] 
    private GameObject pauseMenu;
    
    public static bool IsPaused { get; private set; }

    private GameInputActions.GameActions _gameActions;
    private GameInputActions.PauseMenuActions _pauseMenuActions;

    private void Awake()
    {
        _gameActions = Util.InputAction.Game;
        _pauseMenuActions = Util.InputAction.PauseMenu;

        _gameActions.Pause.performed += _ =>
        {
            if (!IsPaused) PauseGame();
        };
        
        _pauseMenuActions.Resume.performed += _ =>
        {
            if (IsPaused) ResumeGame();
        };
        _gameActions.Enable();
        _pauseMenuActions.Disable();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        _gameActions.Disable();
        _pauseMenuActions.Enable();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        _gameActions.Enable();
        _pauseMenuActions.Disable();
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    
    public void GoToPauseMenu()
    {
        SceneManager.LoadScene("PauseMenuUI");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
