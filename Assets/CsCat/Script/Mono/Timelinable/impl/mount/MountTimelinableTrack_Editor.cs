#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class MountTimelinableTrack
  {
    public override void DrawGUISetting_Detail()
    {
      transform = EditorGUILayout.ObjectField("transform", transform, typeof(Transform), true) as Transform;
    }
  }
}
#endif



