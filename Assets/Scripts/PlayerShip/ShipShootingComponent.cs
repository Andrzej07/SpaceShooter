using UnityEngine;

namespace SpaceShooter
{
	public class ShipShootingComponent : ShipSimulationComponent
	{
		[SerializeField]
		private Vector2 bulletStartOffset;
		[SerializeField]
		private ProjectilePool projectilePool;
		[SerializeField]
		private float timeBetweenBullets;
		
		private float cooldownTime;
		
		protected override void OnInitialize() { }

		public override void TickSimulation()
		{
			if (Time.time < cooldownTime)
			{
				return;
			}
			
			bool isFiring = Ship.LatestInput.isFiring;

			if (isFiring)
			{
				var bulletData = CreateBulletData();
				projectilePool.CreateBullet(bulletData);
				cooldownTime = Time.time + timeBetweenBullets;
			}
		}

		private ProjectileCreationData CreateBulletData()
		{
			return new ProjectileCreationData
			{
				startPosition = Ship.SimulationPosition + bulletStartOffset,
				tagsToIgnore = Ship.Tags
			};
		}
	}
}