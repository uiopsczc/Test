using UnityEngine;

namespace CsCat
{
  public partial class UnitMoveComp
  {
    //模式为lock时，可以占据LookAt， 其他地方调用LookAt将不起作用，除非mode为force强行LookAt
    //在不需占据LookAt时，需传入unlock解锁
    // 暂时没用
    private bool __LookAt(string mode)
    {
      this.unitLookAtInfo.is_rotate_x_arrived = false;
      this.unitLookAtInfo.is_rotate_y_arrived = false;
      if (mode.Equals("stop_look_at"))
      {
        this.unitLookAtInfo.look_at_unit = null;
        this.unitLookAtInfo.look_at_dir = null;
        return false;
      }

      if (mode.Equals("unlock"))
      {
        this.unitLookAtInfo.is_locked = false;
        return false;
      }

      if (!mode.Equals("force") && this.unitLookAtInfo.is_locked)
        return false;
      this.unitLookAtInfo.mode = mode.IsNullOrWhiteSpace() ? "idle" : mode;
      if (mode.Equals("lock"))
        this.unitLookAtInfo.is_locked = true;
      return true;
    }


    public void LookAt(Unit unit, string mode)
    {
      if (__LookAt(mode) == false)
        return;
      this.unitLookAtInfo.look_at_unit = unit;
      this.unitLookAtInfo.look_at_dir = null;
    }

    public void LookAt(Vector3 dir, string mode)
    {
      if (__LookAt(mode) == false)
        return;
      this.unitLookAtInfo.look_at_unit = null;
      this.unitLookAtInfo.look_at_dir = dir;
    }
  }
}