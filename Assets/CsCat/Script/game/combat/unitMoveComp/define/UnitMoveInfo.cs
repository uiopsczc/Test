using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class UnitMoveInfo
  {
    public float animation_speed = -1;
    public string animation_name = AnimationNameConst.walk;
    public float speed = 1;
    public float remain_duration;
    public Vector3 target_pos;
    public int target_index_in_path;
    public List<Vector3> path;
    public Unit look_at_unit;
    public float rotate_remain_duration;
    public Quaternion end_rotation;


    public bool IsHasAnimationName()
    {
      return !this.animation_name.IsNullOrWhiteSpace();
    }
  }
}