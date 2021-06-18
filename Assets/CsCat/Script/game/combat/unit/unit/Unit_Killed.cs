namespace CsCat
{
  public partial class Unit
  {
    public void OnKilled(Unit source_unit, SpellBase spell, bool is_play_dead_animation = false,
      bool is_waiting_rebirth = false, bool is_keep_dead_body = false)
    {
      this.Broadcast<Unit, Unit, SpellBase>(UnitEventNameConst.On_Unit_Kill_Target, source_unit, this, spell);
      if ("be_throwed".Equals(this.unitMoveComp.move_type) || this.unitMoveComp.is_get_caught)
      {
        this.unitMoveComp.move_type = null;
        this.unitMoveComp.is_get_caught = false;
        var unitBeThrowedInfo = new UnitBeThrowedInfo();
        unitBeThrowedInfo.end_pos = Client.instance.combat.pathManager.GetGroundPos(this.position);
        unitBeThrowedInfo.duration = 0.1f;
        unitBeThrowedInfo.height = 0f;
        this.BeThrowed(unitBeThrowedInfo);
      }

      if (!this.unitModelInfo_dict["main"].path.Equals(this.cfgUnitData.model_path))
        this.BuildModel(this.cfgUnitData.model_path);
      this.is_dead = true;
      this.UpdateMixedStates();
      if (is_keep_dead_body)
        this.is_keep_dead_body = true;
      if (is_play_dead_animation)
      {
        this.MoveStop();
        float dead_body_dealy = this.cfgUnitData.dead_body_dealy == 0 ? 0.5f : this.cfgUnitData.dead_body_dealy;
        string death_effect_id = this.cfgUnitData.death_effect_id;
        if (!death_effect_id.IsNullOrWhiteSpace())
        {
          var cfgEffectData = CfgEffect.Instance.get_by_id(death_effect_id);
          var ground_effect_pos = this.GetSocketPosition(cfgEffectData.socket_name_1);
          Client.instance.combat.effectManager.CreateGroundEffectEntity(death_effect_id, this, ground_effect_pos,
            this.GetRotation().eulerAngles, cfgEffectData.duration);
        }

        if (this.animation != null)
        {
          var die_animationState = this.animation[AnimationNameConst.die];
          if (die_animationState != null)
          {
            this.PlayAnimation(AnimationNameConst.die);
            dead_body_dealy = die_animationState.length + 1;
          }

          if (!is_waiting_rebirth)
          {
            this.AddTimer(args =>
              {
                this.__OnDieOver();
                return false;
              },
              dead_body_dealy);
          }
        }
        else
        {
          this.animatorComp.PlayAnimation(AnimationNameConst.die, true);
          this.AddTimer(args =>
          {
            this.__OnDieOver();
            return false;
          }, dead_body_dealy);
        }
      }
      else
        Client.instance.combat.unitManager.RemoveUnit(this.key);
    }

    private void __OnDieOver()
    {
      if (this.is_keep_dead_body)
        return;
      if (!this.cfgUnitData.death_effect_id.IsNullOrWhiteSpace())
      {
        this.SetIsMoveWithMoveAnimation(false);
        this.AddTimer(args =>
        {
          this.__OnDieBuryOver();
          return false;
        }, 3);
      }
      else
        this.__OnDieBuryOver();
    }

    public void __OnDieBuryOver()
    {
      Client.instance.combat.unitManager.RemoveUnit(this.key);
    }
  }
}