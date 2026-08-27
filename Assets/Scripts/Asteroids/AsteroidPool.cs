namespace SpaceShooter
{
	public class AsteroidPool : PoolComponent<Asteroid>
	{
		public Asteroid CreateRandomAsteroid()
		{
			var asteroid = ObjectPool.Get();
			asteroid.gameObject.SetActive(true);
			asteroid.InitializeRandom();

			return asteroid;
		}
	}
}