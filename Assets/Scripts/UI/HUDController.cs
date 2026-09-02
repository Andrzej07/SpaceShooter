using UnityEngine;

namespace SpaceShooter
{
	public class HUDController : MonoBehaviour
	{
		[SerializeField]
		private LabelDataSource asteroidCountDataSource;

		private int count = 0;
		
		private void Awake()
		{
			UpdateLabelData();
		}

		private void OnEnable()
		{
			Asteroid.AsteroidShotAndDestroyed += AsteroidOnAsteroidShotAndDestroyed;
		}

		private void OnDisable()
		{
			Asteroid.AsteroidShotAndDestroyed -= AsteroidOnAsteroidShotAndDestroyed;
		}

		private void AsteroidOnAsteroidShotAndDestroyed()
		{
			count++;
			UpdateLabelData();
		}

		private void UpdateLabelData()
		{
			asteroidCountDataSource.labelContent = $"Asteroids destroyed: {count}";
			asteroidCountDataSource.Commit();
		}
	}
}