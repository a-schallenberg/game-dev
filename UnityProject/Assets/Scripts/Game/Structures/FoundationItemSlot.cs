using UnityEngine;
using UnityEngine.UI;

namespace Game.Structures {
	public class FoundationItemSlot {
		private const int SlotCapacity = 100;

		private readonly Building _buildingPrefab;

		private readonly Button _button;

		private readonly Button _buttonPrefab;

		public FoundationItemSlot(Button buttonPrefab, Building buildingPrefab, Transform parent) {
			_buildingPrefab = buildingPrefab;

			_button              = Object.Instantiate(buttonPrefab, parent);
			_button.image.sprite = _buildingPrefab.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
			_button.onClick.AddListener(() => StructureHandler.Instance.OnInstantiateButtonClicked(_buildingPrefab.gameObject));
		}

		public int NumberOfStructures { get; private set; }

		public void DestroyButton() {
			Object.Destroy(_button.gameObject);
		}

		private void UpdateButtonText() {
			_button.GetComponentInChildren<Text>().text = NumberOfStructures.ToString();
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
	}
}