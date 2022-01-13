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
		private Hashtable refIdHashtable = new Hashtable();
#if UNITY_EDITOR
		public void Save()
		{
			refIdHashtable.Clear();
			Hashtable dict = new Hashtable();
			dict["child_list"] = GetSave_ChildList(this.transform);
			string content = MiniJson.JsonEncode(dict);
			string filePath = textAsset.GetAssetPath().WithRootPath(FilePathConst.ProjectPath);
			StdioUtil.WriteTextFile(filePath, content);
			AssetPathRefManager.instance.Save();
			AssetDatabase.Refresh();
			LogCat.log("保存完成");
		}


		private ArrayList GetSave_ChildList(Transform parent_transform)
		{
			int childCount = parent_transform.childCount;
			ArrayList tilemapList = new ArrayList();
			for (int i = 0; i < childCount; i++)
			{
				Transform childTransform = parent_transform.GetChild(i);
				Hashtable childHashtable = GetSave_Child(childTransform);
				tilemapList.Add(childHashtable);
			}

			return tilemapList;
		}

		private Hashtable GetSave_Child(Transform childTransform)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["name"] = childTransform.name;
			hashtable["Transform_hashtable"] = childTransform.GetSerializeHashtable();

			Object prefab = EditorUtility.GetPrefabParent(childTransform.gameObject);
			bool isPrefab = prefab != null;
			if (isPrefab)
			{
				long prefabRefId = AssetPathRefManager.instance.GetRefIdByGuid(prefab.GetGUID());
				hashtable["prefab_ref_id"] = prefabRefId;
				refIdHashtable[prefabRefId] = true;
			}
			//如果是预设则不用递归子节点
			if (!isPrefab && childTransform.childCount > 0)
				hashtable["child_list"] = GetSave_ChildList(childTransform);

			List<Type> exceptList = new List<Type> { typeof(Transform), typeof(Tilemap) };
			var components = childTransform.GetComponents<Component>();
			for (var i = 0; i < components.Length; i++)
			{
				var component = components[i];
				if (!exceptList.Contains(component.GetType()))
				{
					var componentHashtable = component.InvokeExtensionMethod<Hashtable>("GetSerializeHashtable") ??
					                          new Hashtable();
					string key = string.Format("{0}_hashtable", component.GetType().FullName);
					hashtable[key] = componentHashtable;
				}
			}

			Tilemap tilemap = childTransform.GetComponent<Tilemap>();
			if (tilemap != null)
				hashtable["Tilemap_hashtable"] = tilemap.GetSerializeHashtable(refIdHashtable);

			return hashtable;
		}




#endif

	}
}




