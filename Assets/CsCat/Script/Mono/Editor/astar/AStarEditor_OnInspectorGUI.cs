using UnityEngine;
using UnityEditor;

namespace CsCat
{
	public partial class AStarEditor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
			{
				_target.astarConfigData.textAsset =
					EditorGUILayout.ObjectField(_target.astarConfigData.textAsset, typeof(TextAsset), false) as
						TextAsset;
				using (new EditorGUILayoutBeginHorizontalScope())
				{
					if (GUILayout.Button("Load"))
					{
						_target.Load();
						return;
					}

					if (GUILayout.Button("Save"))
					{
						_target.Save();
						return;
					}
				}
			}


			_target.astarConfigData.cellSize =
				EditorGUILayout.Vector2Field("cell_size", _target.astarConfigData.cellSize);

			using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
			{
				EditorGUILayout.LabelField("Bounds:", EditorStyles.boldLabel);
				using (new EditorGUIUtilityLabelWidthScope(80))
				{
					using (var check = new EditorGUIBeginChangeCheckScope())
					{
						_target.astarConfigData.isEnableEditOutsideBounds = EditorGUILayout.Toggle("允许在边界外编辑?",
							_target.astarConfigData.isEnableEditOutsideBounds);
						using (new EditorGUILayoutBeginHorizontalScope())
						{
							_target.astarConfigData.minGridX =
								EditorGUILayout.IntField("Left", _target.astarConfigData.minGridX);
							_target.astarConfigData.minGridY =
								EditorGUILayout.IntField("Bottom", _target.astarConfigData.minGridY);
						}

						using (new EditorGUILayoutBeginHorizontalScope())
						{
							_target.astarConfigData.maxGridX =
								EditorGUILayout.IntField("Right", _target.astarConfigData.maxGridX);
							_target.astarConfigData.maxGridY =
								EditorGUILayout.IntField("Top", _target.astarConfigData.maxGridY);
						}

						if (check.IsChanged)
							_target.astarConfigData.Resize();
					}
				}
			}


			if (GUI.changed)
			{
				serializedObject.ApplyModifiedProperties();
				EditorUtility.SetDirty(_target);
			}
		}
	}
}