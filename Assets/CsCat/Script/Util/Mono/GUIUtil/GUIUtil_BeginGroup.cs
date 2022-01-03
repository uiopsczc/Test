using UnityEngine;

namespace CsCat
{
	public partial class GUIUtil
	{
		public static GUIBeginGroupScope BeginGroup(Rect position)
		{
			return new GUIBeginGroupScope(position);
		}

		public static GUIBeginGroupScope BeginGroup(Rect position, string text)
		{
			return new GUIBeginGroupScope(position, text);
		}

		public static GUIBeginGroupScope BeginGroup(Rect position, Texture image)
		{
			return new GUIBeginGroupScope(position, image);
		}

		public static GUIBeginGroupScope BeginGroup(Rect position, GUIContent content)
		{
			return new GUIBeginGroupScope(position, content);
		}

		public static GUIBeginGroupScope BeginGroup(Rect position, GUIStyle style)
		{
			return new GUIBeginGroupScope(position, style);
		}

		public static GUIBeginGroupScope BeginGroup(Rect position, string text, GUIStyle style)
		{
			return new GUIBeginGroupScope(position, text, style);
		}

		public static GUIBeginGroupScope BeginGroup(Rect position, Texture image, GUIStyle style)
		{
			return new GUIBeginGroupScope(position, image, style);
		}

		public static GUIBeginGroupScope BeginGroup(Rect position, GUIContent content, GUIStyle style)
		{
			return new GUIBeginGroupScope(position, content, style);
		}
	}
}