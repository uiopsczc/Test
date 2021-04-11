using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class MountTimelinableTestEditorWindow
  {

    void DrawLeft()
    {
      using (new GUILayout.AreaScope(this.resizableRects.rects[0]))
      {
        GUILayout.Space(20);
        using (new GUILayoutBeginVerticalScope(EditorStyles.helpBox))
        {
          TimelinableEditorWindowUtil.DrawGUISetting_Sequence(ref sequence);
          if (sequence == null)
            return;
          sequence.DrawGUISetting_Detail();
        }
        if (sequence.tracks.Length <= 0)
          return;
        sequence.DrawGUISetting_Tracks(timelineRect.play_time);
      }
    }
  }
}