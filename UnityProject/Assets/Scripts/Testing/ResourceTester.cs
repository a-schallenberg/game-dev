using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTester : MonoBehaviour {
	[SerializeField] private int woodAmount;
	[SerializeField] private int woodLimit;
	[SerializeField] private int stoneAmount;
	[SerializeField] private int stoneLimit;
	[SerializeField] private int ironAmount;
	[SerializeField] private int ironLimit;

	private void Start() {
		ResourceHandler.Update();
	}

	private void Update() {
		UpdateResources();
	}

	private void UpdateResources() {
		UpdateResourceAmounts(ResourceType.Wood, ref woodAmount);
		UpdateResourceAmounts(ResourceType.Stone, ref stoneAmount);
		UpdateResourceAmounts(ResourceType.Iron, ref ironAmount);
		
		ResourceHandler.Resources[ResourceType.Wood].Limit = woodLimit;
		ResourceHandler.Resources[ResourceType.Stone].Limit = stoneLimit;
		ResourceHandler.Resources[ResourceType.Iron].Limit = ironLimit;
	}

	private void UpdateResourceAmounts(ResourceType type, ref int amount) {
		var amnt  = ResourceHandler.Resources[type].Amount;
		if (amnt < amount) {
			if (!ResourceHandler.AddResources(type, amount - amnt)) {
				amount = ResourceHandler.Resources[type].Limit;
			}
		} else if (amnt > amount) {
			if (!ResourceHandler.UseResources(type, amnt - amount)) {
				amount = 0;
			}
		}
	}
}