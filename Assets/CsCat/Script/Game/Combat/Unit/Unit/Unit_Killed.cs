namespace CsCat
{
	public partial class Unit
	{
		public void OnKilled(Unit sourceUnit, SpellBase spell, bool isPlayDeadAnimation = false,
		  bool isWaitingRebirth = false, bool isKeepDeadBody = false)
		{
			this.Broadcast<Unit, Unit, SpellBase>(null, UnitEventNameConst.On_Unit_Kill_Target, sourceUnit, this, spell);
			if ("be_throwed".Equals(this.unitMoveComp.moveType) || this.unitMoveComp.isGetCaught)
			{
				this.unitMoveComp.moveType = null;
				this.unitMoveComp.isGetCaught = false;
				var unitBeThrowedInfo = new UnitBeThrowedInfo();
				unitBeThrowedInfo.endPos = Client.instance.combat.pathManager.GetGroundPos(this.position);
				unitBeThrowedInfo.duration = 0.1f;
				unitBeThrowedInfo.height = 0f;
				this.BeThrowed(unitBeThrowedInfo);
			}

			if (!this.unitModelInfoDict["main"].path.Equals(this.cfgUnitData.model_path))
				this.BuildModel(this.cfgUnitData.model_path);
			this.isDead = true;
			this.UpdateMixedStates();
			if (isKeepDeadBody)
				this.isKeepDeadBody = true;
			if (isPlayDeadAnimation)
			{
				this.MoveStop();
				float deadBodyDealy = this.cfgUnitData.dead_body_dealy == 0 ? 0.5f : this.cfgUnitData.dead_body_dealy;
				string deathEffectId = this.cfgUnitData.death_effect_id;
				if (!deathEffectId.IsNullOrWhiteSpace())
				{
					var cfgEffectData = CfgEffect.Instance.get_by_id(deathEffectId);
					var groundEffectPos = this.GetSocketPosition(cfgEffectData.socket_name_1);
					Client.instance.combat.effectManager.CreateGroundEffectEntity(deathEffectId, this, groundEffectPos,
					  this.GetRotation().eulerAngles, cfgEffectData.duration);
				}

				if (this.animation != null)
				{
					var dieAnimationState = this.animation[AnimationNameConst.die];
					if (dieAnimationState != null)
					{
						this.PlayAnimation(AnimationNameConst.die);
						deadBodyDealy = dieAnimationState.length + 1;
					}

					if (!isWaitingRebirth)
					{
						this.AddTimer(args =>
						  {
							  this.__OnDieOver();
							  return false;
						  },
						  deadBodyDealy);
					}
				}
				else
				{
					this.animatorComp.PlayAnimation(AnimationNameConst.die, true);
					this.AddTimer(args =>
					{
						this.__OnDieOver();
						return false;
					}, deadBodyDealy);
				}
			}
			else
				Client.instance.combat.unitManager.RemoveUnit(this.key);
		}

		private void __OnDieOver()
		{
			if (this.isKeepDeadBody)
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