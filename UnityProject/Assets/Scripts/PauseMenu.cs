using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static PauseMenu Instance { get; private set; }
    public static bool      IsPaused { get; private set; }

    [SerializeField] 
    private GameObject pauseMenu;
    
    public PauseMenu() {
        Instance = this;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        InputActions.DisableAll();
        InputActions.PauseMenu.Enable();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        InputActions.DisableAll();
        InputActions.Game.Enable();
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
