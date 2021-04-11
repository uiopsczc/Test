
#if UNITY_EDITOR
using UnityEditor;

namespace CsCat
{
  public partial class EditorWindowUtil
  {
    public static EditorWindowBeginWindowsScope BeginWindows(EditorWindow self)
    {
      return new EditorWindowBeginWindowsScope(self);
    }

  }
}
#endif
