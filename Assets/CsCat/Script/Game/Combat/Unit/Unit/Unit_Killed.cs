namespace CsCat
{
	public partial class Unit
	{
		public void OnKilled(Unit sourceUnit, SpellBase spell, bool isPlayDeadAnimation = false,
		  bool isWaitingRebirth = false, bool isKeepDeadBody = false)
		{
			this.Broadcast<Unit, Unit, SpellBase>(null, UnitEventNameConst.On_Unit_Kill_Target, sourceUnit, this, spell);
			if ("beThrowed".Equals(this.unitMoveComp.moveType) || this.unitMoveComp.isGetCaught)
			{
				this.unitMoveComp.moveType = null;
				this.unitMoveComp.isGetCaught = false;
				var unitBeThrowedInfo = new UnitBeThrowedInfo();
				unitBeThrowedInfo.endPos = Client.instance.combat.pathManager.GetGroundPos(this._position);
				unitBeThrowedInfo.duration = 0.1f;
				unitBeThrowedInfo.height = 0f;
				this.BeThrowed(unitBeThrowedInfo);
			}

			if (!this._unitModelInfoDict["main"].path.Equals(this.cfgUnitData.modelPath))
				this.BuildModel(this.cfgUnitData.modelPath);
			this._isDead = true;
			this.UpdateMixedStates();
			if (isKeepDeadBody)
				this.isKeepDeadBody = true;
			if (isPlayDeadAnimation)
			{
				this.MoveStop();
				float deadBodyDelay = this.cfgUnitData.deadBodyDealy == 0 ? 0.5f : this.cfgUnitData.deadBodyDealy;
				string deathEffectId = this.cfgUnitData.deathEffectId;
				if (!deathEffectId.IsNullOrWhiteSpace())
				{
					var cfgEffectData = CfgEffect.Instance.GetById(deathEffectId);
					var groundEffectPos = this.GetSocketPosition(cfgEffectData.socketName1);
					Client.instance.combat.effectManager.CreateGroundEffectEntity(deathEffectId, this, groundEffectPos,
					  this.GetRotation().eulerAngles, cfgEffectData.duration);
				}

				if (this.animation != null)
				{
					var dieAnimationState = this.animation[AnimationNameConst.die];
					if (dieAnimationState != null)
					{
						this.PlayAnimation(AnimationNameConst.die);
						deadBodyDelay = dieAnimationState.length + 1;
					}

					if (!isWaitingRebirth)
					{
						this.AddTimer(args =>
						  {
							  this.__OnDieOver();
							  return false;
						  },
						  deadBodyDelay);
					}
				}
				else
				{
					this.animatorComp.PlayAnimation(AnimationNameConst.die, true);
					this.AddTimer(args =>
					{
						this.__OnDieOver();
						return false;
					}, deadBodyDelay);
				}
			}
			else
				Client.instance.combat.unitManager.RemoveUnit(this.key);
		}

		private void __OnDieOver()
		{
			if (this.isKeepDeadBody)
				return;
			if (!this.cfgUnitData.deathEffectId.IsNullOrWhiteSpace())
			{
				this.SetIsMoveWithMoveAnimation(false);
				this.AddTimer(args =>
				{
					this._OnDieBuryOver();
					return false;
				}, 3);
			}
			else
				this._OnDieBuryOver();
		}

		public void _OnDieBuryOver()
		{
			Client.instance.combat.unitManager.RemoveUnit(this.key);
		}
	}
}