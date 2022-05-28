using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkshopButton : MonoBehaviour {

	private Building  _prefab;

	public void Init(Building prefab) {
		_prefab = prefab;
	}

	public void OnHoverEnter() {
		Costbar.Instance.Enable(-_prefab.Costs.wood, -_prefab.Costs.stone, -_prefab.Costs.iron);
	}
	
	public void OnHoverExit() {
		Costbar.Instance.Disable();
	}
}
