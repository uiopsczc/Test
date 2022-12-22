#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class EditorIconTexture
	{
		static Dictionary<string, Texture2D> _iconTextureCacheDict = new Dictionary<string, Texture2D>();


		public static int count => _iconTextureCacheDict.Count;

		public static Texture2D Get(string name)
		{
			if (_iconTextureCacheDict.ContainsKey(name))
				return _iconTextureCacheDict[name];

			Texture2D texture = (Texture2D)EditorGUIUtility.Load(name);
			_iconTextureCacheDict[name] = texture;
			return texture;
		}

		public static Texture2D GetCustom(string name)
		{
			if (_iconTextureCacheDict.ContainsKey(name))
				return _iconTextureCacheDict[name];

			Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(string.Format("Assets/Editor/EditorExtensions/EditorTextures/{0}.png", name));
			_iconTextureCacheDict.Add(name, texture);
			return texture;
		}


		public static Texture2D GetSystem(EditorIconTextureType editorIconTextureType)
		{
			string name = EditorIconTextureConst.IconTextureNames[(int)editorIconTextureType];
			if (_iconTextureCacheDict.ContainsKey(name))
				return _iconTextureCacheDict[name];

			Texture2D texture = EditorGUIUtility.FindTexture(name);
			_iconTextureCacheDict.Add(name, texture);
			return texture;
		}

	}
}
#endif




