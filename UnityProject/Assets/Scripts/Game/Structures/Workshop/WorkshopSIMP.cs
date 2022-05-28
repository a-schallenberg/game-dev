using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Structures.Workshop {
	public class WorkshopSIMP : MonoBehaviour {
		[SerializeField] private Transform      group;
		[SerializeField] private Button         buttonPrefab;
		[SerializeField] private List<Building> prefabs;

		private readonly Dictionary<Button, Building> _buttons = new();

		private void OnEnable() {
			Init();
		}

		private void Init() {
			foreach (var building in prefabs) {
				var button = Instantiate(buttonPrefab, group);
				button.image.sprite = building.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
				button.GetComponent<WorkshopButton>().Init(building);
			}
		}
	}
}