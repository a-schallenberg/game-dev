using System.Linq;
using Game.Resources;
using TMPro;

namespace Game.Structures.Warehouse {
	public class WarehouseScript : Building {
		public override void OnMenuEnable() {
			var woodAmount  = ResourceHandler.Resources[ResourceType.Wood].Amount;
			var woodLimit   = ResourceHandler.Resources[ResourceType.Wood].Limit;
			var stoneAmount = ResourceHandler.Resources[ResourceType.Stone].Amount;
			var stoneLimit  = ResourceHandler.Resources[ResourceType.Stone].Limit;
			var ironAmount  = ResourceHandler.Resources[ResourceType.Iron].Amount;
			var ironLimit   = ResourceHandler.Resources[ResourceType.Iron].Limit;

			GetTextByName("Wood").text  = $"{woodAmount} / {woodLimit}";
			GetTextByName("Stone").text = $"{stoneAmount} / {stoneLimit}";
			GetTextByName("Iron").text  = $"{ironAmount} / {ironLimit}";
		}

		private TextMeshProUGUI GetTextByName(string name) {
			return MenuPanel.gameObject.GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(text => text.name == name);
		}

		#region Operators

		public override bool Equals(object obj) {
			return obj != null && obj.GetType() == typeof(WarehouseScript) && ID == ((WarehouseScript) obj).ID;
		}

		public override int GetHashCode() {
			return ID.GetHashCode();
		}

		#endregion
	}
}