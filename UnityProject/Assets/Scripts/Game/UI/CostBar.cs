using TMPro;
using UnityEngine;

namespace Game.UI {
	public class CostBar : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI woodText;
		[SerializeField] private TextMeshProUGUI stoneText;
		[SerializeField] private TextMeshProUGUI ironText;

		public CostBar() {
			Instance = this;
		}

		public static CostBar Instance { get; private set; }

		public void Enable(int wood, int stone, int iron) {
			woodText.text  = wood == 0 ? "" : wood.ToString();
			stoneText.text = stone == 0 ? "" : stone.ToString();
			ironText.text  = iron == 0 ? "" : iron.ToString();

			gameObject.SetActive(true);
		}

		public void Disable() {
			gameObject.SetActive(false);
		}
	}
}