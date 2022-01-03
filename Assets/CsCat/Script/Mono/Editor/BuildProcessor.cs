using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CsCat
{
	public class BuildProcessor : IPreprocessBuild, IPostprocessBuildWithReport
	{
		public int callbackOrder { get; }
		private bool orgIsEditorMode;

		public void OnPreprocessBuild(BuildTarget target, string path)
		{
			orgIsEditorMode = EditorModeConst.IsEditorMode;
			EditorModeConst.IsEditorMode = false;
		}


		public void OnPostprocessBuild(BuildReport report)
		{
			EditorModeConst.IsEditorMode = orgIsEditorMode;
		}
	}
}