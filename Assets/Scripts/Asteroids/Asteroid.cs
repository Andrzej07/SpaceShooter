using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.VFX;

namespace SpaceShooter
{
	public class Asteroid : MonoBehaviour, IPooledObject<Asteroid>, IProjectileHitReceiver
	{
		public static event Action AsteroidShotAndDestroyed;
		
		[SerializeField]
		private Transform meshTransform;
		[SerializeField]
		private AsteroidsConfiguration configuration;
		[SerializeField]
		private MeshFilter meshFilter;
		[SerializeField]
		private PolygonCollider2D collider;
		[SerializeField]
		private Rigidbody2D rigidbody2D;
		[SerializeField]
		private VisualEffect destructionEffect;
		[SerializeField]
		private float vfxLifetime;

		private bool enteredAliveRect;
		private Tween? destroyTween; 
		
		public Action<Asteroid> ReturnToPoolAction { get; set; }

		private void FixedUpdate()
		{
			if (destroyTween != null)
			{
				return;
			}
			
			bool isInRect = configuration.AliveRect.Contains(rigidbody2D.position);
			
			enteredAliveRect |= isInRect;

			if (enteredAliveRect && isInRect == false)
			{
				DestroyAsteroid(false);
			}
		}

		public void InitializeRandom()
		{
			destructionEffect.Stop();
			enteredAliveRect = false;

			transform.localScale = Vector3.one * configuration.GetRandomScale();
			Vector2 startPosition = configuration.GetRandomStartPosition();
			rigidbody2D.position = startPosition;
			rigidbody2D.angularVelocity = configuration.GetRandomRotationSpeed();
			rigidbody2D.linearVelocity = configuration.GetRandomSpeed() * configuration.GetRandomDirection(startPosition);
			collider.enabled = true;
			
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

		private void DestroyAsteroid(bool playVFX)
		{
			if (playVFX == false)
			{
				ReturnToPoolAction?.Invoke(this);
				return;
			}

			collider.enabled = false;
			rigidbody2D.linearVelocity = Vector2.zero;
			Tween.Scale(meshTransform, Vector3.zero, vfxLifetime / 2);
			destroyTween = Tween.Delay(this, vfxLifetime, OnDestroyVFXComplete);
			destructionEffect.Play();
		}

		private void OnDestroyVFXComplete(Asteroid asteroid)
		{
			ReturnToPoolAction?.Invoke(asteroid);
			destroyTween = default;
		}

		public void OnHit(Projectile projectile)
		{
			DestroyAsteroid(true);
			AsteroidShotAndDestroyed?.Invoke();
		}
	}
}