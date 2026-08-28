using System;
using UnityEngine;

namespace SpaceShooter
{
	[CreateAssetMenu]
	public class AsteroidsConfiguration : ScriptableObject
	{
		[SerializeField]
		private AsteroidPreset[] presets;
		[SerializeField]
		private Vector2 minMaxSpeed;
		[SerializeField]
		private Vector2 angularVelocityRange;
		[SerializeField]
		private Rect[] spawnRects;
		[SerializeField]
		private Rect targetRect;
		[SerializeField]
		private Rect aliveRect;
		[SerializeField]
		private Vector2 objectScaleRange = Vector2.one;

		public Rect AliveRect => aliveRect;
		public Rect TargetRect => targetRect;
		public Rect[] SpawnRects => spawnRects;
		public static string EDITOR_AliveRectFieldName => nameof(aliveRect);
		public static string EDITOR_TargetRectFieldName => nameof(targetRect);
		public static string EDITOR_SpawnRectsFieldName => nameof(spawnRects);

		public AsteroidPreset GetRandomPreset()
		{
			return presets.GetRandomElement();
		}

		public float GetRandomSpeed()
		{
			return minMaxSpeed.RandomBetween();
		}

		public float GetRandomRotationSpeed()
		{
			return angularVelocityRange.RandomBetween();
		}

		public Vector2 GetRandomStartPosition()
		{
			return spawnRects.GetRandomElement().GetRandomPositionInRect();
		}

		public Vector2 GetRandomDirection(Vector2 startPosition)
		{
			return (targetRect.GetRandomPositionInRect() - startPosition).normalized;
		}

		public float GetRandomScale()
		{
			return objectScaleRange.RandomBetween();
		}
	}

	[Serializable]
	public class AsteroidPreset
	{
		[SerializeField]
		private Mesh mesh;
		[SerializeField]
		private Vector2[] colliderShape;
		[SerializeField]
		private float meshScale;

		public Mesh Mesh => mesh;
		public Vector2[] ColliderShape => colliderShape;
		public float MeshScale => meshScale;
	}
}