using UnityEngine;

namespace Game.UI.MenuHandling {
	public class Overlay : MonoBehaviour {
		[SerializeField] private ActivityToggle activityToggle;

		public Overlay() {
			Instance = this;
		}

		public static Overlay Instance { get; private set; }

		public ActivityToggle ActivityToggle {
			get { return activityToggle; }
		}
	}
}