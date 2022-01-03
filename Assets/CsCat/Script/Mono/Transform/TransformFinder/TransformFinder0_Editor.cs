#if UNITY_EDITOR
using UnityEditor;

namespace CsCat
{
	public partial class TransformFinder0
	{
		public override void DrawGUI()
		{
			using (new EditorGUILabelWidthScope(32))
			{
				path = EditorGUILayout.TextField("path", path);
			}
		}
	}
}
#endif