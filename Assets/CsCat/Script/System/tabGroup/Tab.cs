using System;

namespace CsCat
{
  public class Tab
  {
    public Action on_select_callback;
    public Action on_unselect_callback;
    private bool _is_selected;

    public bool is_selected => _is_selected;

    public Tab(Action on_select_callback, Action on_unselect_callback)
    {
      this.on_select_callback = on_select_callback;
      this.on_unselect_callback = on_unselect_callback;
    }

    public void Select()
    {
      _is_selected = true;
      on_select_callback?.Invoke();
    }
    public void UnSelect()
    {
      _is_selected = false;
      on_unselect_callback?.Invoke();
    }

  }
}