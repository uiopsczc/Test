#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class PrefabUtilityLoadPrefabContentsScope : IDisposable
	{
		private string _prefabPath;
		public GameObject prefab;

		public PrefabUtilityLoadPrefabContentsScope(string prefabPath)
		{
			prefab = PrefabUtility.LoadPrefabContents(prefabPath);
		}

		public void Dispose()
		{
			if (prefab == null) return;
			PrefabUtility.SaveAsPrefabAsset(prefab, _prefabPath);
			PrefabUtility.UnloadPrefabContents(prefab);
		}
	}
}
#endif