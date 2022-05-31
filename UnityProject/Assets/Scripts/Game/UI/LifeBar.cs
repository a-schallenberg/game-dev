using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	/// <summary>
	/// <c>MonoBehavior</c> for the LifeBar in the Overlay. Manges life points.
	/// </summary>
	public class LifeBar : MonoBehaviour
	{
		public const bool DoLerp = true;

		public static LifeBar Instance { get; private set; }

		/// <summary>
		/// The current life points of the player.
		/// </summary>
		public int Points { get; private set; }

		/// <summary>
		/// The maximum life points the player can get.
		/// </summary>
		[SerializeField] private int maxPoints;

		/// <summary>
		/// The <c>Slider</c> used as life bar.
		/// </summary>
		[SerializeField] private LifeBarFill fill;

		/// <summary>
		/// The <c>TextMeshProUGUI</c> showing the life point on the bar.
		/// </summary>
		[SerializeField] private TextMeshProUGUI text;


		public LifeBar()
		{
			Instance = this;
		}

		/// <summary>
		/// New Unity method. Sets slider value that has to be between 0 and 1 (both inclusive).
		/// </summary>
		private void Start()
		{
			UpdateBar();
		}

		/// <summary>
		/// Adds life points to the bar. Starts the filling animation starting at the old life point value.
		/// </summary>
		/// <param name="points">Life point value that has to be added</param>
		public void AddPoints(int points)
		{
			Points = Math.Min(Points + points, maxPoints);

			UpdateBar();
		}

		/// <summary>
		/// Removes life points to the bar. Starts the filling animation starting at the old life point value.
		/// Triggers the OnDie method, if the life points are equal to (or less then)  zero.
		/// </summary>
		/// <param name="points">Life point value that has to be removed</param>
		public void RemovePoints(int points)
		{
			Points = Math.Max(Points - points, 0);

			UpdateBar();

			if (Points <= 0f)
			{
				OnDie();
			}
		}

		/// <summary>
		/// Is triggered, if the life points are equal to (or less then) zero
		/// </summary>
		private void OnDie() {}

		private void UpdateBar()
		{
			fill.UpdateValue(Points / (float) maxPoints);
			text.text = $"{Points.ToString()} / {maxPoints.ToString()}";
		}

		#region Getter

		public int MaxPoints
		{
			get { return maxPoints; }
			set
			{
				maxPoints = value;
				UpdateBar();
			}
		}

		#endregion
	}
}