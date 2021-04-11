#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CsCat
{
  public class AssetPathRef
  {
    public string asset_path;
    public string guid;
    public long ref_id;

    public AssetPathRef(long ref_id, string asset_path = null, string guid = null)
    {
      this.ref_id = ref_id;
      this.asset_path = asset_path;
      if (this.asset_path == null && guid != null)
      {
#if UNITY_EDITOR
        this.asset_path = AssetDatabase.GUIDToAssetPath(guid);
#endif
      }

      this.guid = guid;
      if (this.guid == null && asset_path != null)
      {
#if UNITY_EDITOR
        this.guid = AssetDatabase.AssetPathToGUID(asset_path);
        ;
#endif
      }
    }

    public bool Refresh()
    {
#if UNITY_EDITOR
      this.asset_path = AssetDatabase.GUIDToAssetPath(guid);
      if (AssetDatabase.LoadAssetAtPath(asset_path, typeof(object)) == null)
        return false;

#endif
      return true;
    }

  }
}




