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
			_resLoad.GetOrLoadAsset(path, assetCat =>
			{
				string contentJson = assetCat.Get<TextAsset>().text;
				GameObjectLoader.instance.Load(contentJson);
			}, null, null, this);
		}


		public void Load(string contentJson)
		{
			Clear();
			Hashtable dict = MiniJson.JsonDecode(contentJson) as Hashtable;
			_LoadChildList(dict.Get<ArrayList>("childList"), this.gameObject.transform);
		}

		private void _LoadChildList(ArrayList childList, Transform parentTransform)
		{
			for (var i = 0; i < childList.Count; i++)
			{
				var childHashtable = (Hashtable) childList[i];
				_LoadChild(childHashtable, parentTransform);
			}
		}

		private void _LoadChild(Hashtable childHashtable, Transform parentTransform)
		{
			long prefabRefId = childHashtable.Get<long>("prefabRefId");
			if (prefabRefId != 0)
			{
				string assetPath = prefabRefId.GetAssetPathByRefId();
				_resLoad.GetOrLoadAsset(assetPath, assetCat =>
				{
					Object prefab = assetCat.Get();
					GameObject clone = null;
					if (Application.isPlaying)
						clone = GameObject.Instantiate(prefab, parentTransform) as GameObject;
					else
					{
#if UNITY_EDITOR
						clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
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
			childGameObject.transform.LoadSerializeHashtable(childHashtable.Get<Hashtable>("TransformHashtable"));

			if (childHashtable.Get<Hashtable>("TilemapHashtable") != null)
			{
				Tilemap tilemap = _GetOrAddComponent<Tilemap>(childGameObject, isPrefab);
				tilemap.LoadSerializeHashtable(childHashtable.Get<Hashtable>("TilemapHashtable"), _resLoad);
			}

			List<string> except_list = new List<string>() {"TransformHashtable", "TilemapHashtable"};
			foreach (DictionaryEntry kv in childHashtable)
			{
				var curChildHashtableKey = kv.Key;
				string childHashtableKey = curChildHashtableKey.ToString();
				if (childHashtableKey.IsFirstLetterUpper() && !except_list.Contains(childHashtableKey))
				{
					string componentTypeName = childHashtableKey.Substring(0, childHashtableKey.IndexOf("_"));
					UnityEngine.Component component = _GetOrAddComponent(childGameObject, TypeUtil.GetType(componentTypeName),
						isPrefab);
					component.InvokeExtensionMethod("LoadSerializeHashtable", true,
						childHashtable.Get<Hashtable>(curChildHashtableKey));
				}
			}

			if (!isPrefab) //如果是预设则不用递归子节点
			{
				ArrayList childList = childHashtable.Get<ArrayList>("childList");
				if (childList != null)
					_LoadChildList(childList, childGameObject.transform);
			}
		}


		private T _GetOrAddComponent<T>(GameObject gameObject, bool isPrefab) where T : UnityEngine.Component
		{
			return !isPrefab ? gameObject.AddComponent<T>() : gameObject.GetComponent<T>();
		}

		private UnityEngine.Component _GetOrAddComponent(GameObject gameObject, Type componentType, bool isPrefab)
		{
			return !isPrefab ? gameObject.AddComponent(componentType) : gameObject.GetComponent(componentType);
		}
	}
}