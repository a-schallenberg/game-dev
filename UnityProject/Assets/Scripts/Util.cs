using UnityEngine;

public static class Util {
	public static readonly GameInputActions InputAction = new();

	public static Vector3 ChangeVectorsX(Vector3 vec, float x) {
		vec.x = x;
		return vec;
	}
}