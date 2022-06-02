using UnityEngine;
using Random = System.Random;

namespace Game
{
	public class HungerScript : MonoBehaviour
	{
		public static HungerScript Instance { get; private set; }
	
		[SerializeField] private PlayerScript player;
		[SerializeField] private bool         hungerActive    = true;
		[SerializeField] private int          minHungerTime   = 3; // Minimum seconds since the player hungers
		[SerializeField] private int          maxHungerTime   = 5; // Maximum seconds since the player hungers
		[SerializeField] private int          minHungerPoints = 3; // Minimum life points removed per hunger
		[SerializeField] private int          maxHungerPoints = 5; // Maximum life points removed per hunger

		private int   _nextHunger;    // Seconds that have to be passed to the next hunger
		private float _passedSeconds; // Seconds that are passed since the last hunger

		public HungerScript()
		{
			Instance = this;
		}

		#region Unity Methods

		private void Start()
		{
			if (hungerActive)
				_nextHunger = Util.Avg(new[] {minHungerPoints, maxHungerPoints});
		}

		void Update()
		{
			if (hungerActive)
			{
				_passedSeconds += Time.deltaTime;

				if (_passedSeconds >= _nextHunger)
				{
					_passedSeconds = 0f;
					var rand = new Random();
					player.UpdateLifePoints(-rand.Next(minHungerPoints, maxHungerPoints + 1));
					_nextHunger = rand.Next(minHungerTime, maxHungerTime + 1);
				}
			}
		}

		#endregion

		/// <summary>
		/// Called when the player eats something. Adds life points to the player.
		/// </summary>
		/// <param name="saturation">is equal to the life points that will be added.</param>
		public void Eat(int saturation)
		{
			player.UpdateLifePoints(saturation);
		}

		#region Getters // Can be removed, if HungerTester is not needed anymore

		public bool HungerActive
		{
			get { return hungerActive; }
			set { hungerActive = value; }
		}
		
		public int MinHungerPoints
		{
			get { return minHungerPoints; }
			set { minHungerPoints = value; }
		}

		public int MaxHungerPoints
		{
			get { return maxHungerPoints; }
			set { maxHungerPoints = value; }
		}
		public int MinHungerTime
		{
			get { return minHungerTime; }
			set { minHungerTime = value; }
		}

		public int MaxHungerTime
		{
			get { return maxHungerTime; }
			set { maxHungerTime = value; }
		}

		#endregion
	}
}