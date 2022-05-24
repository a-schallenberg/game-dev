using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class RandomStateScript : StateScript {
	[SerializeField] private double minStateChangeSeconds;
	[SerializeField] private double maxStateChangeSeconds;

	private DateTime _nextStateChange;
	private bool     _finalState;

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

	public override void Reset() {
		base.Reset();
		_finalState = false;
	}

	private DateTime NewNextStageChange() {
		return DateTime.Now.AddSeconds(RandomInt(minStateChangeSeconds, maxStateChangeSeconds));
	}
	
	private double RandomInt(double min, double max) {
		return (max - min) * (new Random().NextDouble()) + min;
	}
}