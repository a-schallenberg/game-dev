using System.Collections.Generic;
using UnityEngine;

namespace Game.Structures.StateScripts {
	public abstract class StateScript : MonoBehaviour {
		[SerializeField] protected List<GameObject> states;

		protected int StateIndex;

		private void Awake() {
			InitState();
		}

		public virtual void Reset() {
			StateIndex = 0;
			InitState();
		}

		public abstract void InitState();

		public abstract void NextState();
		
		protected void DisableStates() {
			states.ForEach(state => state.SetActive(false));
		}
	}
}