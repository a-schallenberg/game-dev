using System.Collections.Generic;
using UnityEditor;

public static class ResourceHandler {
	public static Dictionary<ResourceType, Resource> Resources = new();

	static ResourceHandler() {
		Resources.Add(ResourceType.Wood, new Resource(ResourceType.Wood));
		Resources.Add(ResourceType.Stone, new Resource(ResourceType.Stone));
		Resources.Add(ResourceType.Iron, new Resource(ResourceType.Iron));

		Resources[ResourceType.Wood].Limit = 10;
	}

	public static bool AddResources(ResourceType type, int amount) {
		return Resources[type].Add(amount);
	}
	
	public static bool UseResources(ResourceType type, int amount) {
		return Resources[type].Use(amount);
	}
}