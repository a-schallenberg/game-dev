using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class StructureInteractionMenu : MonoBehaviour, IMenu {
	public static StructureInteractionMenu Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI structureName;
	[SerializeField] private Transform       structureSpecificPanel;

	private Building _currentBuilding;
	private Transform _currentStructurePanel;

	public StructureInteractionMenu() {
		Instance = this;
	}

	public void Enable(Building building) {
		
		_currentBuilding      = building;
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
		} catch (NullReferenceException) { }
	}
	
	public void OnRemoveClicked() {
		try {
			MenuHandler.DisableMenu();
			_currentBuilding.Remove();
			Reset();
		} catch (NullReferenceException) { }
	}

	private void Reset() {
		_currentBuilding      = null;
		_currentStructurePanel = null;
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
		structureName.text = _currentBuilding.BuildingName;

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