using UnityEditor;

namespace CsCat
{
  [CustomEditor(typeof(AStarMonoBehaviour))]
  public partial class AStarEditor : Editor
  {
    private AStarMonoBehaviour target
    {
      get { return base.target as AStarMonoBehaviour; }
    }

    private Tool org_editor_tool_selected;
    private AStarBrush brush = new AStarBrush();

    void OnEnable()
    {
      org_editor_tool_selected = Tools.current;
      Tools.current = Tool.None;

      brush.astarMonoBehaviour = target;
    }

    void OnDisable()
    {
      Tools.current = org_editor_tool_selected;
    }
  }
}
