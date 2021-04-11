using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public partial class HFSM
  {
    public override void SetIsPaused(bool is_paused, bool is_loop_children = false)
    {
      if (_is_paused == is_paused)
        return;
      this._is_paused = is_paused;
      if (is_loop_children)
      {
        current_sub_direct_hfsm?.SetIsPaused(is_paused, true);
        current_sub_direct_state?.SetIsPaused(is_paused, true);
      }
      __SetIsPaused(is_paused);
    }
  }
}