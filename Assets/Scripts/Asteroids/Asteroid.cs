using System;
using UnityEngine;

namespace SpaceShooter
{
	public class Asteroid : MonoBehaviour, IPooledObject<Asteroid>, IProjectileHitReceiver
	{
		[SerializeField]
		private AsteroidsConfiguration configuration;
		[SerializeField]
		private MeshFilter meshFilter;
		[SerializeField]
		private new PolygonCollider2D collider;
		[SerializeField]
		private new Rigidbody2D rigidbody2D;

		private bool enteredAliveRect;
		
		public Action<Asteroid> ReturnToPoolAction { get; set; }

		private void FixedUpdate()
		{
			bool isInRect = configuration.AliveRect.Contains(rigidbody2D.position);
			
			enteredAliveRect |= isInRect;

			if (enteredAliveRect && isInRect == false)
			{
				DestroyAsteroid();
			}
		}

		public void InitializeRandom()
		{
			enteredAliveRect = false;
			Vector2 startPosition = configuration.GetRandomStartPosition();
			rigidbody2D.position = startPosition;
			rigidbody2D.linearVelocity = configuration.GetRandomSpeed() * configuration.GetRandomDirection(startPosition);
			
			var preset = configuration.GetRandomPreset();

			if (preset == null)
			{
				return;
			}
			
			ApplyPreset(preset);
		}

		private void ApplyPreset(AsteroidPreset preset)
		{
			meshFilter.sharedMesh = preset.Mesh;
			collider.points = preset.ColliderShape;
			meshFilter.transform.localScale = Vector3.one * preset.MeshScale;
		}

		private void DestroyAsteroid()
		{
			ReturnToPoolAction?.Invoke(this);
		}
		
		public void OnHit(Projectile projectile)
		{
			DestroyAsteroid();
		}
	}
}