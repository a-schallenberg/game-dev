using Game.Resources;
using Game.UI;
using Game.UI.MenuHandling.Menus;
using UnityEngine;

namespace Game.Structures.Workshop {
	public class WorkshopButton : MonoBehaviour {
		private Building _prefab;

		public void Init(Building prefab) {
			_prefab = prefab;
		}

		public void OnHoverEnter() {
			CostBar.Instance.Enable(-_prefab.Costs.wood, -_prefab.Costs.stone, -_prefab.Costs.iron);
		}

		public void OnHoverExit() {
			CostBar.Instance.Disable();
		}

		public void OnClick() {
			if (!ResourceHandler.UseResources(_prefab.Costs)) {
				return;
			}

			BuildMenu.Instance.AddFoundationItem(_prefab);
		}
	}
}