using System;
using UnityEngine;
using Random = System.Random;

namespace Game.Structures.StateScripts {
	public class RandomStateScript : StateScript {
		[SerializeField] private double minStateChangeSeconds;
		[SerializeField] private double maxStateChangeSeconds;
		private                  bool   _finalState;

		private DateTime _nextStateChange;

		public override void Reset() {
			base.Reset();
			_finalState = false;
		}

		private void Update() {
			if (!_finalState && _nextStateChange.CompareTo(DateTime.Now) < 0) {
				NextState();

				_nextStateChange = NewNextStageChange();
			}
		}

		public override void InitState() {
			if (states == null || states.Count == 0) {
				throw new ArgumentException($"No states are detected in {this}");
			}

			DisableStates();
			states[StateIndex].SetActive(true);

			if (states.Count == 1) {
				_finalState = true;
			} else {
				_nextStateChange = NewNextStageChange();
			}
		}

		public override void NextState() {
			states[StateIndex].SetActive(false);
			states[++StateIndex].SetActive(true);

			if (StateIndex + 1 >= states.Count) {
				_finalState = true;
			}
		}

		private DateTime NewNextStageChange() {
			return DateTime.Now.AddSeconds(RandomInt(minStateChangeSeconds, maxStateChangeSeconds));
		}

		private double RandomInt(double min, double max) {
			return (max - min) * new Random().NextDouble() + min;
		}
	}
}