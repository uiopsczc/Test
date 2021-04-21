
#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
  public class ScriptableObjectUtil
  {

#if UNITY_EDITOR
    //    public static T CreateAsset<T>() where T : ScriptableObject
    //    {
    //      T asset = ScriptableObject.CreateInstance<T>();
    //      string path = AssetDatabase.GetAssetPath(Selection.activeObject);
    //      if (path == "")
    //        path = "Assets";
    //      else if (Path.GetExtension(path) != "")
    //        path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
    //      string object_name = typeof(T).ToString();
    //      string object_extension = Path.GetExtension(object_name);
    //      if (!string.IsNullOrEmpty(object_extension))
    //        object_name = object_extension.Remove(0, 1);
    //      string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + object_name + ".asset");
    //      AssetDatabase.CreateAsset(asset, assetPathAndName);
    //      AssetDatabase.SaveAssets();
    //      Selection.activeObject = asset;
    //      AssetDatabase.Refresh();
    //      EditorUtility.FocusProjectWindow();
    //      return asset;
    //    }

    public static T CreateAsset<T>(string path, Action<T> on_create_callback = null) where T : ScriptableObject
    {
      T asset = ScriptableObject.CreateInstance<T>();
      path = path.WithoutRootPath(FilePathConst.ProjectPath);
      var dir = path.DirPath();
      if (!dir.IsNullOrEmpty())
        StdioUtil.CreateDirectoryIfNotExist(dir);
      string file_extension_name = Path.GetExtension(path);
      if (!file_extension_name.Equals(".asset"))
        path = path.Replace(file_extension_name, ".asset");
      on_create_callback?.Invoke(asset);

      EditorUtility.SetDirty(asset);
      AssetDatabase.CreateAsset(asset, path);
      AssetDatabase.SaveAssets();
      AssetDatabase.Refresh();
      return asset;
    }



#endif

  }
}