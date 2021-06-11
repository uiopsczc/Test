#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class PrefabUtilityLoadPrefabContentsScope : IDisposable
  {
    private string prefab_path;
    public GameObject prefab;

    public PrefabUtilityLoadPrefabContentsScope(string prefab_path)
    {
      prefab = PrefabUtility.LoadPrefabContents(prefab_path);
    }
    
    public void Dispose()
    {
      if (prefab != null)
      {
        PrefabUtility.SaveAsPrefabAsset(prefab, prefab_path);
        PrefabUtility.UnloadPrefabContents(prefab);
      }
    }
  }
}
#endif