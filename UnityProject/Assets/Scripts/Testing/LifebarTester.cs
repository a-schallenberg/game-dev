using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifebarTester : MonoBehaviour {
	[SerializeField]                  private float          maxLifePoints;
	[SerializeField, Range(0f, 100f)] private float          lifePoints;

	private void Update() {
		UpdateLifeBar();
	}

	public void UpdateLifeBar() {
		var points = Lifebar.Instance.Points;
		if (points < lifePoints) {
			Lifebar.Instance.AddPoints(lifePoints - points);
		} else if (points > lifePoints) {
			Lifebar.Instance.RemovePoints(points - lifePoints);
		}
		
		Lifebar.Instance.maxPoints = maxLifePoints;
	}
}