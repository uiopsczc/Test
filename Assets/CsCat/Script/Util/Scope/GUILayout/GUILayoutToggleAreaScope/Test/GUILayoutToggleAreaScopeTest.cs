#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public static class GUILayoutToggleAreaScopeTest
	{
		private static readonly GUIToggleTween _toggleTween = new GUIToggleTween();

		public static void Test()
		{
			using (new GUILayoutToggleAreaScope(_toggleTween, "Chen"))
			{
				for (int i = 0; i < 20; i++)
					GUILayout.Label("cccc" + i);
			}

			GUILayout.Label("Good");
		}
	}
}
#endif