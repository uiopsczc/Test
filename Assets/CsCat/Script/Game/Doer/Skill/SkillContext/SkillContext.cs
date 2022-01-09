//using UnityEngine;
//
//namespace CsCat
//{
//  public partial class SkillContext : Doer
//  {
//    public Scene parent_scene = null; // 主场景
//
//
//    public Critter attacker = null; // 攻击者
//    public Vector2Int attacker_pos; // 攻击方坐标
//    public Vector2Int attacker_pos_in_parent_scene; // 攻击方主场景坐标
//    public Scene attacker_scene = null; // 攻击方所在场景
//
//    public Critter target = null; // 目标
//    public Vector2Int target_pos; // 目标坐标
//    public Vector2Int target_pos_in_parent_scene; // 目标主场景坐标
//    public Scene target_scene = null; // 攻击方所在场景
//
//
//    public void SetAttacker(Critter attacker)
//    {
//      this.attacker_scene = attacker.GetEnv<Scene>();
//      if (this.attacker_scene.IsChildScene())
//        this.parent_scene = this.attacker_scene.GetEnv<Scene>();
//      else
//        this.parent_scene = this.attacker_scene;
//      this.attacker = attacker;
//      this.attacker_pos = attacker.GetPos();
//      this.attacker_pos_in_parent_scene = this.attacker_scene.ToParentPos(this.attacker_pos, this.parent_scene);
//    }
//
//    public void SetTarget(Critter target)
//    {
//      this.target_scene = target.GetEnv<Scene>();
//      this.target = target;
//      this.target_pos = target.GetPos();
//      this.target_pos_in_parent_scene = this.target_scene.ToParentPos(this.target_pos, this.parent_scene);
//    }
//
//    public void SetTarget(Scene target_scene, Vector2Int target_pos)
//    {
//      this.target_scene = target_scene;
//      this.target = null;
//      this.target_pos = target_pos;
//      this.target_pos_in_parent_scene = this.target_scene.ToParentPos(target_pos, this.parent_scene);
//    }
//  }
//}