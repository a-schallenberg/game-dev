using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class HungerTester : MonoBehaviour
{
	[SerializeField] private bool hungerActive    = true;
	[SerializeField] private int  minHungerTime   = 3; // Minimum seconds since the player hungers
	[SerializeField] private int  maxHungerTime   = 5; // Maximum seconds since the player hungers
	[SerializeField] private int  minHungerPoints = 3; // Minimum life points removed per hunger
	[SerializeField] private int  maxHungerPoints = 5; // Maximum life points removed per hunger

	private bool _hungerActive;
	private int  _minHungerTime;
	private int  _maxHungerTime;
	private int  _minHungerPoints;
	private int  _maxHungerPoints;

	private void Update()
	{
		if (_hungerActive != hungerActive)
		{
			_hungerActive                      = hungerActive;
			HungerScript.Instance.HungerActive = hungerActive;
		}
		if (_minHungerTime != minHungerTime)
		{
			_minHungerTime                      = minHungerTime;
			HungerScript.Instance.MinHungerTime = minHungerTime;
		}
		if (_maxHungerTime != maxHungerTime)
		{
			_maxHungerTime                      = maxHungerTime;
			HungerScript.Instance.MaxHungerTime = maxHungerTime;
		}
		if (_minHungerPoints != minHungerPoints)
		{
			_minHungerPoints                      = minHungerPoints;
			HungerScript.Instance.MinHungerPoints = minHungerPoints;
		}
		if (_maxHungerPoints != maxHungerPoints)
		{
			_maxHungerPoints                      = maxHungerPoints;
			HungerScript.Instance.MaxHungerPoints = maxHungerPoints;
		}
	}
}