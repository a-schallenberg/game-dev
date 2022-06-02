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
		public static LifeBar Instance { get; private set; }

		private int _points;
		
		/// <summary>
		/// The current life points of the player.
		/// </summary>
		public int Points
		{
			get
			{
				return _points;
			}
			private set
			{
				print($"Points: Old: {_points}, New: {value}");
				_points = value;
			}
		}

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
		/// Adds (positive values) or removes (negative values) life points to the bar. Starts the filling animation starting at the old life point value.
		/// </summary>
		/// <param name="addend">Life point value that has to be added</param>
		public void UpdatePoints(int addend)
		{
			
			print($"Points before: {Points}");
			Points = Util.LimitInt(Points + addend, 0, MaxPoints);
			UpdateBar();
			if (Points <= 0f) OnDie();
			
			print($"Points after: {Points}");
		}

		/// <summary>
		/// Is triggered, if the life points are equal to (or less then) zero
		/// </summary>
		private void OnDie()
		{
			PlayerScript.Instance.OnDie();
		}

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