using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CsCat
{
  public class TransformTweenMixerBehaviour : PlayableBehaviour
  {
    public List<TimelineClip> clip_list;


    public PlayableDirector director;
    private bool is_first_frame_happened;


    private Transform is_track_binding;

    public Vector3 position_default;

    private Vector3 position_end;

    public Vector3 position_start;
    public Vector3 rotation_default;
    private Vector3 rotation_end;
    public Vector3 rotation_start;
    public Vector3 scale_default;
    private Vector3 scale_end;
    public Vector3 scale_start;


    private void HandleStart()
    {
      position_start = position_default;
      rotation_start = rotation_default;
      scale_start = scale_default;
      foreach (var e in clip_list)
      {
        var clip = e.asset as TransformTweenClip;
        if (director.time >= clip.clip.start + clip.clip.duration)
        {
          if (clip.template.is_use_position_target)
            position_start = clip.template.position_target;
          if (clip.template.is_use_rotation_target)
            rotation_start = clip.template.rotation_target;
          if (clip.template.is_use_scale_target)
            scale_start = clip.template.scale_target;


          position_start = position_start.Multiply(clip.template.position_multiply);
          rotation_start = rotation_start.Multiply(clip.template.rotation_multiply);
          scale_start = scale_start.Multiply(clip.template.scale_multiply);
        }
        else
        {
          break;
        }
      }
    }


    public override void ProcessFrame(Playable playable, FrameData info, object arg)
    {
      is_track_binding = arg as Transform;

      if (is_track_binding == null)
        return;


      HandleStart();


      var input_count = playable.GetInputCount();
      var weight_blend_total = 0f;


      var position_blend = Vector3.zero;
      var rotation_blend = Vector3.zero;
      var scale_blend = Vector3.zero;

      for (var i = 0; i < input_count; i++)
      {
        var input_weight = playable.GetInputWeight(i);
        var inputPlayable =
          (ScriptPlayable<TransformTweenBehaviour>)playable.GetInput(i);
        var input = inputPlayable.GetBehaviour();
        weight_blend_total += input_weight;

        if (Mathf.Approximately(input_weight, 0f))
          continue;

        if (input.clip.IsInRange(director.time))
        {
          var percent = input.clip.GetPercent(director.time);


          if (input.is_use_position_target)
            position_blend += Vector3.Lerp(position_start, input.position_target, percent) * input_weight;
          else
            position_blend += position_start * input_weight;

          if (input.is_use_rotation_target)
            rotation_blend += Vector3.Lerp(rotation_start, input.rotation_target, percent) * input_weight;
          else
            rotation_blend += rotation_start * input_weight;


          if (input.is_use_scale_target)
            scale_blend += Vector3.Lerp(scale_start, input.scale_target, percent) * input_weight;
          else
            scale_blend += scale_start * input_weight;

          position_blend = position_blend.Multiply(input.position_multiply) * input_weight;
          rotation_blend = rotation_blend.Multiply(input.rotation_multiply) * input_weight;
          scale_blend = scale_blend.Multiply(input.scale_multiply) * input_weight;
        }
      }

      var weight_remain_default = Mathf.Clamp(1 - weight_blend_total, 0, 1);


      is_track_binding.transform.localPosition = weight_remain_default * position_start + position_blend;
      is_track_binding.transform.localEulerAngles = weight_remain_default * rotation_start + rotation_blend;
      is_track_binding.transform.localScale = weight_remain_default * scale_start + scale_blend;
    }
  }
}