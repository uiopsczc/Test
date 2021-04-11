using System.Collections;
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace CsCat
{
  public partial class GameObjectLoader
  {
    private Hashtable ref_id_hashtable = new Hashtable();
#if UNITY_EDITOR
    public void Save()
    {
      ref_id_hashtable.Clear();
      Hashtable dict = new Hashtable();
      dict["child_list"]=GetSave_ChildList(this.transform);
      string content = MiniJson.JsonEncode(dict);
      string file_path = textAsset.GetAssetPath().WithRootPath(FilePathConst.ProjectPath);
      StdioUtil.WriteTextFile(file_path, content);
      AssetPathRefManager.instance.Save();
      AssetDatabase.Refresh();
      LogCat.log("保存完成");
    }


    private ArrayList GetSave_ChildList(Transform parent_transform)
    {
      int child_count = parent_transform.childCount;
      ArrayList tilemap_list = new ArrayList();
      for (int i = 0; i < child_count; i++)
      {
        Transform child_transform = parent_transform.GetChild(i);
        Hashtable child_hashtable = GetSave_Child(child_transform);
        tilemap_list.Add(child_hashtable);
      }

      return tilemap_list;
    }

    private Hashtable GetSave_Child(Transform child_transform)
    {
      Hashtable hashtable = new Hashtable();
      hashtable["name"] = child_transform.name;
      hashtable["Transform_hashtable"] = child_transform.GetSerializeHashtable();

      Object prefab = EditorUtility.GetPrefabParent(child_transform.gameObject);
      bool is_prefab = prefab != null;
      if (is_prefab)
      {
        long prefab_ref_id = AssetPathRefManager.instance.GetRefIdByGuid(prefab.GetGUID());
        hashtable["prefab_ref_id"] = prefab_ref_id;
        ref_id_hashtable[prefab_ref_id] = true;
      }
      //如果是预设则不用递归子节点
      if (!is_prefab&&child_transform.childCount > 0)
        hashtable["child_list"] = GetSave_ChildList(child_transform);

      List<Type> except_list = new List < Type > { typeof(Transform), typeof(Tilemap)};
      foreach (var component in child_transform.GetComponents<Component>())
      {
        if (!except_list.Contains(component.GetType()))
        {
          var component_hashtable = component.InvokeExtensionMethod<Hashtable>("GetSerializeHashtable") ?? new Hashtable();
          string key = string.Format("{0}_hashtable", component.GetType().FullName);
          hashtable[key] = component_hashtable;
        }
      }
      Tilemap tilemap = child_transform.GetComponent<Tilemap>();
      if (tilemap != null)
        hashtable["Tilemap_hashtable"] = tilemap.GetSerializeHashtable(ref_id_hashtable);

      return hashtable;
    }




#endif

  }
}




