using UnityEngine;

public static class Util {

	public static Structure FindNextStructure(GameObject gameObject) {
		Structure[] structures = gameObject.GetComponents<Structure>();

		if (structures.Length != 0) {
			return structures[0];
		}

		structures = gameObject.GetComponentsInChildren<Structure>();
			
		if (structures.Length != 0) {
			return structures[0];
		}
		
		structures = gameObject.GetComponentsInParent<Structure>();
			
		if (structures.Length != 0) {
			return structures[0];
		}

		return null;
	}
}