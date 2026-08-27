namespace SpaceShooter
{
	public interface IProjectileHitReceiver
	{
		void OnHit(Projectile projectile);
	}
}