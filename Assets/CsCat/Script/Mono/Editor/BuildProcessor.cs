using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace CsCat
{
  public class BuildProcessor : IPreprocessBuild, IPostprocessBuildWithReport
  {
    public int callbackOrder { get; }
    private bool org_is_editor_mode;

    public void OnPreprocessBuild(BuildTarget target, string path)
    {
      org_is_editor_mode = EditorModeConst.Is_Editor_Mode;
      EditorModeConst.Is_Editor_Mode = false;
    }


    public void OnPostprocessBuild(BuildReport report)
    {
      EditorModeConst.Is_Editor_Mode = org_is_editor_mode;
    }
  }
}