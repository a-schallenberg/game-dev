using UnityEngine;
using UnityEngine.Events;

namespace Game {
	public class Interactable : MonoBehaviour {
		[SerializeField] private UnityEvent<Collider2D> onInteract;

		public virtual void Interact(Collider2D col) {
			onInteract.Invoke(col);
		}
	}
}