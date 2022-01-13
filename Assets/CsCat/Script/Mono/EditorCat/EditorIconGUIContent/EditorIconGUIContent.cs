#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class EditorIconGUIContent
	{
		static Dictionary<string, GUIContent> iconGUIContentCacheDict = new Dictionary<string, GUIContent>();

		public static int count => iconGUIContentCacheDict.Count;

		public static GUIContent Custom_GUIContent => Get("CustomContent");


		public static GUIContent Get(string name, string text, string tips)
		{
			if (iconGUIContentCacheDict.ContainsKey(name))
				return iconGUIContentCacheDict[name];
			GUIContent guiContent = new GUIContent(text, EditorIconTexture.GetCustom(name), tips);
			iconGUIContentCacheDict[name] = guiContent;
			return guiContent;
		}

		public static GUIContent Get(string name, string text)
		{
			if (iconGUIContentCacheDict.ContainsKey(name))
				return iconGUIContentCacheDict[name];
			GUIContent guiContent = new GUIContent(text, EditorIconTexture.GetCustom(name));
			iconGUIContentCacheDict[name] = guiContent;
			return guiContent;
		}

		public static GUIContent Get(string name)
		{
			if (iconGUIContentCacheDict.ContainsKey(name))
				return iconGUIContentCacheDict[name];
			GUIContent guiContent = new GUIContent(EditorIconTexture.GetCustom(name));
			iconGUIContentCacheDict.Add(name, guiContent);
			return guiContent;
		}

		public static GUIContent Get(EditorIconTextureType editorIconTextureType)
		{
			if (iconGUIContentCacheDict.ContainsKey(editorIconTextureType.ToString()))
				return iconGUIContentCacheDict[editorIconTextureType.ToString()];
			GUIContent guiContent = new GUIContent(EditorIconTexture.GetSystem(editorIconTextureType));
			iconGUIContentCacheDict.Add(editorIconTextureType.ToString(), guiContent);
			return guiContent;
		}

		public static GUIContent GetSystem(EditorIconGUIContentType editorIconGUIContentType)
		{
			string name = EditorIconGUIContentConst.IconGUIContentNames[(int) editorIconGUIContentType];
			if (iconGUIContentCacheDict.ContainsKey(name))
				return iconGUIContentCacheDict[name];

			GUIContent guiContent = EditorGUIUtility.IconContent(name);
			iconGUIContentCacheDict[name] = guiContent;
			return guiContent;
		}
	}
}
#endif