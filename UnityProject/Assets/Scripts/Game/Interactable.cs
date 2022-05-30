using UnityEngine;
using UnityEngine.Events;

namespace Game {
	
	/// <summary>
	/// Makes <c>GameObject</c> interactable
	/// </summary>
	public class Interactable : MonoBehaviour {
		[SerializeField] private UnityEvent<Collider2D> onInteract;

		/// <summary>
		/// Has to be run on interacting with this <c>GameObject</c>
		/// Runs all onInteract actions
		/// </summary>
		/// <param name="col">The collider, which is triggered for interacting</param>
		public virtual void Interact(Collider2D col) {
			onInteract.Invoke(col);
		}
	}
}