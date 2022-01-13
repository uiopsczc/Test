#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class EditorIconTexture
	{
		static Dictionary<string, Texture2D> iconTextureCacheDict = new Dictionary<string, Texture2D>();


		public static int count => iconTextureCacheDict.Count;

		public static Texture2D Get(string name)
		{
			if (iconTextureCacheDict.ContainsKey(name))
				return iconTextureCacheDict[name];

			Texture2D texture = (Texture2D)EditorGUIUtility.Load(name);
			iconTextureCacheDict[name] = texture;
			return texture;
		}

		public static Texture2D GetCustom(string name)
		{
			if (iconTextureCacheDict.ContainsKey(name))
				return iconTextureCacheDict[name];

			Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(string.Format("Assets/Editor/EditorExtensions/EditorTextures/{0}.png", name));
			iconTextureCacheDict.Add(name, texture);
			return texture;
		}


		public static Texture2D GetSystem(EditorIconTextureType editorIconTextureType)
		{
			string name = EditorIconTextureConst.IconTextureNames[(int)editorIconTextureType];
			if (iconTextureCacheDict.ContainsKey(name))
				return iconTextureCacheDict[name];

			Texture2D texture = EditorGUIUtility.FindTexture(name);
			iconTextureCacheDict.Add(name, texture);
			return texture;
		}

	}
}
#endif




