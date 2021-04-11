using UnityEngine;

namespace CsCat
{
  public partial class GUIUtil
  {
    public static GUIZoomGroupScope ZoomGroup(Rect gui_rect, float zoom_scale)
    {
      return new GUIZoomGroupScope(gui_rect, zoom_scale);
    }
  }
}