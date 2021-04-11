using System.Collections;
using UnityEngine;

namespace CsCat
{
  public partial class SpellBase
  {
    private float __animation_time_pct;
    private float __animation_start_time;
    public bool is_past_break_time;

    protected void PlaySpellAnimation(Vector3? face_to_position = null)
    {
      if (this.spellDefinition.animation_duration > 0)
      {
        this.__animation_time_pct = 0;
        this.__animation_start_time = CombatUtil.GetTime();
      }

      if (!this.spellDefinition.animation_name.IsNullOrWhiteSpace())
      {
        if (face_to_position == null && this.target_unit != null)
          face_to_position = this.target_unit.GetPosition();
        // 不转向
        if (this.spellDefinition.is_not_face_to_target)
          face_to_position = null;
        float speed = this.spellDefinition.type == "普攻" ? this.source_unit.GetCalcPropValue("攻击速度") : 1;
        this.source_unit.PlayAnimation(this.spellDefinition.animation_name, null, speed, face_to_position,
          this.spellDefinition.is_can_move_while_cast);
      }
    }

    protected void StopSpellAnimation()
    {
      if (!this.spellDefinition.animation_name.IsNullOrWhiteSpace())
        this.source_unit.StopAnimation(this.spellDefinition.animation_name);
    }

    //注意：只能在start时调用，不能在事件中调用
    protected void RegisterAnimationEvent(float? time_pct, string invoke_method_name, Hashtable arg_dict = null)
    {
      if (this.spellDefinition.animation_duration == 0 || time_pct == null || time_pct.Value <= 0)
      {
        this.InvokeMethod(invoke_method_name, false, arg_dict);
        return;
      }

      var new_event = new Hashtable();
      new_event["time_pct"] = time_pct.Value;
      new_event["event_name"] = invoke_method_name;
      new_event["arg_dict"] = arg_dict;

      for (int i = 0; i < this.animation_event_list.Count; i++)
      {
        var animation_event = this.animation_event_list[i];
        if (animation_event.Get<float>("time_pct") > time_pct)
        {
          this.animation_event_list.Insert(i, new_event);
          return;
        }
      }

      this.animation_event_list.Add(new_event);
    }

    private void ProcessAnimationEvent(float deltaTime)
    {
      if (this.__animation_time_pct == 0)
        return;
      this.__animation_time_pct = this.__animation_time_pct + deltaTime /
                                  (this.spellDefinition.animation_duration /
                                   (1 + this.source_unit.GetCalcPropValue("攻击速度")));
      while (true)
      {
        //没有animation_event了
        if (animation_event_list.IsNullOrEmpty())
          return;
        var animation_event = this.animation_event_list[0];
        // 还没触发
        if (animation_event.Get<float>("time_pct") > this.__animation_time_pct)
          return;
        // 时间到，可以进行触发
        this.animation_event_list.RemoveFirst();
        this.InvokeMethod(animation_event.Get<string>("invoke_method_name"), false,
          animation_event.Get<Hashtable>("arg_dict"));
      }
    }

    public void PassBreakTime()
    {
      this.is_past_break_time = true;
      this.source_unit.UpdateMixedStates();
    }
  }
}
