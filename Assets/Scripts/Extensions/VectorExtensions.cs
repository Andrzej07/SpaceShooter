using UnityEngine;

namespace SpaceShooter
{
	public static class VectorExtensions
	{
		public static Vector3 WithZ(this Vector3 vector, float z)
		{
			return new Vector3(vector.x, vector.y, z);
		}

		public static Vector3 MoveTowardsPerComponent(this Vector3 vector, Vector3 target, float delta)
		{
			float newX = Mathf.MoveTowards(vector.x, target.x, delta);
			float newY = Mathf.MoveTowards(vector.y, target.y, delta);
			float newZ = Mathf.MoveTowards(vector.z, target.z, delta);
			
			return new Vector3(newX, newY, newZ);
		}

		public static float ComponentsAverage(this Vector2 vector)
		{
			return (vector.x + vector.y) / 2;
		}

		public static float RandomBetween(this Vector2 vector2)
		{
			return Random.Range(vector2.x, vector2.y);
		}
	}
}