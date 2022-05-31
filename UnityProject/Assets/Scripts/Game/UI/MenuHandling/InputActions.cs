using System;
using Game.Structures;
using Game.UI.MenuHandling.Menus;
using UnityEditor;
using UnityEngine;

namespace Game.UI.MenuHandling {
	public static class InputActions {
		public static readonly GameInputActions InputAction = new();

		public static GameInputActions.BuildMenuActions Building  = InputAction.BuildMenu;
		public static GameInputActions.GameActions      Game      = InputAction.Game;
		public static GameInputActions.PauseMenuActions PauseMenu = InputAction.PauseMenu;
		public static GameInputActions.SIMenuActions    SIMenu    = InputAction.SIMenu;

		static InputActions() {
			RegisterActions();
			DisableAll();
			Game.Enable();
		}

		public static void Load() {}

		private static void RegisterActions() {
			Game.Movement.performed    += ctx => SGame.MovePerformed(ctx.ReadValue<Vector2>());
			Game.Movement.canceled     += _ => SGame.MoveCanceled();
			Game.BuildMenu.performed   += _ => SGame.BuildMenu();
			Game.Interaction.performed += _ => SGame.Interact();
			Game.Pause.performed       += _ => SGame.Pause();

			Building.Submit.performed        += _ => SBuilding.Submit();
			Building.Cancel.performed        += _ => SBuilding.Cancel();
			Building.MousePosition.performed += ctx => SBuilding.MousePosition(ctx.ReadValue<Vector2>());
			Building.Close.performed         += _ => SBuilding.Close();
			Building.HardClose.performed     += _ => SBuilding.HardClose();

			PauseMenu.Resume.performed += _ => SPauseMenu.Resume();

			SIMenu.Close.performed += _ => SSIMenu.Close();
		}

		private static void DisableAll() {
			Building.Disable();
			Game.Disable();
			PauseMenu.Disable();
			SIMenu.Disable();
		}
	}

	public struct SGame {
		public static void MovePerformed(Vector2 vec) {
			PlayerScript.Instance.Move(vec);
		}

		public static void MoveCanceled() {
			PlayerScript.Instance.Move(Vector2.zero);
		}

		public static void BuildMenu() {
			MenuHandler.EnableMenu(Menus.BuildMenu.Instance);
		}

		public static void Interact() {
			PlayerScript.Instance.Interact();
		}

		public static void Pause() {
			MenuHandler.EnableMenu(PauseMenu.Instance, false);
		}
	}

	public struct SBuilding {
		public static void Submit() {
			StructureHandler.Instance.Submit();
		}

		public static void Cancel() {
			StructureHandler.Instance.Cancel();
		}

		public static void MousePosition(Vector2 vec) {
			StructureHandler.Instance.MousePosition(vec);
		}

		public static void Close() {
			BuildMenu.Instance.Disable(false);
		}

		public static void HardClose() {
			BuildMenu.Instance.Disable(true);
		}
	}

	public struct SPauseMenu {
		public static void Resume() {
			MenuHandler.DisableMenu();
		}
	}

	public struct SSIMenu {
		public static void Close() {
			MenuHandler.DisableMenu();
		}
	}
}