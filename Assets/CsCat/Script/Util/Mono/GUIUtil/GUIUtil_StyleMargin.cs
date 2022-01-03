using UnityEngine;

namespace CsCat
{
	public partial class GUIUtil
	{
		public static GUIStyleMarginScope StyleMargin(GUIStyle style, RectOffset margin)
		{
			return new GUIStyleMarginScope(style, margin);
		}

		public static GUIStyleMarginScope StyleMargin(GUIStyle style, RectOffset margin, RectOffset padding)
		{
			return new GUIStyleMarginScope(style, margin, padding);
		}

		public static GUIStyleMarginScope StyleMargin(GUIStyle style, RectOffset margin, RectOffset padding,
			RectOffset overflow)
		{
			return new GUIStyleMarginScope(style, margin, padding, overflow);
		}
	}
}