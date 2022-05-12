using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildMenuScript : MonoBehaviour {
	public static BuildMenuScript Instance { get; private set; }

	[SerializeField] private Button                button;
	[SerializeField] private RectTransform         foundationView;
	[SerializeField] private HorizontalLayoutGroup group;

	private readonly Dictionary<Structure, FoundationItemSlot> _items = new();
	private          GameInputActions.BuildingActions          _building;

	private void OnEnable() {
		_building.Enable();
	}

	private void OnDisable() {
		_building.Disable();
	}

	private void UpdateFoundationViewSize() {
		var buttonWidthSum = ((RectTransform) button.transform).rect.width * _items.Count;
		var width          = group.padding.left + group.padding.right + buttonWidthSum + (_items.Count - 1) * group.spacing;
		foundationView.sizeDelta = new Vector2(width, 0);
	}

	public bool RemoveFoundationItem(Structure structure) {
		if (!_items.ContainsKey(structure)) {
			return false;
		}

		var success =_items[structure].Remove();
		if (_items[structure].IsEmpty()) {
			_items[structure].DestroyButton();
			_items.Remove(structure);
			UpdateFoundationViewSize();
		}

		UpdateFoundationViewSize();
		return success;
	}

	public bool AddFoundationItem(Structure structure) {
		if (!_items.ContainsKey(structure)) {
			_items.Add(structure, new FoundationItemSlot(button, structure, foundationView));
		}
		
		UpdateFoundationViewSize();
		return _items[structure].Add();
	}

	private void Awake() {
		Instance = this;
		PlayerScript.Instance.LoadStartFoundations();

		_building = Util.InputAction.Building;
		_building.Disable();

		_building.Submit.performed        += _ => StructureHandler.Instance.Submit();
		_building.Cancel.performed        += _ => StructureHandler.Instance.Cancel();
		_building.MousePosition.performed += context => StructureHandler.Instance.MousePosition(context.ReadValue<Vector2>());
	}
}