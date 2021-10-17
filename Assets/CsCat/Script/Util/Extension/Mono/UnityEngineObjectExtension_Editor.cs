#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
    /// <summary>
    /// AssetDatabase 需要包含后缀名称   path是相对于项目目录  如：Assets/xxxx
    /// </summary>
    public static class UnityEngineObjectExtension
    {
        public static void GetRefId(this Object asset)
        {
        }

        public static string GetName(this Object self)
        {
            if (self == null)
                return StringConst.String_null;
            return self.name;
        }

#if UNITY_EDITOR
        public static void AddObjectToAsset(this Object asset, Object obj)
        {
            AssetDatabase.AddObjectToAsset(obj, asset);
        }

        public static void ClearLabels(this Object asset)
        {
            AssetDatabase.ClearLabels(asset);
        }

        public static void SetLabels(this Object asset, params string[] labels)
        {
            AssetDatabase.SetLabels(asset, labels);
        }

        public static string[] GetLabels(this Object asset)
        {
            return asset.GetLabels();
        }

        public static bool IsAsset(this Object self)
        {
            return AssetDatabase.Contains(self);
        }

        public static string GetAssetPath(this Object asset)
        {
            if (!asset.IsMainAsset())
                return AssetDatabase.GetAssetPath(asset) + StringConst.String_Colon + asset.name;
            else
                return AssetDatabase.GetAssetPath(asset);
        }

        public static void CreateAssetAtPath(this Object asset, string path)
        {
            AssetDatabase.CreateAsset(asset, path);
        }

        public static void ExtractAssetAtPath(this Object asset, string path)
        {
            AssetDatabase.ExtractAsset(asset, path);
        }


        public static bool IsForeignAsset(this Object asset)
        {
            return AssetDatabase.IsForeignAsset(asset);
        }

        public static bool IsNativeAsset(this Object asset)
        {
            return AssetDatabase.IsNativeAsset(asset);
        }

        public static bool IsMainAsset(this Object asset)
        {
            return AssetDatabase.IsMainAsset(asset);
        }

        public static bool IsSubAsset(this Object asset)
        {
            return AssetDatabase.IsSubAsset(asset);
        }

        public static bool OpenAsset(this Object asset, int lineNumber = -1)
        {
            return AssetDatabase.OpenAsset(asset, lineNumber);
        }

        public static bool OpenAsset(this Object[] assets)
        {
            return AssetDatabase.OpenAsset(assets);
        }

        public static string GetGUID(this Object asset)
        {
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string guid, out long localId);
            return guid;
        }


        public static long GetLocalFileId(this Object asset)
        {
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string guid, out long localId);
            return localId;
        }

        public static Texture2D GetAssetPreview(this Object asset)
        {
            return AssetPreview.GetAssetPreview(asset);
        }


        public static Texture2D GetMiniThumbnail(this Object asset)
        {
            return AssetPreview.GetMiniThumbnail(asset);
        }
#endif
    }
}