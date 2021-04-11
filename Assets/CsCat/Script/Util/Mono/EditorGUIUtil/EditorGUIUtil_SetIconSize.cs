#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUIUtil
  {
    public EditorGUISetIconSizeScope SetIconSize(Vector2 size_new)
    {
      return new EditorGUISetIconSizeScope(size_new);
    }
  }
}
#endif