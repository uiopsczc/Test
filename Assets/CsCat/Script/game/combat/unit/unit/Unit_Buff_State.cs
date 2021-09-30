namespace CsCat
{
  public partial class Unit
  {
    //////////////////////////////////////////////////
    // Buff
    //////////////////////////////////////////////////
    public bool HasBuff(string buff_id)
    {
      return this.buffManager.HasBuff(buff_id);
    }

    public int GetBuffCount()
    {
      return this.buffManager.GetBuffCount();
    }

    public int GetDebuffCount()
    {
      return this.buffManager.GetDebuffCount();
    }

    //////////////////////////////////////////////////
    // State
    //////////////////////////////////////////////////
    public bool HasState(string state_name)
    {
      return this.buffManager.HasState(state_name);
    }

    private void InitMixedStates()
    {
      this.is_dead = false;

      this.is_can_move = true;
      this.is_can_attack = true;
      this.is_can_cast_skill = true;
      this.is_can_normal_attack = true;
      this.is_can_control = true;
    }

    // 混合状态
    public void UpdateMixedStates()
    {
      //是否是正常状态
      bool is_common_state = !this.IsDead() &&
                             !this.IsStun() &&
                             !this.IsFreeze() &&
                             this.unitMoveComp.unitBeThrowedInfo == null &&
                             !this.unitMoveComp.is_get_caught;
      bool new_is_can_move = is_common_state &&
                             !this.HasState(StateConst.CanNotMove) &&
                             (this.current_attack == null ||
                              this.current_attack.is_past_break_time ||
                              this.current_attack.cfgSpellData.is_can_move_while_cast);
      bool new_is_can_attack = is_common_state &&
                               (!this.HasState(StateConst.CanNotAttack)) &&
                               (this.current_attack == null || this.current_attack.is_past_break_time);
      bool new_is_silent = this.IsSilent();
      bool new_is_can_cast_skill = new_is_can_attack && !new_is_silent;
      bool new_is_can_normal_attack = new_is_can_attack;

      bool new_is_confused = this.IsConfused();
      bool new_is_can_operate = is_common_state && !new_is_confused;
      bool new_is_can_control = (new_is_can_move || new_is_can_attack) && new_is_can_operate;

      //检查混合状态变化
      if (this.is_can_move != new_is_can_move)
      {
        this.is_can_move = new_is_can_move;
        this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Can_Move_Change, this, !this.is_can_move, this.is_can_move);
        if (!this.is_can_move)
          this.MoveStop();
      }

      if (this.is_can_attack != new_is_can_attack)
      {
        this.is_can_attack = new_is_can_attack;
        this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Can_Attack_Change, this, !this.is_can_attack, this.is_can_attack);
      }

      if (this.is_can_cast_skill != new_is_can_cast_skill)
      {
        this.is_can_cast_skill = new_is_can_cast_skill;
        this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Can_Cast_Skill_Change, this, !this.is_can_cast_skill, this.is_can_cast_skill);
        if (!this.is_can_cast_skill &&
            (this.current_attack != null && this.skill_id_list.Contains(this.current_attack.spell_id)))
          Client.instance.combat.spellManager.BreakSpell(this.current_attack.GetGuid());
      }

      if (this.is_can_normal_attack != new_is_can_normal_attack)
      {
        this.is_can_normal_attack = new_is_can_normal_attack;
        this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Can_Normal_Attack_Change, this, !this.is_can_normal_attack, this.is_can_normal_attack);
        if (!this.is_can_normal_attack && (this.current_attack != null &&
                                           this.normal_attack_id_list.Contains(this.current_attack.spell_id)))
          Client.instance.combat.spellManager.BreakSpell(this.current_attack.GetGuid());
      }

      if (this.is_can_control != new_is_can_control)
      {
        this.is_can_control = new_is_can_control;
        this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Can_Control_Change, this, !this.is_can_control, this.is_can_control);
      }
    }
  }
}