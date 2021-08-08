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
        target.astarData.textAsset =
          EditorGUILayout.ObjectField(target.astarData.textAsset, typeof(TextAsset), false) as TextAsset;
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


      target.astarData.cell_size = EditorGUILayout.Vector2Field("cell_size", target.astarData.cell_size);

      using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
      {
        EditorGUILayout.LabelField("Bounds:", EditorStyles.boldLabel);
        using (new EditorGUIUtilityLabelWidthScope(80))
        {
          using (var check = new EditorGUIBeginChangeCheckScope())
          {
            target.astarData.is_enable_edit_outside_bounds = EditorGUILayout.Toggle("允许在边界外编辑?",
              target.astarData.is_enable_edit_outside_bounds);
            using (new EditorGUILayoutBeginHorizontalScope())
            {
              target.astarData.min_grid_x = EditorGUILayout.IntField("Left", target.astarData.min_grid_x);
              target.astarData.min_grid_y = EditorGUILayout.IntField("Bottom", target.astarData.min_grid_y);
            }

            using (new EditorGUILayoutBeginHorizontalScope())
            {
              target.astarData.max_grid_x = EditorGUILayout.IntField("Right", target.astarData.max_grid_x);
              target.astarData.max_grid_y = EditorGUILayout.IntField("Top", target.astarData.max_grid_y);
            }

            if (check.IsChanged)
              target.astarData.Resize();
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