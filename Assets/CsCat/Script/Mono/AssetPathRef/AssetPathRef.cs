#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CsCat
{
	public class AssetPathRef
	{
		public string assetPath;
		public string guid;
		public long refId;

		public AssetPathRef(long refId, string assetPath = null, string guid = null)
		{
			this.refId = refId;
			this.assetPath = assetPath;
			if (this.assetPath == null && guid != null)
			{
#if UNITY_EDITOR
				this.assetPath = AssetDatabase.GUIDToAssetPath(guid);
#endif
			}

			this.guid = guid;
			if (this.guid == null && assetPath != null)
			{
#if UNITY_EDITOR
				this.guid = AssetDatabase.AssetPathToGUID(assetPath);
				;
#endif
			}
		}

		public bool Refresh()
		{
#if UNITY_EDITOR
			this.assetPath = AssetDatabase.GUIDToAssetPath(guid);
			if (AssetDatabase.LoadAssetAtPath(assetPath, typeof(object)) == null)
				return false;

#endif
			return true;
		}

	}
}




