using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildMenu : MonoBehaviour, IMenu {
	public static BuildMenu Instance { get; private set; }

	[SerializeField] private Button                button;
	[SerializeField] private RectTransform         foundationView;
	[SerializeField] private HorizontalLayoutGroup group;

	private readonly Dictionary<Structure, FoundationItemSlot> _items = new();

	public BuildMenu() {
		Instance = this;
	}

	public void Disable(bool hard) {
		if (!hard && StructureHandler.Instance.IsInPlacing()) {
			return;
		}
		
		MenuHandler.DisableMenu();
	}

	#region Foundations

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

	public bool AddFoundationItem(Building building) {
		if (!_items.ContainsKey(building)) {
			_items.Add(building, new FoundationItemSlot(button, building, foundationView));
		}
		
		UpdateFoundationViewSize();
		return _items[building].Add();
	}

	#endregion

	#region Buttons

	public void OnBackButtonPressed() {
		MenuHandler.DisableMenu();
	}

	#endregion
	
	#region IMenu
	
	[Obsolete(IMenu.EnableObsoleteMessage, true)]
	public void Enable() {
		gameObject.GetComponent<ActivityToggle>().SetActivity(true);

		InputActions.Building.Enable();
		InputActions.Game.Movement.Enable();
	}

	[Obsolete(IMenu.DisableObsoleteMessage, true)]
	public void Disable() {
		InputActions.Building.Disable();
		InputActions.Game.Movement.Disable();
		
		if (StructureHandler.Instance.IsInPlacing()) {
			StructureHandler.Instance.StopPlacing();
		}
		
		gameObject.GetComponent<ActivityToggle>().SetActivity(false);
	}

	#endregion
}