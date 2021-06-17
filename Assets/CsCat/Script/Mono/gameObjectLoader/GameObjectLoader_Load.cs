using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace CsCat
{
  public partial class GameObjectLoader
  {
    public void LoadFromPath(string path)
    {
      resLoad.GetOrLoadAsset(path, assetCat =>
      {
        string content_json = assetCat.Get<TextAsset>().text;
        GameObjectLoader.instance.Load(content_json);
      }, null, null, this);
    }


    public void Load(string content_json)
    {
      Clear();
      Hashtable dict = MiniJson.JsonDecode(content_json) as Hashtable;
      Load_ChildList(dict.Get<ArrayList>("child_list"), this.gameObject.transform);
    }

    private void Load_ChildList(ArrayList child_list, Transform parent_transform)
    {
      foreach (Hashtable child_hashtable in child_list)
        Load_Child(child_hashtable, parent_transform);
    }

    private void Load_Child(Hashtable child_hashtable, Transform parent_transform)
    {
      long prefab_ref_id = child_hashtable.Get<long>("prefab_ref_id");
      if (prefab_ref_id != 0)
      {
        string assetPath = prefab_ref_id.GetAssetPathByRefId();
        resLoad.GetOrLoadAsset(assetPath, assetCat =>
        {
          Object prefab = assetCat.Get();
          GameObject clone = null;
          if (Application.isPlaying)
            clone = GameObject.Instantiate(prefab, parent_transform) as GameObject;
          else
          {
#if UNITY_EDITOR
            clone = EditorUtility.InstantiatePrefab(prefab) as GameObject;
            clone.transform.SetParent(parent_transform);
#endif
          }
          _Load(clone, child_hashtable, parent_transform, true);
        }, null, null, this);
        return; //如果是prefab的话，不用检查子孩子节点了
      }

      GameObject child_gameObject = new GameObject();
      _Load(child_gameObject, child_hashtable, parent_transform, false);

    }


    private void _Load(GameObject child_gameObject, Hashtable child_hashtable, Transform parent_transform, bool is_prefab)
    {
      child_gameObject.transform.SetParent(parent_transform);
      child_gameObject.name = child_hashtable.Get<string>("name");
      child_gameObject.transform.LoadSerializeHashtable(child_hashtable.Get<Hashtable>("Transform_hashtable"));

      if (child_hashtable.Get<Hashtable>("Tilemap_hashtable") != null)
      {
        Tilemap tilemap = GetOrAddComponent<Tilemap>(child_gameObject, is_prefab);
        tilemap.LoadSerializeHashtable(child_hashtable.Get<Hashtable>("Tilemap_hashtable"), resLoad);
      }

      List<string> except_list = new List<string>() { "Transform_hashtable", "Tilemap_hashtable" };
      foreach (var _child_hashtable_key in child_hashtable.Keys)
      {
        string child_hashtable_key = _child_hashtable_key.ToString();
        if (child_hashtable_key.IsFirstLetterUpper() && !except_list.Contains(child_hashtable_key))
        {
          string component_type_name = child_hashtable_key.Substring(0, child_hashtable_key.IndexOf("_"));
          Component component = GetOrAddComponent(child_gameObject, TypeUtil.GetType(component_type_name), is_prefab);
          component.InvokeExtensionMethod("LoadSerializeHashtable", true, child_hashtable.Get<Hashtable>(_child_hashtable_key));
        }
      }

      if (!is_prefab)//如果是预设则不用递归子节点
      {
        ArrayList child_list = child_hashtable.Get<ArrayList>("child_list");
        if (child_list != null)
          Load_ChildList(child_list, child_gameObject.transform);
      }
    }


    private T GetOrAddComponent<T>(GameObject gameObject, bool is_prefab) where T : Component
    {
      return !is_prefab ? gameObject.AddComponent<T>() : gameObject.GetComponent<T>();
    }

    private Component GetOrAddComponent(GameObject gameObject, Type component_type, bool is_prefab)
    {
      return !is_prefab ? gameObject.AddComponent(component_type) : gameObject.GetComponent(component_type);
    }

  }
}




