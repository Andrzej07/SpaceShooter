using UnityEngine;

namespace SpaceShooter
{
	public class RandomAsteroidSpawner : MonoBehaviour
	{
		[SerializeField]
		private Vector2 timeBetweenSpawnsMinMax;
		[SerializeField]
		private AsteroidPool asteroidPool;

		private float nextSpawnTime;

		public float TimeToSpawn => nextSpawnTime - Time.time;
		
		private void Awake()
		{
			SetNextSpawnTime();
		}

		private void FixedUpdate()
		{
			if (Time.time > nextSpawnTime)
			{
				SpawnAndResetTimer();
			}
		}

		public void SpawnAndResetTimer()
		{
			asteroidPool.CreateRandomAsteroid();
			SetNextSpawnTime();
		}

		private float SetNextSpawnTime()
		{
			return nextSpawnTime = Time.time + timeBetweenSpawnsMinMax.RandomBetween();
		}
	}
}