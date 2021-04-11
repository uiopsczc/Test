using UnityEngine;

namespace CsCat
{
  public partial class GUIUtil
  {
    public static GUILabelAlignScope LabelAlign(TextAnchor a)
    {
      return new GUILabelAlignScope(a);
    }
  }
}