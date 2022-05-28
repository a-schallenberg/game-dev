using System.Collections.Generic;
using Game.Structures;
using Game.UI.MenuHandling.Menus;
using UnityEngine;

namespace Game.Testing {
	public class FoundationTester : MonoBehaviour {
		[SerializeField] public List<Building> foundations;

		private void Awake() {
			LoadStartFoundations();
		}

		public void LoadStartFoundations() {
			foreach (var foundation in foundations) {
				BuildMenu.Instance.AddFoundationItem(foundation);
			}
		}
	}
}