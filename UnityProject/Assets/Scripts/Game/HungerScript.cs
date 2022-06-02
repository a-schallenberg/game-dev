using System;
using System.Collections;
using Game;
using UnityEngine;
using Random = System.Random;

public class HungerScript : MonoBehaviour
{
	[SerializeField] private PlayerScript player;
	[SerializeField] private int          minHungerTime   = 3; // Minimum seconds since the player hungers
	[SerializeField] private int          maxHungerTime   = 5; // Maximum seconds since the player hungers
	[SerializeField] private int          minHungerPoints = 3; // Minimum life points removed per hunger
	[SerializeField] private int          maxHungerPoints = 5; // Maximum life points removed per hunger

	private int   _nextHunger;    // Seconds that have to be passed to the next hunger
	private float _passedSeconds; // Seconds that are passed since the last hunger

	private void Start()
	{
		_nextHunger = Util.Avg(new[] {minHungerPoints, maxHungerPoints});
	}

	void Update()
	{
		_passedSeconds += Time.deltaTime;

		if (_passedSeconds >= _nextHunger)
		{
			print("Hunger");
			_passedSeconds = 0f;
			var rand = new Random();
			player.UpdateLifePoints(-rand.Next(minHungerPoints, maxHungerPoints + 1));
			_nextHunger = rand.Next(minHungerTime, maxHungerTime + 1);
		}
	}

	/// <summary>
	/// Called when the player eats something. Adds life points to the player.
	/// </summary>
	/// <param name="saturation">is equal to the life points that will be added.</param>
	private void Eat(int saturation)
	{
		player.UpdateLifePoints(saturation);
	}
}