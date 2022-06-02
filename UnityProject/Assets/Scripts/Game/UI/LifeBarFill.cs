using System;
using UnityEngine;

namespace Game.UI
{
	public class LifeBarFill : MonoBehaviour
	{
		[SerializeField]                private RectTransform lifePoints;
		[SerializeField, Range(0f, 3f)] private float         lerpStep;

		private float _lerpStart;
		private float _lerpGoal;
		private float _value;
		private float _t;

		public void UpdateValue(float value)
		{
			_lerpStart = _value;
			_lerpGoal  = value;
			_t         = 0f;
		}

		private void Update()
		{
			if (!Util.FloatEquals(_lerpGoal, _value))
			{
				_t     += lerpStep * Time.deltaTime;
				_value =  Mathf.Lerp(_lerpStart, _lerpGoal, _t);

				UpdateBar();
			}
			else if (_lerpGoal != _value)
			{
				_value = _lerpGoal;
			}
		}

		private void UpdateBar()
		{
			lifePoints.localScale = Util.ChangeXInVector(lifePoints.localScale, _value);
		}
	}
}