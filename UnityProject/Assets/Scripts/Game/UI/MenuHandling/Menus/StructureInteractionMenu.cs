using System;
using Game.Structures;
using TMPro;
using UnityEngine;

namespace Game.UI.MenuHandling.Menus {
	public class StructureInteractionMenu : MonoBehaviour, IMenu {
		[SerializeField] private TextMeshProUGUI structureName;
		[SerializeField] private Transform       structureSpecificPanel;

		private Building  _currentBuilding;
		private Transform _currentStructurePanel;

		public StructureInteractionMenu() {
			Instance = this;
		}

		public static StructureInteractionMenu Instance { get; private set; }

		private void Reset() {
			_currentBuilding       = null;
			_currentStructurePanel = null;
		}

		public void Enable(Building building) {
			_currentBuilding = building;
			MenuHandler.EnableMenu(this);
		}

		public void OnExitClicked() {
			MenuHandler.DisableMenu();
			Reset();
		}

		public void OnMoveClicked() {
			try {
				MenuHandler.EnableMenu(BuildMenu.Instance);
				_currentBuilding.Move();
				Reset();
			} catch (NullReferenceException) {}
		}

		public void OnRemoveClicked() {
			try {
				MenuHandler.DisableMenu();
				_currentBuilding.Remove();
				Reset();
			} catch (NullReferenceException) {}
		}

		#region IMenu

		[Obsolete(IMenu.EnableObsoleteMessage, true)]
		public void Enable() {
			if (_currentBuilding == null) {
				return;
			}

			_currentBuilding.OnMenuEnable();
			InputActions.SIMenu.Enable();

			_currentStructurePanel = Instantiate(_currentBuilding.MenuPanel, structureSpecificPanel);
			structureName.text     = _currentBuilding.BuildingName;

			gameObject.SetActive(true);
		}

		[Obsolete(IMenu.DisableObsoleteMessage, true)]
		public void Disable() {
			_currentBuilding.OnMenuDisable();
			InputActions.SIMenu.Disable();

			gameObject.SetActive(false);

			Destroy(_currentStructurePanel.gameObject);
		}

		#endregion
	}
}