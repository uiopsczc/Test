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

		public static T CreateAsset<T>(string path, Action<T> onCreateCallback = null) where T : ScriptableObject
		{
			var asset = ScriptableObject.CreateInstance<T>();
			path = path.WithoutRootPath(FilePathConst.ProjectPath);
			var dir = path.DirPath();
			if (!dir.IsNullOrEmpty())
				StdioUtil.CreateDirectoryIfNotExist(dir);
			var fileExtensionName = Path.GetExtension(path);
			if (!StringConst.String_Asset_Extension.Equals(fileExtensionName))
				path = path.Replace(fileExtensionName, StringConst.String_Asset_Extension);
			onCreateCallback?.Invoke(asset);

			EditorUtility.SetDirty(asset);
			AssetDatabase.CreateAsset(asset, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			return asset;
		}


#endif
	}
}