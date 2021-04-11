using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CsCat
{
  public class TransformTweenBehaviour : PlayableBehaviour
  {
    public TimelineClip clip;

    [SerializeField] public bool is_use_position_target = true;

    [SerializeField] public bool is_use_rotation_target;

    [SerializeField] public bool is_use_scale_target;


    [SerializeField] public Vector3 position_multiply = Vector3.one;

    [SerializeField] public Vector3 position_target;

    [SerializeField] public Vector3 rotation_multiply = Vector3.one;

    [SerializeField] public Vector3 rotation_target;

    [SerializeField] public Vector3 scale_multiply = Vector3.one;

    [SerializeField] public Vector3 scale_target;
  }
}