using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
	/// <summary>
	/// <c>MonoBehavior</c> for the LifeBar in the Overlay. Manges life points.
	/// </summary>
	public class LifeBar : MonoBehaviour {
		public static LifeBar Instance { get; private set; }

		/// <summary>
		/// The current life points of the player.
		/// </summary>
		public float Points { get; private set; }

		/// <summary>
		/// The maximum life points the player can get.
		/// </summary>
		public int maxPoints;

		/// <summary>
		/// Speed of the filling animation of the bar.
		/// </summary>
		[SerializeField] private float lerpSpeed = 2;

		/// <summary>
		/// The <c>Slider</c> used as life bar.
		/// </summary>
		[SerializeField] private Slider slider;

		/// <summary>
		/// The <c>TextMeshProUGUI</c> showing the life point on the bar.
		/// </summary>
		[SerializeField] private TextMeshProUGUI text;

		/// <summary>
		/// Coroutine mutex for the filling animation
		/// </summary>
		private bool _lerpFlag;


		public LifeBar() {
			Instance = this;
		}

		/// <summary>
		/// New Unity method. Sets slider value that has to be between 0 and 1 (both inclusive).
		/// </summary>
		private void Start() {
			slider.value = Points / maxPoints;
			text.text    = $"{((int) Points).ToString()} / {maxPoints.ToString()}";
		}

		/// <summary>
		/// Adds life points to the bar. Starts the filling animation starting at the old life point value.
		/// </summary>
		/// <param name="points">Life point value that has to be added</param>
		public void AddPoints(float points) {
			var start = Points;
			Points = Math.Min(Points + points, maxPoints);
			
			StartCoroutine(Lerp(start));
		}

		/// <summary>
		/// Removes life points to the bar. Starts the filling animation starting at the old life point value.
		/// Triggers the OnDie method, if the life points are equal to (or less then)  zero.
		/// </summary>
		/// <param name="points">Life point value that has to be removed</param>
		public void RemovePoints(float points) {
			var start = Points;
			Points = Math.Max(Points - points, 0f);

			StartCoroutine(Lerp(start));

			if (Points <= 0f) {
				OnDie();
			}
		}
		
		/// <summary>
		/// Is triggered, if the life points are equal to (or less then) zero
		/// </summary>
		private void OnDie() {}

		/// <summary>
		/// Lerp function. Calculates the lerp steps and put them (indirect) into the IEnumerator that will be returned.
		/// </summary>
		/// <param name="start">Start value or rather the old life points value on which the lerp is started.</param>
		/// <returns>An enumerator that can be executed by a coroutine.</returns>
		private IEnumerator Lerp(float start) {
			if (_lerpFlag) { // If mutex is closed, exit
				yield return null;
			}

			_lerpFlag = true; // Close mutex

			var timeScale = 0f;
			while (timeScale < 1) {
				timeScale += Time.deltaTime * lerpSpeed;
				var lerp = Mathf.Lerp(start, Points, timeScale);
				slider.value = lerp / maxPoints;
				text.text    = $"{((int) lerp).ToString()} / {maxPoints.ToString()}";

				yield return null;
			}

			_lerpFlag = false; // Open mutex
		}
	}
}