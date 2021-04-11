using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class SpellBase
  {
    private List<Unit> target_unit_list;

    public override void Start()
    {
      base.Start();
      if ("被动".Equals(this.spellDefinition.type))
        this.CounterIncrease(); // 被动默认不被消耗
      this.CounterIncrease();

      this.target_unit_list = Client.instance.combat.spellManager.RecommendSpellRule(this.source_unit, this.target_unit,
        this.spellDefinition, this.origin_position.Value);
      this.target_unit = this.target_unit_list.IsNullOrEmpty() ? null : this.target_unit_list[0];
      if (this.IsHasMethod("OnStart"))
        this.InvokeMethod("OnStart", false);
      this.RegisterTriggerSpell();
      this.Broadcast<Unit, Unit, SpellBase>(SpellEventNameConst.On_Spell_Start, this.source_unit, this.target_unit, this);
      Client.instance.combat.spellManager.UnRegisterListener("on_start", this.source_unit, this,
        "RegisterTriggerSpell");
      if (!this.spellDefinition.action_name.IsNullOrWhiteSpace())
      {
        //      if not self.source_unit.action_dict or
        //      not self.source_unit.action_dict[self.spellDefinition.action_name] then
        //      Error("action is not find", self.spell_id, self.source_unit.unit_id)
        //      end
        //      self.action = SpellAction.New(self.source_unit.action_dict[self.spellDefinition.action_name], self.source_unit, self)
        //      self.action:Play()
      }
      else
      {
        this.PlaySpellAnimation();
        if (this.IsHasMethod("OnCast"))
        {
          //起手前摇
          var cast_time_pct = this.GetAnimationTimePct(this.spellDefinition.cast_time, 0);
          this.RegisterAnimationEvent(cast_time_pct, "__OnCast");
        }

        //可打断后摇
        var break_time_pct = this.GetAnimationTimePct(this.spellDefinition.break_time, 1);
        this.RegisterAnimationEvent(break_time_pct
          , "PassBreakTime");
        if ("触发".Equals(this.spellDefinition.cast_type))
        {
          var _cast_time_pct = this.GetAnimationTimePct(this.spellDefinition.cast_time, 0);
          var _break_time_pct = this.GetAnimationTimePct(this.spellDefinition.break_time, 1);
          if (_break_time_pct < _cast_time_pct)
            LogCat.LogError("技能脱手时间比出手时间快");
          this.RegisterAnimationEvent(_break_time_pct, "OnSpellAnimationFinished");
        }
      }

      this.CounterDecrease();
    }

    private float GetAnimationTimePct(float time, float default_value)
    {
      if (this.spellDefinition.animation_duration != 0)
        return time / this.spellDefinition.animation_duration;
      return default_value;
    }

    protected void __OnCast()
    {
      if (this.IsHasMethod("OnCast"))
        this.InvokeMethod("OnCast", false);
      this.Broadcast<Unit, Unit, SpellBase>(SpellEventNameConst.On_Spell_Cast, this.source_unit, this.target_unit, this);
      Client.instance.combat.spellManager.UnRegisterListener("on_cast", this.source_unit, this, "RegisterTriggerSpell");
    }

    protected void RegisterTriggerSpell()
    {
      //注册表里填的技能触发事件，由简单的技能按顺序触发组成复杂的技能
      var new_spell_trigger_ids = this.spellDefinition.new_spell_trigger_ids;
      if (new_spell_trigger_ids.IsNullOrEmpty())
        return;
      foreach (var new_spell_trigger_id in new_spell_trigger_ids)
        this.__RegisterTriggerSpell(new_spell_trigger_id);
    }

    public void __RegisterTriggerSpell(string new_spell_trigger_id)
    {
      var spellTriggerDefinition = DefinitionManager.instance.spellTriggerDefinition.GetData(new_spell_trigger_id);
      var trigger_type = spellTriggerDefinition.trigger_type;
      trigger_type = SpellConst.Trigger_Type_Dict[trigger_type];
      var trigger_spell_id = spellTriggerDefinition.trigger_spell_id; // 触发的技能id
      var trigger_spell_delay_duration = spellTriggerDefinition.trigger_spell_delay_duration;
      Action<Unit, Unit, SpellBase> func = (source_unit, target_unit, spell) =>
      {
        //这里可以添加是否满足其它触发条件判断
        if (!this.CheckTriggerCondition(spellTriggerDefinition, source_unit, target_unit))
          return;
        var trigger_arg_dict = new Hashtable();
        trigger_arg_dict["source_spell"] = this;
        trigger_arg_dict["transmit_arg_dict"] = this.GetTransmitArgDict();
        trigger_arg_dict["new_spell_trigger_id"] = new_spell_trigger_id;
        Action trigger_func = () =>
        {
          //启动技能时需要把新技能需要的参数传进去，如果当前技能没有提供这样的方法，则说明当前技能不能启动目标技能
          Client.instance.combat.spellManager.CastSpell(this.source_unit, trigger_spell_id, target_unit,
            trigger_arg_dict);
        };
        if (trigger_spell_delay_duration > 0)
        {
          this.CounterIncrease();
          this.AddTimer((args) =>
          {
            trigger_func();
            this.CounterDecrease();
            return false;
          }, trigger_spell_delay_duration);
        }
        else
          trigger_func();
      };
      Client.instance.combat.spellManager.RegisterListener(trigger_type, this.source_unit, this, "RegisterTriggerSpell",
        new MethodInvoker(func));
    }


    protected bool CheckTriggerCondition(SpellTriggerDefinition spellTriggerDefinition, Unit source_unit,
      Unit target_unit)
    {
      return true;
    }

    // 需要解决的问题，比如一个技能同时攻击了几个单位，触发了几次on_hit，怎么在回调中知道这个hit是由哪次攻击造成的
    // 定义几种参数类型
    //
    //  SpellBase提供默认参数，具体技能根据自己实际情况重写
    //  1.攻击方向
    //  2.技能基础位置
    ////////////////////////////////////////////传递给下一个技能的方法///////////////////////////////////
    public Hashtable GetTransmitArgDict()
    {
      Hashtable result = new Hashtable();
      result["origin_position"] = this.GetOriginPosition();
      result["attack_dir"] = this.GetAttackDir();
      return result;
    }

    public Vector3 GetOriginPosition()
    {
      return this.origin_position.GetValueOrDefault(this.source_unit.GetPosition());
    }

    public Vector3 GetAttackDir()
    {
      return Vector3.zero;
    }

    public void SwitchAction()
    {
      //    self.action = SpellAction.New(self.source_unit.action_dict[action_name], self.source_unit, self)
      //    self.action:Play()
    }

    // 技能脱手，表示角色释放技能完成，可以做其他动作，但是技能本身可能没有完成，继续运行
    // 比如脱手后子弹任然要飞，打到人才真正结束
    // 使用CounterIncrease()和CounterDecrease()计数来控制真正结束
    public void OnSpellAnimationFinished()
    {
      if (this.is_spell_animation_finished)
        return;
      this.is_spell_animation_finished = true;
      Client.instance.combat.spellManager.OnSpellAnimationFinished(this);
      if (this.counter.count <= 0)
        this.RemoveSelf();
      if (this.spellDefinition.is_can_move_while_cast && this.source_unit != null && !this.source_unit.IsDead())
        this.source_unit.SetIsMoveWithMoveAnimation(true);
    }

    public void Break()
    {
      this.StopSpellAnimation();
      this.OnSpellAnimationFinished();
    }

    //子类添加 FilterUnit 函数可以自定义过滤掉不需要的目标
    protected bool FilterUnit(Unit unit, string spell_id, Unit target_unit, SpellDefinition spellDefinition)
    {
      return true;
    }

    protected void OnMissileReach(EffectEntity missileEffect)
    {
      this.Broadcast<Unit, EffectEntity, SpellBase>(SpellEventNameConst.On_Missile_Reach, this.source_unit, missileEffect, this);
      this.CounterDecrease();
    }
  }
}