using System.IO;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  ///   CZM工具菜单
  /// </summary>
  public partial class CZMToolMenu
  {
    [MenuItem(CZMToolConst.MenuRoot + "AssetPathRef/Save")]
    public static void AssetPathRefSave()
    {
      AssetPathRefManager.instance.Save();
      AssetDatabase.Refresh();
    }

    [MenuItem(CZMToolConst.MenuRoot + "AssetPathRef/Add")]
    public static void AssetPathRefAdd()
    {
      foreach (var selected_obj in Selection.objects)
        AssetPathRefManager.instance.Add(selected_obj.GetGUID());
      AssetPathRefManager.instance.Save();
      AssetDatabase.Refresh();
    }

    [MenuItem(CZMToolConst.MenuRoot + "AssetPathRef/AddByFolder")]
    public static void AssetPathRefAddByFolder()
    {
      Object[] selected_assets = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
      foreach (Object selected_asset in selected_assets)
      {
        var assetPath = AssetDatabase.GetAssetPath(selected_asset);
        if (Directory.Exists(assetPath))
          continue;
        AssetPathRefManager.instance.Add(AssetDatabase.AssetPathToGUID(assetPath));
      }

      AssetPathRefManager.instance.Save();
      AssetDatabase.Refresh();
    }


    [MenuItem(CZMToolConst.MenuRoot + "AssetPathRef/Clear All")]
    public static void AssetPathRefClearAll()
    {
      AssetPathRefManager.instance.ClearAll();
      AssetDatabase.Refresh();
    }
  }
}