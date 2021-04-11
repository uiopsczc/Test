using System;
using UnityEngine;

namespace CsCat
{
  public partial class GUIUtil
  {
    public static void Box(Rect rect, string content, Color? background_color = null, bool is_outline = false,
      float border_size = 1)
    {
      using (new GUIBackgroundColorScope(background_color == null ? GUI.backgroundColor : background_color.Value))
      {
        GUI.Box(rect, content);
      }

      _Box_Border(rect, is_outline, border_size);
    }

    public static void Box(Rect rect, Texture image, Color? background_color = null, bool is_outline = false,
      float border_size = 1)
    {
      using (new GUIBackgroundColorScope(background_color == null ? GUI.backgroundColor : background_color.Value))
      {
        GUI.Box(rect, image);
      }

      _Box_Border(rect, is_outline, border_size);
    }

    public static void Box(Rect rect, GUIContent content, Color? background_color = null, bool is_outline = false,
      float border_size = 1)
    {
      using (new GUIBackgroundColorScope(background_color == null ? GUI.backgroundColor : background_color.Value))
      {
        GUI.Box(rect, content);
      }

      _Box_Border(rect, is_outline, border_size);
    }

    public static void Box(Rect rect, string content, GUIStyle style, Color? background_color = null,
      bool is_outline = false, float border_size = 1)
    {
      using (new GUIBackgroundColorScope(background_color == null ? GUI.backgroundColor : background_color.Value))
      {
        GUI.Box(rect, content, style);
      }

      _Box_Border(rect, is_outline, border_size);
    }

    public static void Box(Rect rect, Texture image, GUIStyle style, Color? background_color = null,
      bool is_outline = false, float border_size = 1)
    {
      using (new GUIBackgroundColorScope(background_color == null ? GUI.backgroundColor : background_color.Value))
      {
        GUI.Box(rect, image, style);
      }

      _Box_Border(rect, is_outline, border_size);
    }

    public static void Box(Rect rect, GUIContent content, GUIStyle style, Color? background_color = null,
      bool is_outline = false, float border_size = 1)
    {
      using (new GUIBackgroundColorScope(background_color == null ? GUI.backgroundColor : background_color.Value))
      {
        GUI.Box(rect, content, style);
      }

      _Box_Border(rect, is_outline, border_size);
    }

    private static void _Box_Border(Rect rect, bool is_outline = false, float border_size = 1)
    {
      float x = rect.x;
      float y = rect.y;
      float width = rect.width;
      float height = rect.height;
      using (new GUIColorScope(UnityEngine.Color.black))
      {
        if (is_outline)
        {
          //上边
          GUI.Box(new Rect(x - border_size, y - border_size, width + 2 * border_size, border_size), "");
          //下边
          GUI.Box(new Rect(x - border_size, y + height, width + 2 * border_size, border_size), "");
          //左边
          GUI.Box(new Rect(x - border_size, y - border_size, border_size, height + 2 * border_size), "");
          //右边
          GUI.Box(new Rect(x + width, y - border_size, border_size, height + 2 * border_size), "");
        }
        else
        {
          //上边
          GUI.Box(new Rect(x, y, width, border_size), "");
          //下边
          GUI.Box(new Rect(x, y + height - border_size, width, border_size), "");
          //左边
          GUI.Box(new Rect(x, y, border_size, height), "");
          //右边
          GUI.Box(new Rect(x + width - border_size, y, border_size, height), "");
        }
      }
    }
  }
}