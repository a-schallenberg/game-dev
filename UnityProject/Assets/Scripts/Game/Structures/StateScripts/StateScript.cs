using System.Collections.Generic;
using Game.Structures.StateScripts.States;
using UnityEngine;

namespace Game.Structures.StateScripts {
	public abstract class StateScript : MonoBehaviour {
		protected const string TagInProgress = "Structure_InProgress";
		protected const string TagFinished   = "Structure_Finished";
		
		[SerializeField] protected List<State> states;

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

		public virtual bool IsInFinalState()
		{
			return StateIndex == LastBuiltStateIndex();
		}
		
		public virtual BuiltState GetNextBuiltState()
		{
			for (var i = StateIndex + 1; i < states.Count; i++)
			{
				if (states[i] is BuiltState)
				{
					return (BuiltState) states[i];
				}
			}

			return null;
		}

		protected void DisableStates() 
		{
			states.ForEach(state => state.gameObject.SetActive(false));
		}

		private int LastBuiltStateIndex()
		{
			for (var i = states.Count - 1; i >= 0; i--)
			{
				if (states[i] is BuiltState)
				{
					return i;
				}
			}

			return -1;
		}
	}
}