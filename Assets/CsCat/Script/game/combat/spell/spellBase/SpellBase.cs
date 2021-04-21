using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class SpellBase : TickObject
  {
    public Unit source_unit;
    public string spell_id;
    public Unit target_unit;
    public SpellDefinition spellDefinition;
    public Hashtable instance_arg_dict;

    public Vector3? origin_position;
    public Hashtable transmit_arg_dict;
    public Vector3 attack_dir;
    public string new_spell_trigger_id;
    public Hashtable arg_dict;
    private bool is_can_move_while_cast;
    public bool is_spell_animation_finished;
    public List<Hashtable> animation_event_list = new List<Hashtable>();

    public void Init(Unit source_unit, string spell_id,
      Unit target_unit, SpellDefinition spellDefinition, Hashtable instance_arg_dict)
    {
      base.Init();
      this.source_unit = source_unit;
      this.spell_id = spell_id;
      this.target_unit = target_unit;
      this.spellDefinition = spellDefinition;
      this.instance_arg_dict = instance_arg_dict;

      this.origin_position =
        this.instance_arg_dict.GetOrGetDefault<Vector3>("origin_position", () => this.source_unit.GetPosition());
      this.transmit_arg_dict = this.instance_arg_dict.GetOrGetDefault("transmit_arg_dict", () => new Hashtable());
      this.attack_dir = this.transmit_arg_dict.Get<Vector3>(attack_dir);
      this.new_spell_trigger_id = this.transmit_arg_dict.Get<string>("new_spell_trigger_id"); // 通过哪个trigger_id启动的技能

      this.arg_dict = DoerAttrParserUtil.ConvertTableWithTypeString(this.spellDefinition.arg_dict);
      this.is_can_move_while_cast = this.spellDefinition.is_can_move_while_cast;
      this.is_spell_animation_finished = "触发".Equals(this.spellDefinition.cast_type);

      if (this.is_can_move_while_cast && this.source_unit != null && !this.source_unit.IsDead())
        this.source_unit.SetIsMoveWithMoveAnimation(false);
      this.InitCounter();
    }

    protected override void __Update(float deltaTime, float unscaledDeltaTime)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      if (!this.is_spell_animation_finished)
      {
        //脱手了就不需要执行动画了
        //      if self.action then
        //      self.action:Update(deltaTime)
        //      else
        //      self: ProcessAnimationEvent(deltaTime)
        //      end
        this.ProcessAnimationEvent(deltaTime);
      }
      else if ("触发".Equals(this.spellDefinition.cast_type))
        this.ProcessAnimationEvent(deltaTime);
    }

    public void RemoveSelf()
    {
      Client.instance.combat.spellManager.RemoveSpell(this.GetGuid());
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      if (!this.is_spell_animation_finished)
        Client.instance.combat.spellManager.OnSpellAnimationFinished(this);
      Client.instance.combat.spellManager.RemoveListenersByObj(this);

    }

    public void AddCombatNumber(int number, string target_unit_guid, string max_type, Hashtable arg_dict)
    {

    }

    public void AddCombatImage(int immune_type, string target_unit_guid)
    {

    }

    public void AddCombatText(string str_info, string target_unit_guid)
    {

    }
  }
}