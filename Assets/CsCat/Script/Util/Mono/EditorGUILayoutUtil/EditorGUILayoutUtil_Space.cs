#if UNITY_EDITOR
using UnityEditor;

namespace CsCat
{
	public partial class EditorGUILayoutUtil
	{
		private static void SpaceCount(int count)
		{
			for (var i = 0; i < count; i++) EditorGUILayout.Space();
		}

		public static void Space(int count = 1, bool isHorizontal = false)
		{
			if (isHorizontal)
				using (BeginHorizontal())
				{
					SpaceCount(count);
				}
			else
				SpaceCount(count);
		}
	}
}
#endif