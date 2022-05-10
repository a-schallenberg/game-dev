using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class BuildMenuScript : MonoBehaviour {
	public static BuildMenuScript Instance { get; private set; }

	[SerializeField] private Button                button;
	[SerializeField] private RectTransform         foundationView;
	[SerializeField] private HorizontalLayoutGroup group;

	private Dictionary<Structure, FoundationItemSlot> _items = new();

	private void OnEnable() {
		UpdateFoundationSlots();
	}

	public void UpdateFoundationSlots() {
		_items = new();
		foreach (var foundation in PlayerScript.Instance.foundations) {
			if (!_items.ContainsKey(foundation)) {
				_items.Add(foundation, new FoundationItemSlot(button, foundation, foundationView));
			}
			_items[foundation].Add();
		}

		var buttonWidthSum = ((RectTransform) button.transform).rect.width * _items.Count;
		var width          = group.padding.left + group.padding.right + buttonWidthSum + (_items.Count - 1) * group.spacing;
		foundationView.sizeDelta = new Vector2(width, 0);
	}

	public void RemoveFoundationItem(Structure structure) {
		if (!_items.ContainsKey(structure)) {
			return;
		}
		
		_items[structure].Remove();
		PlayerScript.Instance.RemoveFoundation(structure);
		if (_items[structure].IsEmpty()) {
			_items[structure].DestroyButton();
			_items.Remove(structure);
			UpdateFoundationSlots();
		}
	}

	private void Awake() {
		Instance = this;
	}
}