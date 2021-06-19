using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
  public partial class Unit
  {
    //构建模型相关
    private bool is_setting_model_path;
    private bool is_load_ok;
    private string build_ok_animation_name;
    private List<UnitMaterialInfo> unitMaterialInfo_list = new List<UnitMaterialInfo>();
    private Dictionary<string, UnitModelInfo> unitModelInfo_dict = new Dictionary<string, UnitModelInfo>();
    private Hashtable arg_dict;

    public void Build(Hashtable arg_dict)
    {
      this.unitLockTargetInfo = new UnitLockTargetInfo();
      this.normal_attack_comboInfo = new ComboInfo();
      this.unitModelInfo_dict.Clear();
      this.load_ok_listen_list.Clear();
      this.socket_transform_dict.Clear();

      this.arg_dict = arg_dict;
      this.InitMixedStates();

      this.cfgUnitData = CfgUnit.Instance.get_by_id(this.unit_id);
      this.name = this.cfgUnitData.name;
      this.type = this.cfgUnitData.type;
      this.radius = this.cfgUnitData.radius;
      this.original_radius = this.radius;

      this.level = arg_dict.Get<int>("level");
      this.unit_id = arg_dict.Get<string>("unit_id");
      this.player_name = arg_dict.Get<string>("player_name");
      this.show_name_offset = arg_dict.Get<Vector2>("show_name_offset");
      this.is_not_show_headBlood = arg_dict.Get<bool>("is_not_show_headBlood");
      if (arg_dict.ContainsKey("owner_unit_guid"))
        this.owner_unit = Client.instance.combat.unitManager.GetUnit(arg_dict.Get<string>("owner_unit_guid"));
      //创建时播放的动画
      this.build_ok_animation_name = arg_dict.Get<string>("build_ok_animation_name");
      //是否需要保持尸体
      this.is_keep_dead_body =
        arg_dict.GetOrGetDefault("is_keep_dead_body", () => this.cfgUnitData.is_keep_dead_body);


      this.faction = arg_dict.Get<string>("faction");
      this.position = arg_dict.Get<Vector3>("position");
      this.rotation = arg_dict.Get<Quaternion>("rotation");
      this.scale =
        arg_dict.GetOrGetDefault<float>("scale", () => this.cfgUnitData.scale == 0 ? 1 : this.cfgUnitData.scale);
      this.SetScale(this.scale);


      this.propertyComp = new PropertyComp(arg_dict);
      this.animatorComp = new AnimatorComp();
      this.unitMoveComp = this.AddChild<UnitMoveComp>(null, this);
      this.buffManager = this.AddChild<BuffManager>(null, this);

      this.spellInfo_dict.Clear();

      //技能相关
      this.skill_id_list = this.cfgUnitData._skill_ids.ToList();
      foreach (var skill_id in this.skill_id_list)
        this.AddSkill(skill_id);

      //普攻相关
      this.normal_attack_id_list = this.cfgUnitData._normal_attack_ids.ToList();
      foreach (var normal_attack_id in this.normal_attack_id_list)
        this.AddNormalAttack(normal_attack_id);

      //添加被动buff
      if (!this.cfgUnitData._passive_buff_ids.IsNullOrEmpty())
      {
        foreach (var passive_buff_id in cfgUnitData._passive_buff_ids)
          this.buffManager.AddBuff(passive_buff_id, this);
      }

      if (!this.cfgUnitData.model_path.IsNullOrWhiteSpace())
        this.BuildModel(this.cfgUnitData.model_path);

      this.unitMoveComp.OnBuild();
      this.animatorComp.OnBuild();
      this.propertyComp.OnBuild(this);

      if (arg_dict.ContainsKey("hp_pct"))
        this.SetHp((int)(this.GetMaxHp() * arg_dict.Get<float>("hp_pct")), true);
      else
        this.SetHp(arg_dict.GetOrGetDefault("hp", () => this.GetMaxHp()), true);

      this.UpdateMixedStates();

    }

    private void BuildModel(string model_path)
    {
      //      Client.instance.combat.effectManager.DeAttach(this);
      this.__ClearModel();
      this.__StartChangeModel();
      this.__SetModel("main", model_path);
      this.__FinishChangeModel();
    }

    private void __ClearModel()
    {
      if (graphicComponent.gameObject != null)
        graphicComponent.gameObject.Destroy();
      graphicComponent.SetGameObject(null, null);
      this.animation = null;
      this.socket_transform_dict.Clear();
      ;
      this.unitMaterialInfo_list.Clear();
    }

    private void __StartChangeModel()
    {
      this.is_setting_model_path = true;
      this.is_load_ok = false;
    }

    private void __SetModel(string tag, string model_path, Type model_type = null)
    {
      model_type = model_type == null ? typeof(GameObject) : model_type;
      if (model_path == null)
      {
        this.unitModelInfo_dict.Remove(tag);
        return;
      }

      if (!unitModelInfo_dict.ContainsKey(tag))
        this.unitModelInfo_dict[tag] = new UnitModelInfo();
      var unitModelInfo = this.unitModelInfo_dict[tag];
      if (model_path.Equals(unitModelInfo.path))
        return;
      unitModelInfo.path = model_path;
      unitModelInfo.prefab = null;
      this.resLoadComponent.GetOrLoadAsset(model_path, (assetCat) =>
      {
        var prefab = assetCat.Get(model_path.GetSubAssetPath(), model_type);
        this.__OnLoadOK(prefab, tag);
      }, null, null, this);
    }

    private void __OnLoadOK(Object prefab, string tag)
    {
      if (!this.unitModelInfo_dict.ContainsKey(tag))
        return;
      var unitModelInfo = this.unitModelInfo_dict[tag];
      unitModelInfo.prefab = prefab;
      this.__CheckAllLoadOK();
    }

    private void __CheckAllLoadOK()
    {
      if (this.is_setting_model_path || graphicComponent.gameObject != null)
        return;
      foreach (var _unitModelInfo in this.unitModelInfo_dict.Values)
      {
        if (_unitModelInfo.prefab == null)
          return;
      }

      var unitModelInfo = this.unitModelInfo_dict["main"];
      var clone = GameObject.Instantiate(unitModelInfo.prefab, this.GetPosition(), this.GetRotation(),
        this.parent.graphicComponent.transform) as GameObject;
      clone.name = string.Format("{0}:{1}", this.unit_id, this.key);
      graphicComponent.SetGameObject(clone, false);
      this.__OnBuildOK();
      this.is_load_ok = true;
      this.__OnLoadOKListen();
    }

    public void __OnLoadOKListen()
    {
      foreach (var action in this.load_ok_listen_list)
        action();
      this.load_ok_listen_list.Clear();
    }

    private void __OnBuildOK()
    {
      this.SetPosition(this.position);
      this.SetRotation(this.rotation);
      this.orginal_transform_sacle = graphicComponent.transform.localScale;
      graphicComponent.transform.localScale = this.orginal_transform_sacle * this.scale;

      this.InitAnimation();
      this.InitMaterial();

      this.unitMoveComp.OnBuildOk();
      this.animatorComp.OnBuildOk(graphicComponent.gameObject);
      this.Broadcast(UnitEventNameConst.On_Unit_Build_Ok, this);
    }

    private void InitAnimation()
    {
      this.animation = graphicComponent.gameObject.GetComponentInChildren<Animation>();
      if (this.animationCullingType != null)
      {
        this.SetAnimationCullingType(this.animationCullingType.Value);
        this.animationCullingType = null;
      }

      if (this.animation != null)
      {
        var walk_animationState = this.animation[AnimationNameConst.walk];
        if (walk_animationState != null)
          walk_animationState.wrapMode = WrapMode.Loop;
        var idle_animationState = this.animation[AnimationNameConst.idle];
        if (idle_animationState != null)
        {
          idle_animationState.wrapMode = WrapMode.Loop;
          if (this.actionManager == null)
            idle_animationState.layer = -1;
        }

        var die_animationState = this.animation[AnimationNameConst.die];
        if (die_animationState != null)
          die_animationState.wrapMode = WrapMode.ClampForever;
        if (!this.build_ok_animation_name.IsNullOrWhiteSpace() && this.animation[this.build_ok_animation_name])
        {
          this.PlayAnimation(AnimationNameConst.idle);
          this.PlayAnimation(this.build_ok_animation_name);
          var build_ok_animationState = this.animation[build_ok_animation_name];
          graphicComponent.transform.position = new Vector3(0.01f, 0.01f, 0.01f);
          this.AddTimer((arg) =>
          {
            this.SetPosition(this.position);
            this.SetRotation(this.rotation);
            return false;
          }, build_ok_animationState.length);
        }
        else
          this.PlayAnimation(AnimationNameConst.idle);
      }
    }

    private void InitMaterial()
    {
      this.unitMaterialInfo_list.Clear();
      var renderer_type_list = new List<Type> { typeof(MeshRenderer), typeof(SkinnedMeshRenderer) };
      foreach (var renderer_type in renderer_type_list)
      {
        var render_list = graphicComponent.gameObject.GetComponentsInChildren(renderer_type);
        for (int i = 0; i < render_list.Length; i++)
        {
          Renderer render = render_list[i] as Renderer;
          var material = render.material;
          if (material.HasProperty("_Color"))
          {
            var unitMaterialInfo = new UnitMaterialInfo();
            unitMaterialInfo.material = material;
            unitMaterialInfo.color = material.color;
            this.unitMaterialInfo_list.Add(unitMaterialInfo);
          }
        }
      }

      this.change_color_dict.Clear();
    }

    private void __FinishChangeModel()
    {
      this.is_setting_model_path = false;
      this.__CheckAllLoadOK();
    }
  }
}