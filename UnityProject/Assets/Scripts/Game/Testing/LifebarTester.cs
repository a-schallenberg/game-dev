using Game.UI;
using UnityEngine;

namespace Game.Testing {
	public class LifebarTester : MonoBehaviour {
		[SerializeField]                  private float maxLifePoints;
		[SerializeField, Range(0f, 100f)] private float lifePoints;

		private void Update() {
			UpdateLifeBar();
		}

		public void UpdateLifeBar() {
			var points = LifeBar.Instance.Points;
			if (points < lifePoints) {
				LifeBar.Instance.AddPoints(lifePoints - points);
			} else if (points > lifePoints) {
				LifeBar.Instance.RemovePoints(points - lifePoints);
			}

			LifeBar.Instance.maxPoints = maxLifePoints;
		}
	}
}