namespace CsCat
{
	public partial class Unit
	{
		//////////////////////////////////////////////////
		// Buff
		//////////////////////////////////////////////////
		public bool HasBuff(string buffId)
		{
			return this.buffManager.HasBuff(buffId);
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
			return this.buffManager.HasState(stateName);
		}

		private void InitMixedStates()
		{
			this.isDead = false;

			this.isCanMove = true;
			this.isCanAttack = true;
			this.isCanCastSkill = true;
			this.isCanNormalAttack = true;
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
									this.currentAttack.cfgSpellData.is_can_move_while_cast);
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
			if (this.isCanMove != newIsCanMove)
			{
				this.isCanMove = newIsCanMove;
				this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Can_Move_Change, this, !this.isCanMove, this.isCanMove);
				if (!this.isCanMove)
					this.MoveStop();
			}

			if (this.isCanAttack != newIsCanAttack)
			{
				this.isCanAttack = newIsCanAttack;
				this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Can_Attack_Change, this, !this.isCanAttack, this.isCanAttack);
			}

			if (this.isCanCastSkill != newIsCanCastSkill)
			{
				this.isCanCastSkill = newIsCanCastSkill;
				this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Can_Cast_Skill_Change, this, !this.isCanCastSkill, this.isCanCastSkill);
				if (!this.isCanCastSkill &&
					(this.currentAttack != null && this.skillIdList.Contains(this.currentAttack.spellId)))
					Client.instance.combat.spellManager.BreakSpell(this.currentAttack.GetGuid());
			}

			if (this.isCanNormalAttack != newIsCanNormalAttack)
			{
				this.isCanNormalAttack = newIsCanNormalAttack;
				this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Can_Normal_Attack_Change, this, !this.isCanNormalAttack, this.isCanNormalAttack);
				if (!this.isCanNormalAttack && (this.currentAttack != null &&
												   this.normalAttackIdList.Contains(this.currentAttack.spellId)))
					Client.instance.combat.spellManager.BreakSpell(this.currentAttack.GetGuid());
			}

			if (this.isCanControl != newIsCanControl)
			{
				this.isCanControl = newIsCanControl;
				this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Can_Control_Change, this, !this.isCanControl, this.isCanControl);
			}
		}
	}
}