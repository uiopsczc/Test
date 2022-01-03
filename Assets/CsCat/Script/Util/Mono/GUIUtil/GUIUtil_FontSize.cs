using UnityEngine;

namespace CsCat
{
	public partial class GUIUtil
	{
		public static GUIFontSizeScope FontSize(float size, params GUIStyle[] styles)
		{
			return new GUIFontSizeScope(size, styles);
		}
	}
}