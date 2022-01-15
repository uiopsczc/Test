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
		private bool isSettingModelPath;
		private bool isLoadOk;
		private string buildOkAnimationName;
		private List<UnitMaterialInfo> unitMaterialInfoList = new List<UnitMaterialInfo>();
		private Dictionary<string, UnitModelInfo> unitModelInfoDict = new Dictionary<string, UnitModelInfo>();
		private Hashtable argDict;

		public void Build(Hashtable argDict)
		{
			this.unitLockTargetInfo = new UnitLockTargetInfo();
			this.normalAttackComboInfo = new ComboInfo();
			this.unitModelInfoDict.Clear();
			this.loadOkListenList.Clear();
			this.socketTransformDict.Clear();

			this.argDict = argDict;
			this.InitMixedStates();

			this.cfgUnitData = CfgUnit.Instance.get_by_id(this.unitId);
			this.name = this.cfgUnitData.name;
			this.type = this.cfgUnitData.type;
			this.radius = this.cfgUnitData.radius;
			this.originalRadius = this.radius;

			this.level = argDict.Get<int>("level");
			this.unitId = argDict.Get<string>("unit_id");
			this.playerName = argDict.Get<string>("player_name");
			this.showNameOffset = argDict.Get<Vector2>("show_name_offset");
			this.isNotShowHeadBlood = argDict.Get<bool>("is_not_show_headBlood");
			if (argDict.ContainsKey("owner_unit_guid"))
				this.ownerUnit = Client.instance.combat.unitManager.GetUnit(argDict.Get<string>("owner_unit_guid"));
			//创建时播放的动画
			this.buildOkAnimationName = argDict.Get<string>("build_ok_animation_name");
			//是否需要保持尸体
			this.isKeepDeadBody =
			  argDict.GetOrGetDefault2("is_keep_dead_body", () => this.cfgUnitData.is_keep_dead_body);


			this.faction = argDict.Get<string>("faction");
			this.position = argDict.Get<Vector3>("position");
			this.rotation = argDict.Get<Quaternion>("rotation");
			this.scale =
			  argDict.GetOrGetDefault2<float>("scale", () => this.cfgUnitData.scale == 0 ? 1 : this.cfgUnitData.scale);
			this.SetScale(this.scale);


			this.propertyComp = new PropertyComp(argDict);
			this.animatorComp = new AnimatorComp();
			this.unitMoveComp = this.AddChild<UnitMoveComp>(null, this);
			this.buffManager = this.AddChild<BuffManager>(null, this);

			this.spellInfoDict.Clear();

			//技能相关
			this.skillIdList = this.cfgUnitData._skill_ids.ToList();
			for (var i = 0; i < this.skillIdList.Count; i++)
			{
				var skillId = this.skillIdList[i];
				this.AddSkill(skillId);
			}

			//普攻相关
			this.normalAttackIdList = this.cfgUnitData._normal_attack_ids.ToList();
			for (var i = 0; i < this.normalAttackIdList.Count; i++)
			{
				var normalAttackId = this.normalAttackIdList[i];
				this.AddNormalAttack(normalAttackId);
			}

			//添加被动buff
			if (!this.cfgUnitData._passive_buff_ids.IsNullOrEmpty())
			{
				for (var i = 0; i < cfgUnitData._passive_buff_ids.Length; i++)
				{
					var passiveBuffId = cfgUnitData._passive_buff_ids[i];
					this.buffManager.AddBuff(passiveBuffId, this);
				}
			}

			if (!this.cfgUnitData.model_path.IsNullOrWhiteSpace())
				this.BuildModel(this.cfgUnitData.model_path);

			this.unitMoveComp.OnBuild();
			this.animatorComp.OnBuild();
			this.propertyComp.OnBuild(this);

			if (argDict.ContainsKey("hp_pct"))
				this.SetHp((int)(this.GetMaxHp() * argDict.Get<float>("hp_pct")), true);
			else
				this.SetHp(argDict.GetOrGetDefault2("hp", () => this.GetMaxHp()), true);

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
			this.socketTransformDict.Clear();
			;
			this.unitMaterialInfoList.Clear();
		}

		private void __StartChangeModel()
		{
			this.isSettingModelPath = true;
			this.isLoadOk = false;
		}

		private void __SetModel(string tag, string modelPath, Type modelType = null)
		{
			modelType = modelType == null ? typeof(GameObject) : modelType;
			if (modelPath == null)
			{
				this.unitModelInfoDict.Remove(tag);
				return;
			}

			if (!unitModelInfoDict.ContainsKey(tag))
				this.unitModelInfoDict[tag] = new UnitModelInfo();
			var unitModelInfo = this.unitModelInfoDict[tag];
			if (modelPath.Equals(unitModelInfo.path))
				return;
			unitModelInfo.path = modelPath;
			unitModelInfo.prefab = null;
			this.resLoadComponent.GetOrLoadAsset(modelPath, (assetCat) =>
			{
				var prefab = assetCat.Get(modelPath.GetSubAssetPath(), modelType);
				this.__OnLoadOK(prefab, tag);
			}, null, null, this);
		}

		private void __OnLoadOK(Object prefab, string tag)
		{
			if (!this.unitModelInfoDict.ContainsKey(tag))
				return;
			var unitModelInfo = this.unitModelInfoDict[tag];
			unitModelInfo.prefab = prefab;
			this.__CheckAllLoadOK();
		}

		private void __CheckAllLoadOK()
		{
			if (this.isSettingModelPath || graphicComponent.gameObject != null)
				return;
			foreach (var keyValue in this.unitModelInfoDict)
			{
				var curUnitModelInfo = keyValue.Value;
				if (curUnitModelInfo.prefab == null)
					return;
			}

			var unitModelInfo = this.unitModelInfoDict["main"];
			var clone = GameObject.Instantiate(unitModelInfo.prefab, this.GetPosition(), this.GetRotation(),
			  this.parent.graphicComponent.transform) as GameObject;
			clone.name = string.Format("{0}:{1}", this.unitId, this.key);
			graphicComponent.SetGameObject(clone, false);
			this.__OnBuildOK();
			this.isLoadOk = true;
			this.__OnLoadOKListen();
		}

		public void __OnLoadOKListen()
		{
			foreach (var action in this.loadOkListenList)
				action();
			this.loadOkListenList.Clear();
		}

		private void __OnBuildOK()
		{
			this.SetPosition(this.position);
			this.SetRotation(this.rotation);
			this.orginalTransformScale = graphicComponent.transform.localScale;
			graphicComponent.transform.localScale = this.orginalTransformScale * this.scale;

			this.InitAnimation();
			this.InitMaterial();

			this.unitMoveComp.OnBuildOk();
			this.animatorComp.OnBuildOk(graphicComponent.gameObject);
			this.Broadcast(null, UnitEventNameConst.On_Unit_Build_Ok, this);
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
				var walkAnimationState = this.animation[AnimationNameConst.walk];
				if (walkAnimationState != null)
					walkAnimationState.wrapMode = WrapMode.Loop;
				var idleAnimationState = this.animation[AnimationNameConst.idle];
				if (idleAnimationState != null)
				{
					idleAnimationState.wrapMode = WrapMode.Loop;
					if (this.actionManager == null)
						idleAnimationState.layer = -1;
				}

				var dieAnimationState = this.animation[AnimationNameConst.die];
				if (dieAnimationState != null)
					dieAnimationState.wrapMode = WrapMode.ClampForever;
				if (!this.buildOkAnimationName.IsNullOrWhiteSpace() && this.animation[this.buildOkAnimationName])
				{
					this.PlayAnimation(AnimationNameConst.idle);
					this.PlayAnimation(this.buildOkAnimationName);
					var buildOkAnimationState = this.animation[buildOkAnimationName];
					graphicComponent.transform.position = new Vector3(0.01f, 0.01f, 0.01f);
					this.AddTimer((arg) =>
					{
						this.SetPosition(this.position);
						this.SetRotation(this.rotation);
						return false;
					}, buildOkAnimationState.length);
				}
				else
					this.PlayAnimation(AnimationNameConst.idle);
			}
		}

		private void InitMaterial()
		{
			this.unitMaterialInfoList.Clear();
			var rendererTypeList = new List<Type> { typeof(MeshRenderer), typeof(SkinnedMeshRenderer) };
			for (var m = 0; m < rendererTypeList.Count; m++)
			{
				var rendererType = rendererTypeList[m];
				var rendererList = graphicComponent.gameObject.GetComponentsInChildren(rendererType);
				for (int n = 0; n < rendererList.Length; n++)
				{
					Renderer render = rendererList[n] as Renderer;
					var material = render.material;
					if (material.HasProperty("_Color"))
					{
						var unitMaterialInfo = new UnitMaterialInfo();
						unitMaterialInfo.material = material;
						unitMaterialInfo.color = material.color;
						this.unitMaterialInfoList.Add(unitMaterialInfo);
					}
				}
			}

			this.changeColorDict.Clear();
		}

		private void __FinishChangeModel()
		{
			this.isSettingModelPath = false;
			this.__CheckAllLoadOK();
		}
	}
}