using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopButton : MonoBehaviour {

	[SerializeField] private Building  prefab;
	[SerializeField] private Transform costs;
	
	[SerializeField] private GameObject woodPanel;
	[SerializeField] private GameObject stonePanel;
	[SerializeField] private GameObject ironPanel;

	public void Init() {
		if (prefab.Costs.wood >= 0) {
			GameObject panel = Instantiate(woodPanel, costs);
			
			// TODO add text to woodPanel
		} 
		// TODO do this for all resoruces
	}
}
