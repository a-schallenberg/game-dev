using Game.Resources;
using UnityEngine;

namespace Game.Testing {
	public class StartResourceTester : MonoBehaviour {
		[SerializeField] private int woodAmount;
		[SerializeField] private int woodLimit;
		[SerializeField] private int stoneAmount;
		[SerializeField] private int stoneLimit;
		[SerializeField] private int ironAmount;
		[SerializeField] private int ironLimit;

		private void Start() {
			ResourceHandler.Resources[ResourceType.Wood].Limit  = woodLimit;
			ResourceHandler.Resources[ResourceType.Stone].Limit = stoneLimit;
			ResourceHandler.Resources[ResourceType.Iron].Limit  = ironLimit;

			ResourceHandler.AddResources(ResourceType.Wood, woodAmount);
			ResourceHandler.AddResources(ResourceType.Stone, stoneAmount);
			ResourceHandler.AddResources(ResourceType.Iron, ironAmount);
		}
	}
}