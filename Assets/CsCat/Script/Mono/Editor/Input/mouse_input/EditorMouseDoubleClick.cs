using UnityEngine;

namespace CsCat
{
  public class EditorMouseDoubleClick
  {
    public bool is_double_click
    {
      get { return _is_double_click; }
    }

    float last_click_time = 0f;
    bool _is_double_click = false;

    public void Update()
    {
      Event e = Event.current;
      _is_double_click = false;
      if (e.isMouse && e.type == EventType.MouseDown)
      {
        _is_double_click = (Time.realtimeSinceStartup - last_click_time) <= 0.2f;
        last_click_time = Time.realtimeSinceStartup;
      }
    }
  }
}

