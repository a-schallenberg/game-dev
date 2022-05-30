using UnityEngine;

namespace Game.UI.MenuHandling {
	
	/// <summary>
	/// Manages the Overlay
	/// </summary>
	public class Overlay : MonoBehaviour {
		public static            Overlay        Instance { get; private set; }
		
		/// <summary>
		/// <c>ActivityToggle</c> of the overlay
		/// </summary>
		[SerializeField] private ActivityToggle activityToggle;


		public Overlay() {
			Instance = this;
		}

		/// <summary>
		/// Getter of the <c>ActivityToggle</c> attribute
		/// </summary>
		public ActivityToggle ActivityToggle {
			get { return activityToggle; }
		}
	}
}