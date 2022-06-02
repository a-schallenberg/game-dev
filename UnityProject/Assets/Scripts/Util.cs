using System;
using System.Linq;
using UnityEngine;

public static class Util
{
	private const float TOLERANCE = 0.0001f;

	public static bool FloatEquals(float a, float b)
	{
		return Math.Abs(a - b) < TOLERANCE;
	}

	public static Vector3 ChangeXInVector(Vector3 vec, float x)
	{
		return new Vector3(x, vec.y, vec.z);
	}

	public static int LimitInt(int x, int lower, int upper)
	{
		return Math.Min(Math.Max(lower, x), upper);
	}

	public static int Avg(int[] numbers)
	{
		return (int) Math.Round((double) numbers.Sum()/numbers.Length);
	}
}