using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class FoundationItemSlot {
	private const int SlotCapacity = 100;

	private readonly Button    _buttonPrefab;
	private readonly Structure _structurePrefab;

	private readonly Button _button;

	public int NumberOfStructures { get; private set; }

	public FoundationItemSlot(Button buttonPrefab, Structure structurePrefab, Transform parent) {
		_structurePrefab = structurePrefab;

		_button              = Object.Instantiate(buttonPrefab, parent);
		_button.image.sprite = _structurePrefab.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
		_button.onClick.AddListener(() => StructureHandler.Instance.OnInstantiateButtonClicked(_structurePrefab.gameObject));
	}

	#region Slot Functions

	public bool Add(int number = 1) {
		var num = NumberOfStructures + number;
		
		if (num > SlotCapacity) {
			return false;
		}
		
		NumberOfStructures = num;
		UpdateButtonText();
		return true;
	}

	public bool Remove(int number = 1) {
		var num = NumberOfStructures - number;
		
		if (num < 0) {
			return false;
		}
		
		NumberOfStructures = num;
		UpdateButtonText();
		return true;
	}

	public bool IsEmpty() {
		return NumberOfStructures <= 0;
	}

	public bool IsFull() {
		return NumberOfStructures >= SlotCapacity;
	}

	#endregion

	public void DestroyButton() {
		Object.Destroy(_button.gameObject);
	}

	private void UpdateButtonText() {
		_button.GetComponentInChildren<Text>().text = NumberOfStructures.ToString();
	}
}