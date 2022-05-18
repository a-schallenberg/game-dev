using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour, IMenu {
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
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
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

    public void OnResumeButtonPressed() {
        MenuHandler.DisableMenu();
    }
    
    #region IMenu

    [Obsolete(IMenu.EnableObsoleteMessage, true)]
    public void Enable() {
        InputActions.PauseMenu.Enable();
        PauseGame();
    }

    [Obsolete(IMenu.DisableObsoleteMessage, true)]
    public void Disable() {
        InputActions.PauseMenu.Disable();
        ResumeGame();
    }

    #endregion
}
