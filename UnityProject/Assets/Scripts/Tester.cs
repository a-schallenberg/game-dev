using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour {
	[SerializeField] private int woodAmount;
	[SerializeField] private int woodLimit;
	[SerializeField] private int stoneAmount;
	[SerializeField] private int stoneLimit;
	[SerializeField] private int ironAmount;
	[SerializeField] private int ironLimit;

	[SerializeField]                  private float          maxLifePoints;
	[SerializeField, Range(0f, 100f)] private float          lifePoints;
	[SerializeField]                  public  List<Building> foundations;

	private void Awake() {
		LoadStartFoundations();
	}

	private void Start() {
		ResourceHandler.Update();
	}

	private void Update() {
		UpdateLifeBar();
		UpdateResources();
	}

	public void LoadStartFoundations() {
		foreach (var foundation in foundations) {
			BuildMenu.Instance.AddFoundationItem(foundation);
		}
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