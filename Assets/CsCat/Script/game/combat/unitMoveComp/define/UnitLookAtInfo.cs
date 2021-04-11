using UnityEngine;

namespace CsCat
{
  public class UnitLookAtInfo
  {
    public bool is_rotate_x_arrived;
    public bool is_rotate_y_arrived;
    public Unit look_at_unit;
    public Vector3? look_at_dir;
    public string mode = "idle";
    public bool is_locked;


    public bool HasLookAt()
    {
      return IsLookAtDir() || IsLookAtUnit();
    }

    public bool IsLookAtDir()
    {
      return this.look_at_dir != null;
    }

    public bool IsLookAtUnit()
    {
      return this.look_at_unit != null;
    }
  }
}