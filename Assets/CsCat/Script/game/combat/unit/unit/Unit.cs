using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class Unit : TickObject
  {
    private bool is_dead;
    private bool is_can_attack;
    private bool is_can_normal_attack;
    public bool is_can_cast_skill;
    private bool is_can_move;
    public bool is_can_control;

    private int level;
    private Vector3 position;
    private Quaternion rotation;
    private int hp;
    private float radius;
    private float original_radius;
    private float scale;
    private Vector3 orginal_transform_sacle;
    private bool is_not_show_headBlood;
    public BuffManager buffManager;
    private bool is_keep_dead_body; //是否需要保持尸体
    private List<Action> load_ok_listen_list = new List<Action>();
    public bool is_move_occupy;


    public UnitDefinition unitDefinition;
    public string unit_id;
    public string player_name;
    public Vector2 show_name_offset;
    public string name;
    public string type;
    public Unit owner_unit;
    public bool is_in_sight = true; //是否在视野内，用于优化，由unitManager设置
    public UnitLockTargetInfo unitLockTargetInfo;


    protected override void __Destroy()
    {
      base.__Destroy();
      this.Broadcast<Unit>(UnitEventNameConst.On_Unit_Destroy, this);
      if (this.animatorComp != null)
        animatorComp.Destroy();
      if (this.propertyComp != null)
        this.propertyComp.Destroy();
      this.unitModelInfo_dict.Clear();
      this.animation = null;
      this.actionManager = null;
      this.socket_transform_dict.Clear();
      this.unitMaterialInfo_list.Clear();
    }

    public void UpdateUnit(Hashtable arg_dict)
    {
      foreach (string key in arg_dict.Keys)
      {
        if (key.Equals("hp"))
          this.SetHp(arg_dict.Get<int>(hp));
        else if (key.Equals("faction"))
          this.SetFaction(arg_dict.Get<string>("faction"));
        else if (key.Equals("level"))
          this.SetLevel(arg_dict.Get<int>("level"));
        else if (key.Equals("position"))
          this.SetPosition(arg_dict.Get<Vector3>("position"));
        else if (key.Equals("scale"))
          this.SetPosition(arg_dict.Get<Vector3>("scale"));
        else
          this.SetFieldValue(key, arg_dict[key]);
      }
    }


    public void SetPosition(Vector3 pos)
    {
      if (graphicComponent.transform)
        graphicComponent.transform.position = this.unitDefinition.offset_y != 0
          ? (pos + new Vector3(0, this.unitDefinition.offset_y, 0))
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
        graphicComponent.transform.localScale = this.orginal_transform_sacle * scale;
      this.scale = scale;
      this.radius = this.original_radius * this.scale;
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
        this.Broadcast<Unit, Unit, int, int>(UnitEventNameConst.On_Unit_Hp_Change, source_unit, this, old_value, new_value);
    }

    protected void OnMaxHpChange(int old_value, int new_value)
    {
      if (old_value == new_value)
        this.Broadcast<Unit, int, int>(UnitEventNameConst.On_MaxHp_Change, this, old_value, new_value);
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
      return this.is_can_move;
    }

    //能否攻击（包括普攻和技能）
    public bool IsCanAttack()
    {
      return this.is_can_attack;
    }

    //能否普攻
    public bool IsCanNormalAttack()
    {
      return this.is_can_normal_attack;
    }


    //能否释放技能
    public bool IsCanCastSkill()
    {
      return this.is_can_cast_skill;
    }


    //能否被控制
    public bool IsCanControl()
    {
      return this.is_can_control;
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
      return this.is_dead;
    }
  }
}