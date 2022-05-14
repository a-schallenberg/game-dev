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
	}

	public void Disable() {
		gameObject.SetActive(false);
		_currentStructure.OnMenuDisabled();

		Destroy(_currentStructurePanel.gameObject);

		_currentStructure      = null;
		_currentStructurePanel = null;
	}

	public void OnMoveClicked() {
		try {
			_currentStructure.Move();
			Disable();
		} catch (NullReferenceException) { }
	}
	
	public void OnRemoveClicked() {
		try {
			_currentStructure.Remove();
			Disable();
		} catch (NullReferenceException) { }
	}
}