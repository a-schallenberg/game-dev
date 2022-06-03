using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI.MenuHandling.Menus
{
    public class DeathMenu : MonoBehaviour, IMenu
    {
        public static DeathMenu Instance { get; private set; }

        public DeathMenu()
        {
            Instance = this;
        }

        #region Buttons

        public void OnMainMenuButtonClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }

        #endregion

        #region IMenu

        [Obsolete(IMenu.EnableObsoleteMessage, true)]
        public void Enable()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        [Obsolete(IMenu.DisableObsoleteMessage, true)]
        public void Disable()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        #endregion
    }
}
