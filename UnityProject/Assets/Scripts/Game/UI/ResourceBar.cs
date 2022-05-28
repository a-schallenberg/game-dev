using Game.Resources;
using TMPro;
using UnityEngine;

namespace Game.UI {
	public class ResourceBar : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI wood;
		[SerializeField] private TextMeshProUGUI stone;
		[SerializeField] private TextMeshProUGUI iron;

		public ResourceBar() {
			Instance = this;
		}

		public static ResourceBar Instance { get; private set; }

		public void SetResourceAmount(ResourceType type, int amount) {
			switch (type) {
				case ResourceType.Wood:
					wood.text = amount.ToString();
					break;
				case ResourceType.Stone:
					stone.text = amount.ToString();
					break;
				case ResourceType.Iron:
					iron.text = amount.ToString();
					break;
			}
		}
	}
}