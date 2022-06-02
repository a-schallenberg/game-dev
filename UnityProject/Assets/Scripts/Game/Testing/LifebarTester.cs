using Game.UI;
using UnityEngine;

namespace Game.Testing
{
	public class LifebarTester : MonoBehaviour
	{
		[SerializeField]                private int maxLifePoints;
		[SerializeField, Range(0, 100)] private int lifePoints;
		
		private int _maxLifePoints;
		private int _lifePoints;


		private void Update()
		{
			UpdateLifeBar();
		}

		public void UpdateLifeBar()
		{
			if (maxLifePoints != _maxLifePoints)
			{
				_maxLifePoints             = maxLifePoints;
				LifeBar.Instance.MaxPoints = maxLifePoints;
			}

			if (lifePoints != _lifePoints)
			{
				_lifePoints = lifePoints;
				LifeBar.Instance.UpdatePoints(lifePoints - LifeBar.Instance.Points);
			}

		}
	}
}