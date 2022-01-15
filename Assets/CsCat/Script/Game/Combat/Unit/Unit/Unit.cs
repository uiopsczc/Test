using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Unit : TickObject
	{
		private bool isDead;
		private bool isCanAttack;
		private bool isCanNormalAttack;
		public bool isCanCastSkill;
		private bool isCanMove;
		public bool isCanControl;

		private int level;
		private Vector3 position;
		private Quaternion rotation;
		private int hp;
		private float radius;
		private float originalRadius;
		private float scale;
		private Vector3 orginalTransformScale;
		private bool isNotShowHeadBlood;
		public BuffManager buffManager;
		private bool isKeepDeadBody; //是否需要保持尸体
		private List<Action> loadOkListenList = new List<Action>();
		public bool isMoveOccupy;


		public CfgUnitData cfgUnitData;
		public string unitId;
		public string playerName;
		public Vector2 showNameOffset;
		public string name;
		public string type;
		public Unit ownerUnit;
		public bool isInSight = true; //是否在视野内，用于优化，由unitManager设置
		public UnitLockTargetInfo unitLockTargetInfo;


		protected override void _Destroy()
		{
			base._Destroy();
			this.Broadcast<Unit>(null, UnitEventNameConst.On_Unit_Destroy, this);
			animatorComp?.Destroy();
			propertyComp?.Destroy();
			this.unitModelInfoDict.Clear();
			this.animation = null;
			this.actionManager = null;
			this.socketTransformDict.Clear();
			this.unitMaterialInfoList.Clear();
		}

		public void UpdateUnit(Hashtable argDict)
		{
			foreach (string key in argDict.Keys)
			{
				if (key.Equals("hp"))
					this.SetHp(argDict.Get<int>(hp));
				else if (key.Equals("faction"))
					this.SetFaction(argDict.Get<string>("faction"));
				else if (key.Equals("level"))
					this.SetLevel(argDict.Get<int>("level"));
				else if (key.Equals("position"))
					this.SetPosition(argDict.Get<Vector3>("position"));
				else if (key.Equals("scale"))
					this.SetPosition(argDict.Get<Vector3>("scale"));
				else
					this.SetFieldValue(key, argDict[key]);
			}
		}


		public void SetPosition(Vector3 pos)
		{
			if (graphicComponent.transform)
				graphicComponent.transform.position = this.cfgUnitData.offset_y != 0
				  ? (pos + new Vector3(0, this.cfgUnitData.offset_y, 0))
				  : pos;
			this.position = pos;
		}

		public Vector3 GetPosition()
		{
			return this.position;
		}

		public void SetRotation(Quaternion rotation)
		{
			if (graphicComponent.transform)
				graphicComponent.transform.rotation = rotation;
			this.rotation = rotation;
		}

		public Quaternion GetRotation()
		{
			return this.rotation;
		}

		public UnitPosition ToUnitPosition()
		{
			return new UnitPosition(this);
		}

		public void SetScale(float scale)
		{
			if (graphicComponent.transform != null)
				graphicComponent.transform.localScale = this.orginalTransformScale * scale;
			this.scale = scale;
			this.radius = this.originalRadius * this.scale;
		}

		public float GetScale()
		{
			return this.scale;
		}

		public void SetLevel(int level)
		{
			this.level = level;
			this.propertyComp.__CalculateProp();
		}

		public int GetLevel()
		{
			return this.level;
		}

		public float GetRadius()
		{
			return this.radius;
		}

		public float Distance(Vector3 target)
		{
			return (target - this.GetPosition()).magnitude - this.radius;
		}

		public float Distance(Unit target)
		{
			return (target.GetPosition() - this.GetPosition()).magnitude - this.radius - target.radius;
		}

		public float Distance(Transform target)
		{
			return (target.position - this.GetPosition()).magnitude - this.radius;
		}

		public float Distance(IPosition target_iposition)
		{
			if (target_iposition is UnitPosition)
				return Distance(((UnitPosition)target_iposition).unit);
			else
				return Distance(target_iposition.GetPosition());
		}

		public int GetMaxHp()
		{
			return (int)this.GetCalcPropValue("生命上限");
		}

		public void SetHp(int hp, bool is_not_broadcast = false)
		{
			var old_value = this.hp;
			this.hp = (int)Math.Min(hp, this.GetMaxHp());
			if (!is_not_broadcast)
				this.OnHpChange(null, old_value, this.hp);
		}

		protected void OnHpChange(Unit source_unit, int old_value, int new_value)
		{
			if (old_value != new_value)
				this.Broadcast<Unit, Unit, int, int>(null, UnitEventNameConst.On_Unit_Hp_Change, source_unit, this, old_value, new_value);
		}

		protected void OnMaxHpChange(int old_value, int new_value)
		{
			if (old_value == new_value)
				this.Broadcast<Unit, int, int>(null, UnitEventNameConst.On_MaxHp_Change, this, old_value, new_value);
		}

		public int GetHp()
		{
			return this.hp;
		}

		public float GetSpeed()
		{
			return this.GetCalcPropValue("移动速度");
		}

		private void OnSpeedChange(float old_value, float new_value)
		{
			this.unitMoveComp.OnSpeedChange(old_value, new_value);
		}

		//能否移动
		public bool IsCanMove()
		{
			return this.isCanMove;
		}

		//能否攻击（包括普攻和技能）
		public bool IsCanAttack()
		{
			return this.isCanAttack;
		}

		//能否普攻
		public bool IsCanNormalAttack()
		{
			return this.isCanNormalAttack;
		}


		//能否释放技能
		public bool IsCanCastSkill()
		{
			return this.isCanCastSkill;
		}


		//能否被控制
		public bool IsCanControl()
		{
			return this.isCanControl;
		}


		//是否混乱状态
		public bool IsConfused()
		{
			return this.HasState(StateConst.Confused);
		}

		//是否无敌
		public bool IsInvincible()
		{
			return this.HasState(StateConst.Invincible);
		}


		//是否昏眩状态
		public bool IsStun()
		{
			return this.HasState(StateConst.Stun);
		}

		//是否冰冻状态
		public bool IsFreeze()
		{
			return this.HasState(StateConst.Freeze);
		}

		//是否沉默状态
		public bool IsSilent()
		{
			return this.HasState(StateConst.Silent);
		}

		//是否免控状态
		public bool IsImmuneControl()
		{
			return this.HasState(StateConst.ImmuneControl);
		}

		//是否不受伤害状态
		public bool IsCanNotBeTakeDamage()
		{
			return this.HasState(StateConst.CanNotBeTakeDamage);
		}


		//是否不能被治疗状态
		public bool IsCanNotBeHeal()
		{
			return this.HasState(StateConst.CanNotBeHeal);
		}

		//是否隐身状态
		public bool IsHide()
		{
			return this.HasState(StateConst.Hide);
		}

		//是否反隐状态
		public bool IsExpose()
		{
			return this.HasState(StateConst.Expose);
		}

		//是否死亡
		public bool IsDead()
		{
			return this.isDead;
		}
	}
}