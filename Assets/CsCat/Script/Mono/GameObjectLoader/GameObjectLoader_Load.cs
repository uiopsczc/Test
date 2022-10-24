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
				string contentJson = assetCat.Get<TextAsset>().text;
				GameObjectLoader.instance.Load(contentJson);
			}, null, null, this);
		}


		public void Load(string content_json)
		{
			Clear();
			Hashtable dict = MiniJson.JsonDecode(content_json) as Hashtable;
			Load_ChildList(dict.Get<ArrayList>("child_list"), this.gameObject.transform);
		}

		private void Load_ChildList(ArrayList childList, Transform parentTransform)
		{
			for (var i = 0; i < childList.Count; i++)
			{
				var childHashtable = (Hashtable) childList[i];
				Load_Child(childHashtable, parentTransform);
			}
		}

		private void Load_Child(Hashtable childHashtable, Transform parentTransform)
		{
			long prefabRefId = childHashtable.Get<long>("prefab_ref_id");
			if (prefabRefId != 0)
			{
				string assetPath = prefabRefId.GetAssetPathByRefId();
				resLoad.GetOrLoadAsset(assetPath, assetCat =>
				{
					Object prefab = assetCat.Get();
					GameObject clone = null;
					if (Application.isPlaying)
						clone = GameObject.Instantiate(prefab, parentTransform) as GameObject;
					else
					{
#if UNITY_EDITOR
						clone = EditorUtility.InstantiatePrefab(prefab) as GameObject;
						clone.transform.SetParent(parentTransform);
#endif
					}

					_Load(clone, childHashtable, parentTransform, true);
				}, null, null, this);
				return; //如果是prefab的话，不用检查子孩子节点了
			}

			GameObject childGameObject = new GameObject();
			_Load(childGameObject, childHashtable, parentTransform, false);
		}


		private void _Load(GameObject childGameObject, Hashtable childHashtable, Transform parentTransform,
			bool isPrefab)
		{
			childGameObject.transform.SetParent(parentTransform);
			childGameObject.name = childHashtable.Get<string>("name");
			childGameObject.transform.LoadSerializeHashtable(childHashtable.Get<Hashtable>("Transform_hashtable"));

			if (childHashtable.Get<Hashtable>("Tilemap_hashtable") != null)
			{
				Tilemap tilemap = GetOrAddComponent<Tilemap>(childGameObject, isPrefab);
				tilemap.LoadSerializeHashtable(childHashtable.Get<Hashtable>("Tilemap_hashtable"), resLoad);
			}

			List<string> except_list = new List<string>() {"Transform_hashtable", "Tilemap_hashtable"};
			foreach (var curChildHashtableKey in childHashtable.Keys)
			{
				string childHashtableKey = curChildHashtableKey.ToString();
				if (childHashtableKey.IsFirstLetterUpper() && !except_list.Contains(childHashtableKey))
				{
					string componentTypeName = childHashtableKey.Substring(0, childHashtableKey.IndexOf("_"));
					UnityEngine.Component component = GetOrAddComponent(childGameObject, TypeUtil.GetType(componentTypeName),
						isPrefab);
					component.InvokeExtensionMethod("LoadSerializeHashtable", true,
						childHashtable.Get<Hashtable>(curChildHashtableKey));
				}
			}

			if (!isPrefab) //如果是预设则不用递归子节点
			{
				ArrayList childList = childHashtable.Get<ArrayList>("child_list");
				if (childList != null)
					Load_ChildList(childList, childGameObject.transform);
			}
		}


		private T GetOrAddComponent<T>(GameObject gameObject, bool isPrefab) where T : UnityEngine.Component
		{
			return !isPrefab ? gameObject.AddComponent<T>() : gameObject.GetComponent<T>();
		}

		private UnityEngine.Component GetOrAddComponent(GameObject gameObject, Type componentType, bool isPrefab)
		{
			return !isPrefab ? gameObject.AddComponent(componentType) : gameObject.GetComponent(componentType);
		}
	}
}