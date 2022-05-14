using System;
using TMPro;
using UnityEngine;

public class StructureInteractionMenu : MonoBehaviour {
	public static StructureInteractionMenu Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI structureName;
	[SerializeField] private Transform       structureSpecificPanel;

	private Structure _currentStructure;
	private Transform _currentStructurePanel;

	public StructureInteractionMenu() {
		Instance = this;
	}

	public void Enable(Structure structure) {
		_currentStructure      = structure;
		_currentStructurePanel = Instantiate(structure.MenuPanel, structureSpecificPanel);

		structureName.text = structure.StructureName;

		gameObject.SetActive(true);
		structure.OnMenuEnabled();
		
		InputActions.DisableAll();
		InputActions.SIMenu.Enable();
	}

	
	public void Disable() {
		Disable(() => {
					InputActions.Game.Enable();
					Reset();
				});
	}
	private void Disable(Action action) {
		gameObject.SetActive(false);
		_currentStructure.OnMenuDisabled();

		Destroy(_currentStructurePanel.gameObject);

		InputActions.DisableAll();
		action.Invoke();
	}

	public void OnMoveClicked() {
		try {
			Disable(() => {
						InputActions.Building.Enable();
						InputActions.Game.Movement.Enable();
					});
			_currentStructure.Move();
			Reset();
		} catch (NullReferenceException) { }
	}
	
	public void OnRemoveClicked() {
		try {
			_currentStructure.Remove();
			Disable();
		} catch (NullReferenceException) { }
	}

	private void Reset() {
		_currentStructure      = null;
		_currentStructurePanel = null;
	}
}