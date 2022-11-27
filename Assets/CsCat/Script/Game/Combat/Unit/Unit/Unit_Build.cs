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
		private bool _isSettingModelPath;
		private bool _isLoadOk;
		private string _buildOkAnimationName;
		private readonly List<UnitMaterialInfo> _unitMaterialInfoList = new List<UnitMaterialInfo>();
		private readonly Dictionary<string, UnitModelInfo> _unitModelInfoDict = new Dictionary<string, UnitModelInfo>();
		private Hashtable _argDict;

		public void Build(Hashtable argDict)
		{
			this.unitLockTargetInfo = new UnitLockTargetInfo();
			this._normalAttackComboInfo = new ComboInfo();
			this._unitModelInfoDict.Clear();
			this._loadOkListenList.Clear();
			this._socketTransformDict.Clear();

			this._argDict = argDict;
			this.InitMixedStates();

			this.cfgUnitData = CfgUnit.Instance.GetById(this.unitId);
			this.name = this.cfgUnitData.name;
			this.type = this.cfgUnitData.type;
			this._radius = this.cfgUnitData.radius;
			this._originalRadius = this._radius;

			this._level = argDict.Get<int>("level");
			this.unitId = argDict.Get<string>("unitId");
			this.playerName = argDict.Get<string>("playerName");
			this.showNameOffset = argDict.Get<Vector2>("showNameOffset");
			this._isNotShowHeadBlood = argDict.Get<bool>("isNotShowHeadBlood");
			if (argDict.ContainsKey("ownerUnitGuid"))
				this.ownerUnit = Client.instance.combat.unitManager.GetUnit(argDict.Get<string>("ownerUnitGuid"));
			//创建时播放的动画
			this._buildOkAnimationName = argDict.Get<string>("buildOkAnimationName");
			//是否需要保持尸体
			this.isKeepDeadBody =
			  argDict.GetOrGetDefault2("isKeepDeadBody", () => this.cfgUnitData.isKeepDeadBody);


			this.faction = argDict.Get<string>("faction");
			this._position = argDict.Get<Vector3>("position");
			this._rotation = argDict.Get<Quaternion>("rotation");
			this._scale =
			  argDict.GetOrGetDefault2<float>("scale", () => this.cfgUnitData.scale == 0 ? 1 : this.cfgUnitData.scale);
			this.SetScale(this._scale);


			this.propertyComp = new PropertyComp(argDict);
			this.animatorComp = new AnimatorComp();
			this.unitMoveComp = this.AddChild<UnitMoveComp>(null, this);
			this.buffManager = this.AddChild<BuffManager>(null, this);

			this.spellInfoDict.Clear();

			//技能相关
			this.skillIdList = this.cfgUnitData.skillIds.ToList();
			for (var i = 0; i < this.skillIdList.Count; i++)
			{
				var skillId = this.skillIdList[i];
				this.AddSkill(skillId);
			}

			//普攻相关
			this._normalAttackIdList = this.cfgUnitData.normalAttackIds.ToList();
			for (var i = 0; i < this._normalAttackIdList.Count; i++)
			{
				var normalAttackId = this._normalAttackIdList[i];
				this.AddNormalAttack(normalAttackId);
			}

			//添加被动buff
			if (!this.cfgUnitData.passiveBuffIds.IsNullOrEmpty())
			{
				for (var i = 0; i < cfgUnitData.passiveBuffIds.Length; i++)
				{
					var passiveBuffId = cfgUnitData.passiveBuffIds[i];
					this.buffManager.AddBuff(passiveBuffId, this);
				}
			}

			if (!this.cfgUnitData.modelPath.IsNullOrWhiteSpace())
				this.BuildModel(this.cfgUnitData.modelPath);

			this.unitMoveComp.OnBuild();
			this.animatorComp.OnBuild();
			this.propertyComp.OnBuild(this);

			if (argDict.ContainsKey("hpPct"))
				this.SetHp((int)(this.GetMaxHp() * argDict.Get<float>("hpPct")), true);
			else
				this.SetHp(argDict.GetOrGetDefault2("hp", () => this.GetMaxHp()), true);

			this.UpdateMixedStates();

		}

		private void BuildModel(string model_path)
		{
			//      Client.instance.combat.effectManager.DeAttach(this);
			this._ClearModel();
			this._StartChangeModel();
			this._SetModel("main", model_path);
			this._FinishChangeModel();
		}

		private void _ClearModel()
		{
			if (graphicComponent.gameObject != null)
				graphicComponent.gameObject.Destroy();
			graphicComponent.SetGameObject(null, null);
			this.animation = null;
			this._socketTransformDict.Clear();
			;
			this._unitMaterialInfoList.Clear();
		}

		private void _StartChangeModel()
		{
			this._isSettingModelPath = true;
			this._isLoadOk = false;
		}

		private void _SetModel(string tag, string modelPath, Type modelType = null)
		{
			modelType = modelType == null ? typeof(GameObject) : modelType;
			if (modelPath == null)
			{
				this._unitModelInfoDict.Remove(tag);
				return;
			}

			if (!_unitModelInfoDict.ContainsKey(tag))
				this._unitModelInfoDict[tag] = new UnitModelInfo();
			var unitModelInfo = this._unitModelInfoDict[tag];
			if (modelPath.Equals(unitModelInfo.path))
				return;
			unitModelInfo.path = modelPath;
			unitModelInfo.prefab = null;
			this.resLoadComponent.GetOrLoadAsset(modelPath, (assetCat) =>
			{
				var prefab = assetCat.Get(modelPath.GetSubAssetPath(), modelType);
				this._OnLoadOK(prefab, tag);
			}, null, null, this);
		}

		private void _OnLoadOK(Object prefab, string tag)
		{
			if (!this._unitModelInfoDict.ContainsKey(tag))
				return;
			var unitModelInfo = this._unitModelInfoDict[tag];
			unitModelInfo.prefab = prefab;
			this._CheckAllLoadOK();
		}

		private void _CheckAllLoadOK()
		{
			if (this._isSettingModelPath || graphicComponent.gameObject != null)
				return;
			foreach (var keyValue in this._unitModelInfoDict)
			{
				var curUnitModelInfo = keyValue.Value;
				if (curUnitModelInfo.prefab == null)
					return;
			}

			var unitModelInfo = this._unitModelInfoDict["main"];
			var clone = GameObject.Instantiate(unitModelInfo.prefab, this.GetPosition(), this.GetRotation(),
			  this.parent.graphicComponent.transform) as GameObject;
			clone.name = string.Format("{0}:{1}", this.unitId, this.key);
			graphicComponent.SetGameObject(clone, false);
			this._OnBuildOK();
			this._isLoadOk = true;
			this._OnLoadOKListen();
		}

		public void _OnLoadOKListen()
		{
			for (var i = 0; i < this._loadOkListenList.Count; i++)
			{
				var action = this._loadOkListenList[i];
				action();
			}

			this._loadOkListenList.Clear();
		}

		private void _OnBuildOK()
		{
			this.SetPosition(this._position);
			this.SetRotation(this._rotation);
			this._orginalTransformScale = graphicComponent.transform.localScale;
			graphicComponent.transform.localScale = this._orginalTransformScale * this._scale;

			this.InitAnimation();
			this.InitMaterial();

			this.unitMoveComp.OnBuildOk();
			this.animatorComp.OnBuildOk(graphicComponent.gameObject);
			this.FireEvent(null, UnitEventNameConst.On_Unit_Build_Ok, this);
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
				if (!this._buildOkAnimationName.IsNullOrWhiteSpace() && this.animation[this._buildOkAnimationName])
				{
					this.PlayAnimation(AnimationNameConst.idle);
					this.PlayAnimation(this._buildOkAnimationName);
					var buildOkAnimationState = this.animation[_buildOkAnimationName];
					graphicComponent.transform.position = new Vector3(0.01f, 0.01f, 0.01f);
					this.AddTimer((arg) =>
					{
						this.SetPosition(this._position);
						this.SetRotation(this._rotation);
						return false;
					}, buildOkAnimationState.length);
				}
				else
					this.PlayAnimation(AnimationNameConst.idle);
			}
		}

		private void InitMaterial()
		{
			this._unitMaterialInfoList.Clear();
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
						this._unitMaterialInfoList.Add(unitMaterialInfo);
					}
				}
			}

			this._changeColorDict.Clear();
		}

		private void _FinishChangeModel()
		{
			this._isSettingModelPath = false;
			this._CheckAllLoadOK();
		}
	}
}