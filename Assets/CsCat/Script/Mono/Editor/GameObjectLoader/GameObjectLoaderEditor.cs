using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace CsCat
{
	[CustomEditor(typeof(GameObjectLoader))]
	public partial class GameObjectLoaderEditor : Editor
	{
		private GameObjectLoader target => base.target as GameObjectLoader;

		void OnEnable()
		{
			var instance = GameObjectLoader.instance;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
			{
				using (new EditorGUILayoutBeginHorizontalScope())
				{
					EditorGUILayout.LabelField("save_path:", GUILayout.MaxWidth(70));
					target.textAsset = EditorGUILayout.ObjectField(target.textAsset, typeof(TextAsset)) as TextAsset;
				}

				using (new EditorGUILayoutBeginHorizontalScope())
				{
					if (GUILayout.Button("Load"))
					{
						target.Load(target.textAsset.text);
						return;
					}

					if (GUILayout.Button("Save"))
					{
						target.Save();
						return;
					}
				}

				if (GUILayout.Button("Clear"))
				{
					target.Clear();
					return;
				}
			}

			if (GUILayout.Button("New Grid(child)"))
			{
				GameObject gridGameObject = target.NewChildGameObject("Grid");
				gridGameObject.AddComponent<Grid>();
				GameObject tilemapGameObject = gridGameObject.NewChildGameObject("Tilemap");
				tilemapGameObject.AddComponent<Tilemap>();
				tilemapGameObject.AddComponent<TilemapRenderer>();
			}
		}
	}
}