
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
    private GameObjectLoader target
    {
      get { return base.target as GameObjectLoader; }
    }

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
        GameObject grid_gameObject = target.NewChildGameObject("Grid");
        grid_gameObject.AddComponent<Grid>();
        GameObject tilemap_gameObject = grid_gameObject.NewChildGameObject("Tilemap");
        tilemap_gameObject.AddComponent<Tilemap>();
        tilemap_gameObject.AddComponent<TilemapRenderer>();

      }
    }
  }
}