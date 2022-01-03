using UnityEngine;

namespace CsCat
{
	public partial class GUIUtil
	{
		public static GUIZoomGroupScope ZoomGroup(Rect guiRect, float zoomScale)
		{
			return new GUIZoomGroupScope(guiRect, zoomScale);
		}
	}
}