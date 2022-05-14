using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class BuildMenuScript : MonoBehaviour {
	public static BuildMenuScript Instance { get; private set; }

	[SerializeField] private Button                button;
	[SerializeField] private RectTransform         foundationView;
	[SerializeField] private HorizontalLayoutGroup group;

	private readonly Dictionary<Structure, FoundationItemSlot> _items = new();

	public BuildMenuScript() {
		Instance = this;
	}

	public void OnBuildMenuButtonPressed() {
		if (gameObject.activeSelf) {
			Disable(true);
		} else {
			Enable();
		}
	}
	
	public void Enable() {
		gameObject.GetComponent<ActivityToggle>().SetActivity(true);
		
		InputActions.DisableAll();
		InputActions.Building.Enable();
		InputActions.Game.Movement.Enable();
	}

	public void Disable(bool hard) {
		if (StructureHandler.Instance.IsInPlacing()) {
			if (hard) {
				StructureHandler.Instance.StopPlacing();
			} else {
				return;
			}
		}
		
		gameObject.GetComponent<ActivityToggle>().SetActivity(false);
		
		InputActions.DisableAll();
		InputActions.Game.Enable();
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
}