using UnityEngine;

namespace SpaceShooter
{
	public static class RectExtensions
	{
		public static Vector2 GetRandomPositionInRect(this Rect rect)
		{
			var min = rect.min;
			var max = rect.max;

			return new(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
		}
	}
}