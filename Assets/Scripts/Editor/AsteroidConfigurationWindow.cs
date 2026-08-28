using UnityEditor;
using UnityEngine;

namespace SpaceShooter.Editor
{
	public class AsteroidConfigurationWindow : EditorWindow
	{
		[SerializeField]
		private AsteroidsConfiguration configuration;
		
		private const float CAP_SIZE = 1;
		private static readonly Color[] COLORS = { Color.red,  Color.yellow, Color.green, Color.cyan,  Color.blue, Color.magenta, Color.white };

		private SerializedObject serializedObject;
		private bool drawRectsEnabled = true;
		private int colorIndex;
		private GUIStyle textStyle;
		private Handles.CapFunction capFunction;
		
		[MenuItem("SpaceShooter/Windows/Asteroid Configuration")]
		public static void ShowWindow()
		{
			GetWindow<AsteroidConfigurationWindow>("Asteroid Configuration");
		}

		private void OnEnable()
		{
			if (configuration == null)
			{
				Debug.LogError("Set configuration field and reopen the window!");
				drawRectsEnabled = false;
				return;
			}
			
			drawRectsEnabled = true;
			serializedObject = new SerializedObject(configuration);
			capFunction = Handles.DotHandleCap;
			SceneView.duringSceneGui += OnSceneGUI;
			textStyle = new GUIStyle();
		}

		private void OnDisable()
		{
			SceneView.duringSceneGui -= OnSceneGUI;
		}

		private void OnGUI()
		{
			bool newValue = EditorGUILayout.Toggle("Draw rects enabled", drawRectsEnabled);

			if (drawRectsEnabled)
			{
				GUILayout.Label("Editable rects are being drawn in the scene view.");
			}
			
			if (newValue != drawRectsEnabled)
			{
				drawRectsEnabled = newValue;
				SceneView.RepaintAll();
			}
		}

		private void OnSceneGUI(SceneView sceneView)
		{
			if (drawRectsEnabled == false)
			{
				return;
			}
			
			serializedObject.Update();
			
			colorIndex = 0;
			HandleRectField(AsteroidsConfiguration.EDITOR_AliveRectFieldName);
			HandleRectField(AsteroidsConfiguration.EDITOR_TargetRectFieldName);
			
			for (int i = 0; i < configuration.SpawnRects.Length; i++)
			{
				HandleRectField(AsteroidsConfiguration.EDITOR_SpawnRectsFieldName, i);
			}
			
			Handles.color = Color.white;

			serializedObject.ApplyModifiedProperties();
		}

		private void HandleRectField(string propertyName, int? arrayIndex = null)
		{
			SerializedProperty property = serializedObject.FindProperty(propertyName);

			if (property == null)
			{
				Debug.LogError($"{propertyName} not found!");
				return;
			}

			string displayName = property.displayName;

			if (arrayIndex != null)
			{
				property = property.GetArrayElementAtIndex(arrayIndex.Value);
			}

			if (property == null)
			{
				Debug.LogError($"Index {arrayIndex.Value} of array {propertyName} not found!");
				return;
			}

			if (arrayIndex != null)
			{
				displayName += $" ({arrayIndex.Value})";
			}
			
			property.rectValue = RectHandle(displayName, property.rectValue);
		}

		private Rect RectHandle(string name, Rect rect)
		{
			textStyle.normal.textColor = Handles.color;
			Handles.Label(rect.center, name, textStyle);
			
			Handles.color = COLORS[colorIndex++ % COLORS.Length];
			Handles.DrawWireCube(rect.center, rect.size);

			rect.min = Handles.FreeMoveHandle(rect.min, CAP_SIZE, Vector3.zero, capFunction);
			rect.max = Handles.FreeMoveHandle(rect.max, CAP_SIZE, Vector3.zero, capFunction);
			
			return rect;
		}
	}
}