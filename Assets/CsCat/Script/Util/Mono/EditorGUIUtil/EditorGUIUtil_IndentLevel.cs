#if UNITY_EDITOR
namespace CsCat
{
	public partial class EditorGUIUtil
	{
		public static EditorGUIIndentLevelScope IndentLevel(int add = 1)
		{
			return new EditorGUIIndentLevelScope(add);
		}
	}
}
#endif