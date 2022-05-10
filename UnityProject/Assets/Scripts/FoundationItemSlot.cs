using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class FoundationItemSlot {
	public const int SlotCapacity = 100;

	private readonly Button    _buttonPrefab;
	private readonly Structure _structurePrefab;

	private readonly Button _button;

	public int NumberOfStructures { get; private set; } // TODO show on Button

	public FoundationItemSlot(Button buttonPrefab, Structure structurePrefab, Transform parent) {
		_structurePrefab = structurePrefab;

		_button              = Object.Instantiate(buttonPrefab, parent);
		_button.image.sprite = _structurePrefab.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
		_button.onClick.AddListener(() => StructureHandler.Instance.OnInstantiateButtonClicked(_structurePrefab.gameObject));
	}

	public void Add(int number = 1) {
		NumberOfStructures = Math.Min(SlotCapacity, NumberOfStructures + number);
		UpdateButtonText();
	}

	public void Remove(int number = 1) {
		NumberOfStructures = Math.Max(0, NumberOfStructures - number);
		UpdateButtonText();
	}

	public bool IsEmpty() {
		return NumberOfStructures <= 0;
	}

	public bool IsFull() {
		return NumberOfStructures >= SlotCapacity;
	}

	public void DestroyButton() {
		Object.Destroy(_button.gameObject);
	}

	private void UpdateButtonText() {
		_button.GetComponentInChildren<Text>().text = NumberOfStructures.ToString();
	}
}