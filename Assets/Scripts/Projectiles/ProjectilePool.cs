using UnityEngine;
using UnityEngine.Pool;

namespace SpaceShooter
{
	public class ProjectilePool : MonoBehaviour
	{
		[SerializeField]
		private Projectile projectilePrefab;
		
		private ObjectPool<Projectile> bulletPool;

		private void Awake()
		{
			bulletPool = new ObjectPool<Projectile>(CreateFunc, actionOnRelease: ActionOnRelease);
			Projectile CreateFunc()
			{
				var bullet = Instantiate(projectilePrefab);
				bullet.SetPool(this);

				return bullet;
			}

			void ActionOnRelease(Projectile obj)
			{
				obj.gameObject.SetActive(false);
			}
		}

		public void CreateBullet(ProjectileCreationData projectileCreationData)
		{
			var bullet = bulletPool.Get();
			bullet.gameObject.SetActive(true);
			bullet.CreateFromData(projectileCreationData);
		}

		public void Release(Projectile projectile)
		{
			bulletPool.Release(projectile);
		}
	}
}