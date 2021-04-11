using UnityEditor;

namespace CsCat
{
  public class LuaAssetProcessEditor : AssetPostprocessor
  {

    static void OnPostprocessAllAssets(
      string[] importedAssets,
      string[] deletedAssets,
      string[] movedAssets,
      string[] movedFromAssetPaths)
    {
      foreach (string path in importedAssets)
      {
        if (path.Contains(".lua.txt"))
        {
          return;
        }
      }
    }

  }
}