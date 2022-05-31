using System;
using UnityEngine;

namespace Game.UI
{
	public class LifeBarFill : MonoBehaviour
	{
		[SerializeField]                private RectTransform lifePoints;
		[SerializeField, Range(0f, 1f)] private float         lerpGoal;
		[SerializeField, Range(0, 1)]   private float         lerpSpeed;
		
		private float _value;

		public void UpdateValue(float value)
		{
			lerpGoal = value;
		}

		private void Update()
		{
			if (!Util.FloatEquals(lerpGoal, _value))
			{
				_value = Mathf.Lerp(_value, lerpGoal, lerpSpeed);
				UpdateBar();
			} else if (lerpGoal != _value)
			{
				_value = lerpGoal;
			} 
		}

		private void UpdateBar()
		{
			lifePoints.localScale = Util.ChangeXInVector(lifePoints.localScale, _value);
		}
	}
}