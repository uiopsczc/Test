namespace CsCat
{
	public partial class Unit
	{
		//////////////////////////////////////////////////
		// Buff
		//////////////////////////////////////////////////
		public bool HasBuff(string buffId)
		{
			return this.buffManager.IsHasBuff(buffId);
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
		public bool HasState(string stateName)
		{
			return this.buffManager.IsHasState(stateName);
		}

		private void InitMixedStates()
		{
			this._isDead = false;

			this._isCanMove = true;
			this._isCanAttack = true;
			this.isCanCastSkill = true;
			this._isCanNormalAttack = true;
			this.isCanControl = true;
		}

		// 混合状态
		public void UpdateMixedStates()
		{
			//是否是正常状态
			bool isCommonState = !this.IsDead() &&
								   !this.IsStun() &&
								   !this.IsFreeze() &&
								   this.unitMoveComp.unitBeThrowedInfo == null &&
								   !this.unitMoveComp.isGetCaught;
			bool newIsCanMove = isCommonState &&
								   !this.HasState(StateConst.CanNotMove) &&
								   (this.currentAttack == null ||
									this.currentAttack.isPastBreakTime ||
									this.currentAttack.cfgSpellData.isCanMoveWhileCast);
			bool newIsCanAttack = isCommonState &&
									 (!this.HasState(StateConst.CanNotAttack)) &&
									 (this.currentAttack == null || this.currentAttack.isPastBreakTime);
			bool newIsSilent = this.IsSilent();
			bool newIsCanCastSkill = newIsCanAttack && !newIsSilent;
			bool newIsCanNormalAttack = newIsCanAttack;

			bool newIsConfused = this.IsConfused();
			bool newIsCanOperate = isCommonState && !newIsConfused;
			bool newIsCanControl = (newIsCanMove || newIsCanAttack) && newIsCanOperate;

			//检查混合状态变化
			if (this._isCanMove != newIsCanMove)
			{
				this._isCanMove = newIsCanMove;
				this.FireEvent(null, UnitEventNameConst.On_Unit_Is_Can_Move_Change, this, !this._isCanMove, this._isCanMove);
				if (!this._isCanMove)
					this.MoveStop();
			}

			if (this._isCanAttack != newIsCanAttack)
			{
				this._isCanAttack = newIsCanAttack;
				this.FireEvent(null, UnitEventNameConst.On_Unit_Is_Can_Attack_Change, this, !this._isCanAttack, this._isCanAttack);
			}

			if (this.isCanCastSkill != newIsCanCastSkill)
			{
				this.isCanCastSkill = newIsCanCastSkill;
				this.FireEvent(null, UnitEventNameConst.On_Unit_Is_Can_Cast_Skill_Change, this, !this.isCanCastSkill, this.isCanCastSkill);
				if (!this.isCanCastSkill &&
					(this.currentAttack != null && this.skillIdList.Contains(this.currentAttack.spellId)))
					Client.instance.combat.spellManager.BreakSpell(this.currentAttack.GetGuid());
			}

			if (this._isCanNormalAttack != newIsCanNormalAttack)
			{
				this._isCanNormalAttack = newIsCanNormalAttack;
				this.FireEvent(null, UnitEventNameConst.On_Unit_Is_Can_Normal_Attack_Change, this, !this._isCanNormalAttack, this._isCanNormalAttack);
				if (!this._isCanNormalAttack && (this.currentAttack != null &&
												   this._normalAttackIdList.Contains(this.currentAttack.spellId)))
					Client.instance.combat.spellManager.BreakSpell(this.currentAttack.GetGuid());
			}

			if (this.isCanControl != newIsCanControl)
			{
				this.isCanControl = newIsCanControl;
				this.FireEvent(null, UnitEventNameConst.On_Unit_Is_Can_Control_Change, this, !this.isCanControl, this.isCanControl);
			}
		}
	}
}