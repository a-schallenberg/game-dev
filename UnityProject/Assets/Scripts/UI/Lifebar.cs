using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour {
	public static Lifebar Instance { get; private set; }

	public float maxPoints;

	[SerializeField] private Slider          slider;
	[SerializeField] private TextMeshProUGUI text;

	private float _points;

	public Lifebar() {
		Instance = this;
	}

	private void Start() {
		_points = maxPoints;
		UpdateBar();
	}

	public void AddPoints(int points) {
		_points = Math.Min(_points + points, maxPoints);
		UpdateBar();
	}

	public void RemovePoints(int points) {
		_points = Math.Max(_points - points, 0f);
		UpdateBar();

		if (_points <= 0f) {
			OnDie();
		}
	}

	private void OnDie() {}

	private void UpdateBar() {
		slider.value = _points / maxPoints;
		text.text    = $"{_points} / {maxPoints}";
	}
}