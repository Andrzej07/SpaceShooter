using UnityEditor;
using UnityEngine;

namespace SpaceShooter.Editor
{
	[CustomEditor(typeof(RandomAsteroidSpawner))]
	public class RandomAsteroidSpawnerEditor : UnityEditor.Editor
	{
		private RandomAsteroidSpawner spawner;
		private int lastTimerValue;
		private string lastTimerString;
		
		private void OnEnable()
		{
			spawner = (RandomAsteroidSpawner)target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (Application.isPlaying == false || spawner == null)
			{
				return;
			}

			RefreshTimerLabel();
			
			if (GUILayout.Button("Spawn"))
			{
				spawner.SpawnAndResetTimer();
			}
		}

		private void RefreshTimerLabel()
		{
			float timeToSpawn = spawner.TimeToSpawn;
			int rounded = (int)(timeToSpawn * 10);

			if (lastTimerString == null || rounded != lastTimerValue)
			{
				lastTimerValue = rounded;
				lastTimerString = $"Time to spawn: {timeToSpawn :F1}";
			}
			
			GUILayout.Label(lastTimerString);
		}
	}
}