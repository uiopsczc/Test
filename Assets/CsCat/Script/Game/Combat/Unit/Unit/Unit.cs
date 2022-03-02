using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Unit : TickObject
	{
		private bool _isDead;
		private bool _isCanAttack;
		private bool _isCanNormalAttack;
		private bool _isCanMove;
		private int _level;
		private Vector3 _position;
		private Quaternion _rotation;
		private int _hp;
		private float _radius;
		private float _originalRadius;
		private float _scale;
		private Vector3 _orginalTransformScale;
		private bool _isNotShowHeadBlood;
		private bool isKeepDeadBody; //是否需要保持尸体
		private List<Action> _loadOkListenList = new List<Action>();
		


		public CfgUnitData cfgUnitData;
		public string unitId;
		public string playerName;
		public Vector2 showNameOffset;
		public string name;
		public string type;
		public Unit ownerUnit;
		public bool isInSight = true; //是否在视野内，用于优化，由unitManager设置
		public UnitLockTargetInfo unitLockTargetInfo;
		public bool isCanControl;
		public bool isCanCastSkill;
		public BuffManager buffManager;
		public bool isMoveOccupy;


		protected override void _Destroy()
		{
			base._Destroy();
			this.Broadcast<Unit>(null, UnitEventNameConst.On_Unit_Destroy, this);
			animatorComp?.Destroy();
			propertyComp?.Destroy();
			this._unitModelInfoDict.Clear();
			this.animation = null;
			this.actionManager = null;
			this._socketTransformDict.Clear();
			this._unitMaterialInfoList.Clear();
		}

		public void UpdateUnit(Hashtable argDict)
		{
			foreach (DictionaryEntry keyValue in argDict)
			{
				var key = (string)keyValue.Key;
				if (key.Equals("hp"))
					this.SetHp(argDict.Get<int>(_hp));
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
				graphicComponent.transform.position = this.cfgUnitData.offsetYy != 0
				  ? (pos + new Vector3(0, this.cfgUnitData.offsetYy, 0))
				  : pos;
			this._position = pos;
		}

		public Vector3 GetPosition()
		{
			return this._position;
		}

		public void SetRotation(Quaternion rotation)
		{
			if (graphicComponent.transform)
				graphicComponent.transform.rotation = rotation;
			this._rotation = rotation;
		}

		public Quaternion GetRotation()
		{
			return this._rotation;
		}

		public UnitPosition ToUnitPosition()
		{
			return new UnitPosition(this);
		}

		public void SetScale(float scale)
		{
			if (graphicComponent.transform != null)
				graphicComponent.transform.localScale = this._orginalTransformScale * scale;
			this._scale = scale;
			this._radius = this._originalRadius * this._scale;
		}

		public float GetScale()
		{
			return this._scale;
		}

		public void SetLevel(int level)
		{
			this._level = level;
			this.propertyComp._CalculateProp();
		}

		public int GetLevel()
		{
			return this._level;
		}

		public float GetRadius()
		{
			return this._radius;
		}

		public float Distance(Vector3 target)
		{
			return (target - this.GetPosition()).magnitude - this._radius;
		}

		public float Distance(Unit target)
		{
			return (target.GetPosition() - this.GetPosition()).magnitude - this._radius - target._radius;
		}

		public float Distance(Transform target)
		{
			return (target.position - this.GetPosition()).magnitude - this._radius;
		}

		public float Distance(IPosition targetIposition)
		{
			if (targetIposition is UnitPosition unitPosition)
				return Distance(unitPosition.unit);
			return Distance(targetIposition.GetPosition());
		}

		public int GetMaxHp()
		{
			return (int)this.GetCalcPropValue("生命上限");
		}

		public void SetHp(int hp, bool isNotBroadcast = false)
		{
			var oldValue = this._hp;
			this._hp = (int)Math.Min(hp, this.GetMaxHp());
			if (!isNotBroadcast)
				this.OnHpChange(null, oldValue, this._hp);
		}

		protected void OnHpChange(Unit sourceUnit, int oldValue, int newValue)
		{
			if (oldValue != newValue)
				this.Broadcast<Unit, Unit, int, int>(null, UnitEventNameConst.On_Unit_Hp_Change, sourceUnit, this, oldValue, newValue);
		}

		protected void OnMaxHpChange(int old_value, int new_value)
		{
			if (old_value == new_value)
				this.Broadcast<Unit, int, int>(null, UnitEventNameConst.On_MaxHp_Change, this, old_value, new_value);
		}

		public int GetHp()
		{
			return this._hp;
		}

		public float GetSpeed()
		{
			return this.GetCalcPropValue("移动速度");
		}

		private void OnSpeedChange(float oldValue, float newValue)
		{
			this.unitMoveComp.OnSpeedChange(oldValue, newValue);
		}

		//能否移动
		public bool IsCanMove()
		{
			return this._isCanMove;
		}

		//能否攻击（包括普攻和技能）
		public bool IsCanAttack()
		{
			return this._isCanAttack;
		}

		//能否普攻
		public bool IsCanNormalAttack()
		{
			return this._isCanNormalAttack;
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
			return this._isDead;
		}
	}
}