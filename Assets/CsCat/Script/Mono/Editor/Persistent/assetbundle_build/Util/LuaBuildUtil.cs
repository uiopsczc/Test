using System.IO;
using UnityEditor;

namespace CsCat
{
  public class LuaBuildUtil
  {
    public static void PreBuild()
    {
      var luaFiles = Directory.GetFiles(FilePathConst.AssetsPath, "*.lua.txt", SearchOption.AllDirectories);
      foreach (var luaFile in luaFiles)
      {
        var luaPath = FilePathConst.GetPathRelativeTo(luaFile, FilePathConst.ProjectPath).Replace("\\", "/");
        var importer = AssetImporter.GetAtPath(luaPath);
        importer.assetBundleName = BuildConst.LuaBundle_Prefix_Name + luaPath.DirPath().Replace("/", "_");
        importer.assetBundleVariant = BuildConst.AssetBundle_Suffix.Substring(1);

        importer.SaveAndReimport();
      }

      AssetDatabase.Refresh();
    }
  }
}