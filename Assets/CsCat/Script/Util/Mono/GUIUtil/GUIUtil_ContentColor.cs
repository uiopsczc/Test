using UnityEngine;

namespace CsCat
{
	public partial class GUIUtil
	{
		public static GUIContentColorScope ContentColor(Color newColor)
		{
			return new GUIContentColorScope(newColor);
		}
	}
}