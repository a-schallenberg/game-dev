using System;
using UnityEngine;

namespace Game.UI.MenuHandling.Menus {
	/// <summary>
	///     This is a standard menu to display the normal gameplay, which takes place outside of all other menus, as a menu.
	/// </summary>
	public class DefaultMenu : MonoBehaviour, IMenu {
		public DefaultMenu() {
			Instance = this;
		}

		public static DefaultMenu Instance { get; private set; }

		#region Buttons

		public void OnBuildMenuButtonPressed() {
			if (MenuHandler.IsActiveMenu(this)) {
				MenuHandler.EnableMenu(BuildMenu.Instance);
			} else if (MenuHandler.IsActiveMenu(BuildMenu.Instance)) {
				MenuHandler.DisableMenu();
			}
		}

		public void OnQuestsButtonPressed() {}

		public void OnPauseMenuPressed() {
			MenuHandler.EnableMenu(PauseMenu.Instance, false);
		}

		#endregion

		#region IMenu

		[Obsolete(IMenu.EnableObsoleteMessage, true)]
		public void Enable() {
			foreach (var child in gameObject.GetComponentsInChildren<ActivityToggle>(true)) {
				child.SetActivity(true);
			}

			InputActions.Game.Enable();
		}

		[Obsolete(IMenu.DisableObsoleteMessage, true)]
		public void Disable() {
			foreach (var child in gameObject.GetComponentsInChildren<ActivityToggle>()) {
				child.SetActivity(false);
			}

			InputActions.Game.Disable();
		}

		#endregion
	}
}