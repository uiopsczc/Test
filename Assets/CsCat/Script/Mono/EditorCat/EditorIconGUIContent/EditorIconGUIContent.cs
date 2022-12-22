#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class EditorIconGUIContent
	{
		static Dictionary<string, GUIContent> _iconGUIContentCacheDict = new Dictionary<string, GUIContent>();

		public static int count => _iconGUIContentCacheDict.Count;

		public static GUIContent Custom_GUIContent => Get("CustomContent");


		public static GUIContent Get(string name, string text, string tips)
		{
			if (_iconGUIContentCacheDict.ContainsKey(name))
				return _iconGUIContentCacheDict[name];
			GUIContent guiContent = new GUIContent(text, EditorIconTexture.GetCustom(name), tips);
			_iconGUIContentCacheDict[name] = guiContent;
			return guiContent;
		}

		public static GUIContent Get(string name, string text)
		{
			if (_iconGUIContentCacheDict.ContainsKey(name))
				return _iconGUIContentCacheDict[name];
			GUIContent guiContent = new GUIContent(text, EditorIconTexture.GetCustom(name));
			_iconGUIContentCacheDict[name] = guiContent;
			return guiContent;
		}

		public static GUIContent Get(string name)
		{
			if (_iconGUIContentCacheDict.ContainsKey(name))
				return _iconGUIContentCacheDict[name];
			GUIContent guiContent = new GUIContent(EditorIconTexture.GetCustom(name));
			_iconGUIContentCacheDict.Add(name, guiContent);
			return guiContent;
		}

		public static GUIContent Get(EditorIconTextureType editorIconTextureType)
		{
			if (_iconGUIContentCacheDict.ContainsKey(editorIconTextureType.ToString()))
				return _iconGUIContentCacheDict[editorIconTextureType.ToString()];
			GUIContent guiContent = new GUIContent(EditorIconTexture.GetSystem(editorIconTextureType));
			_iconGUIContentCacheDict.Add(editorIconTextureType.ToString(), guiContent);
			return guiContent;
		}

		public static GUIContent GetSystem(EditorIconGUIContentType editorIconGUIContentType)
		{
			string name = EditorIconGUIContentConst.IconGUIContentNames[(int) editorIconGUIContentType];
			if (_iconGUIContentCacheDict.ContainsKey(name))
				return _iconGUIContentCacheDict[name];

			GUIContent guiContent = EditorGUIUtility.IconContent(name);
			_iconGUIContentCacheDict[name] = guiContent;
			return guiContent;
		}
	}
}
#endif