using TMPro;
using UnityEngine;

namespace Game.UI {
	
	/// <summary>
	/// <c>MonoBehavior</c> for the <c>GameObject</c> CostBar in the Overlay placed under the <c>ResourceBar</c>.
	/// </summary>
	public class CostBar : MonoBehaviour {
		public static CostBar Instance { get; private set; }
		
		/// <summary>
		/// Textfield for the deduction of wood 
		/// </summary>
		[SerializeField] private TextMeshProUGUI woodText;

		/// <summary>
		/// Textfield for the deduction of stone 
		/// </summary>
		[SerializeField] private TextMeshProUGUI stoneText;

		/// <summary>
		/// Textfield for the deduction of iron 
		/// </summary>
		[SerializeField] private TextMeshProUGUI ironText;


		public CostBar() {
			Instance = this;
		}

		/// <summary>
		/// Enables the CostBar with the following values.
		/// </summary>
		/// <param name="wood">Deduction of wood</param>
		/// <param name="stone">Deduction of stone</param>
		/// <param name="iron">Deduction of iron</param>
		public void Enable(int wood, int stone, int iron) {
			woodText.text  = wood == 0 ? "" : wood.ToString();
			stoneText.text = stone == 0 ? "" : stone.ToString();
			ironText.text  = iron == 0 ? "" : iron.ToString();

			gameObject.SetActive(true);
		}

		/// <summary>
		/// Disables the CostBar with the following values.
		/// </summary>
		public void Disable() {
			gameObject.SetActive(false);
		}
	}
}