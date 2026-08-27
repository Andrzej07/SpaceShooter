using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public static class CollectionExtensions
	{
		public static T GetRandomElement<T>(this IReadOnlyList<T> list)
		{
			if (list.Count == 0)
			{
				return default;
			}
			
			return list[Random.Range(0, list.Count)];
		}
	}
}