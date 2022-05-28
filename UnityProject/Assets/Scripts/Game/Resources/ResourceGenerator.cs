using UnityEngine;
using Random = System.Random;

namespace Game.Resources {
	public class ResourceGenerator : MonoBehaviour {
		[SerializeField] private ResourceType resourceType;
		[SerializeField] private int          minGeneratedAmount;
		[SerializeField] private int          maxGeneratedAmount;

		private int Amount() {
			return new Random().Next(minGeneratedAmount, maxGeneratedAmount + 1);
		}

		public void Generate() {
			ResourceHandler.AddResources(resourceType, Amount());
		}
	}
}