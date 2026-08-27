namespace SpaceShooter
{
	public class ProjectilePool : PoolComponent<Projectile>
	{
		public void CreateBullet(ProjectileCreationData projectileCreationData)
		{
			var bullet = ObjectPool.Get();
			bullet.gameObject.SetActive(true);
			bullet.CreateFromData(projectileCreationData);
		}
	}
}