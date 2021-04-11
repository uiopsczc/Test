
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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("textAsset"), "textAsset".ToGUIContent());
        using (new EditorGUILayoutBeginHorizontalScope())
        {
          if (GUILayout.Button("Load"))
          {
            target.Load();
            return;
          }

          if (GUILayout.Button("Save"))
          {
            target.Save();
            return;
          }
        }
      }


      target.cell_size = EditorGUILayout.Vector2Field("cell_size", target.cell_size);

      using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
      {
        EditorGUILayout.LabelField("Bounds:", EditorStyles.boldLabel);
        using (new EditorGUIUtilityLabelWidthScope(80))
        {
          using (var check = new EditorGUIBeginChangeCheckScope())
          {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("is_enable_edit_outside_bounds"),
              "是否允许在边界外编辑".ToGUIContent());
            using (new EditorGUILayoutBeginHorizontalScope())
            {
              EditorGUILayout.PropertyField(serializedObject.FindProperty("min_grid_x"), "Left".ToGUIContent());
              EditorGUILayout.PropertyField(serializedObject.FindProperty("min_grid_y"), "Bottom".ToGUIContent());
            }

            using (new EditorGUILayoutBeginHorizontalScope())
            {
              EditorGUILayout.PropertyField(serializedObject.FindProperty("max_grid_x"), "Right".ToGUIContent());
              EditorGUILayout.PropertyField(serializedObject.FindProperty("max_grid_y"), "Top".ToGUIContent());
            }

            if (check.IsChanged)
              target.Resize();
          }
        }
      }


      if (GUI.changed)
      {
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
      }
    }

  }
}
