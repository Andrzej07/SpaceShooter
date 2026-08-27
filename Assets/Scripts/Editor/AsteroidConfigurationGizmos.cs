using System;
using UnityEditor;
using UnityEngine;

namespace SpaceShooter.Editor
{
	public class AsteroidConfigurationGizmos : EditorWindow
	{
		[SerializeField]
		private AsteroidsConfiguration configuration;

		private int colorIndex;
		private GUIStyle textStyle;
		private static Color[] colors = { Color.red,  Color.yellow, Color.green, Color.cyan,  Color.blue, Color.magenta, Color.white };
		
		[MenuItem("SpaceShooter/Asteroid Configuration")]
		public static void ShowWindow()
		{
			GetWindow<AsteroidConfigurationGizmos>("Asteroid Configuration");
		}

		private void OnEnable()
		{
			SceneView.duringSceneGui += OnSceneGUI;
			textStyle = new GUIStyle();
		}

		private void OnDisable()
		{
			SceneView.duringSceneGui -= OnSceneGUI;
		}

		private void OnSceneGUI(SceneView sceneView)
		{
			colorIndex = 0;
			DrawRect("AliveRect", configuration.AliveRect);
			DrawRect("TargetRect", configuration.TargetRect);
			
			for (int i = 0; i < configuration.SpawnRects.Length; i++)
			{
				DrawRect($"SpawnRect({i})", configuration.SpawnRects[i]);
			}
		}

		private void DrawRect(string name, Rect rect)
		{
			Handles.color = colors[colorIndex++ % colors.Length];
			Handles.DrawWireCube(rect.center, rect.size);

			textStyle.normal.textColor = Handles.color;
			Handles.Label(rect.center, name, textStyle);
		}
	}
}