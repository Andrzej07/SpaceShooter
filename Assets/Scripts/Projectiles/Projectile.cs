using System;
using UnityEngine;

namespace SpaceShooter
{
	public class Projectile : MonoBehaviour, IPooledObject<Projectile>
	{
		[SerializeField]
		private new Rigidbody2D rigidbody2D;
		[SerializeField]
		private float lifespan;
		[SerializeField]
		private Vector2 speed;

		private float endOfLifeTime;
		private ObjectTags tagsToIgnore;
		
		public Action<Projectile> ReturnToPoolAction { get; set; }
		
		public void CreateFromData(ProjectileCreationData projectileCreationData)
		{
			tagsToIgnore = projectileCreationData.tagsToIgnore;
			rigidbody2D.position = projectileCreationData.startPosition;
			rigidbody2D.linearVelocity = speed;
			endOfLifeTime = Time.time + lifespan;
		}

		private void FixedUpdate()
		{
			if (endOfLifeTime < Time.time)
			{
				DestroyBullet();
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			var otherRigidbody = collision.rigidbody == rigidbody2D ? collision.otherRigidbody : collision.rigidbody;
			if (otherRigidbody != null && otherRigidbody.TryGetComponent(out ObjectTagsComponent tags))
			{
				if ((tags.Tags & tagsToIgnore) != 0)
				{
					return;
				}
			}

			if (otherRigidbody.TryGetComponent(out IProjectileHitReceiver hitReceiver))
			{
				hitReceiver.OnHit(this);
			}
			
			DestroyBullet();
		}

		private void DestroyBullet()
		{
			ReturnToPoolAction?.Invoke(this);
		}
	}
}