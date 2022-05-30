using Game.UI;
using UnityEngine;

namespace Game.Testing {
	public class LifebarTester : MonoBehaviour {
		[SerializeField]                  private int maxLifePoints;
		[SerializeField, Range(0f, 100f)] private float lifePoints;

		private void Update() {
			UpdateLifeBar();
		}

		public void UpdateLifeBar() {
			LifeBar.Instance.MaxPoints = maxLifePoints;
			
			var points = LifeBar.Instance.Points;
			if (points < lifePoints) {
				LifeBar.Instance.AddPoints(lifePoints - points);
			} else if (points > lifePoints) {
				LifeBar.Instance.RemovePoints(points - lifePoints);
			}
		}
	}
}