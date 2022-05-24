using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundationTester : MonoBehaviour {
	[SerializeField]                  public  List<Building> foundations;

	private void Awake() {
		LoadStartFoundations();
	}

	public void LoadStartFoundations() {
		foreach (var foundation in foundations) {
			BuildMenu.Instance.AddFoundationItem(foundation);
		}
	}
}