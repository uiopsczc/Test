using UnityEngine;

namespace CsCat
{
	public class GizmosUtil
	{
		public static GizmosColorScope Color(Color newColor)
		{
			return new GizmosColorScope(newColor);
		}
	}
}