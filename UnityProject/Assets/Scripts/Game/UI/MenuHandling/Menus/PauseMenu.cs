using System;
using UnityEngine;

namespace Game.UI.MenuHandling.Menus {
	public class PauseMenu : MonoBehaviour, IMenu {
		[SerializeField]
		private GameObject pauseMenu;

		public PauseMenu() {
			Instance = this;
		}

		public static PauseMenu Instance { get; private set; }
		public static bool      IsPaused { get; private set; }

		public void PauseGame() {
			pauseMenu.SetActive(true);
			Time.timeScale = 0f;
			IsPaused       = true;
			transform.GetChild(0).gameObject.SetActive(true);
		}

		public void ResumeGame() {
			pauseMenu.SetActive(false);
			Time.timeScale = 1f;
			IsPaused       = false;
			transform.GetChild(1).gameObject.SetActive(false);
		}

		public void QuitGame() {
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
}