using UnityEngine;

namespace SpaceShooter
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField]
		private new Rigidbody2D rigidbody2D;
		[SerializeField]
		private float lifespan;
		[SerializeField]
		private Vector2 speed;

		private ProjectilePool ownerPool;
		private float endOfLifeTime;
		private ObjectTags tagsToIgnore;
		
		public void CreateFromData(ProjectileCreationData projectileCreationData)
		{
			tagsToIgnore = projectileCreationData.tagsToIgnore;
			rigidbody2D.position = projectileCreationData.startPosition;
			rigidbody2D.linearVelocity = speed;
			endOfLifeTime = Time.time + lifespan;
		}

		public void SetPool(ProjectilePool pool)
		{
			ownerPool = pool;
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
			
			DestroyBullet();
		}

		private void DestroyBullet()
		{
			ownerPool.Release(this);
		}
	}
}