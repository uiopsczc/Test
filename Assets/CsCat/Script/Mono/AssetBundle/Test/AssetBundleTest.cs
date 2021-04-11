using System.Collections;
using UnityEngine;

namespace CsCat
{
  public class AssetBundleTest : MonoBehaviour
  {
    public void Start()
    {
      if (Application.isEditor)
        EditorModeConst.Is_Editor_Mode = false;
      StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
      yield return Client.instance.assetBundleUpdater.CheckUpdate();
      yield return Client.instance.assetBundleManager.Initialize();
      yield return LuaRequireLoader.LoadLuaFiles();
    }
  }
}