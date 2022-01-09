using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIBlood : UIObject
	{

		private float slideFrom0To1Duration = 1;
		private float maxValue;
		private int sliderCount;
		private float toValue;
		private List<Color> sliderColorList;
		private Slider slider;
		private Image sliderFrontImage;
		private Image sliderBackImage;
		private SliderCat sliderCat;

		public void Init(Transform parentTransform, float maxValue, int? sliderCount, float? toValue, List<Color> sliderColorList = null)
		{
			base.Init();
			this.graphicComponent.SetParentTransform(parentTransform);
			this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIBoold.prefab");
			InitBlood(maxValue, sliderCount, toValue, sliderColorList);
		}

		protected void InitBlood(float maxValue, int? sliderCount, float? toValue, List<Color> sliderColorList = null)
		{
			this.maxValue = maxValue;
			this.sliderCount = sliderCount.GetValueOrDefault(1);
			this.toValue = toValue.GetValueOrDefault(this.maxValue);
			this.sliderColorList = this.sliderColorList ?? UIBloodConst.Color_List1;
		}

		//    public override GameObject InstantiateGameObject(GameObject prefab)
		//    {
		//      GameObject clone = Client.instance.uiManager.uiBloodManager.SpawnUIBloodGameObject();
		//      if (clone == null)
		//        clone = GameObject.Instantiate(prefab);
		//      return clone;
		//    }

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			this.slider = graphicComponent.transform.Find("slider").GetComponent<Slider>();
			this.sliderBackImage = graphicComponent.transform.FindChildRecursive("Background").GetComponent<Image>();
			this.sliderFrontImage = graphicComponent.transform.FindChildRecursive("Fill").GetComponent<Image>();
		}

		//    public override void OnAllAssetsLoadDone()
		//    {
		//      base.OnAllAssetsLoadDone();
		//      graphicComponent.SetIsNotDestroyGameObject(true);
		//      __OnAllAssetsLoadDone();
		//    }

		// spawn的时候重用
		public void __OnAllAssetsLoadDone()
		{
			var sliderInfo = this.__GetSliderInfoByValue(this.toValue);
			if (sliderCat != null)
				this.sliderCat.Init(this.slider, sliderInfo.index, this.slideFrom0To1Duration, sliderInfo.pct);
			else
				this.sliderCat =
				  new SliderCat(this.slider, sliderInfo.index, this.slideFrom0To1Duration, sliderInfo.pct);
			this.__SetSliderColor(this.sliderCat.curIndex);
			graphicComponent.SetIsShow(true);
		}

		public SliderInfo __GetSliderInfoByValue(float value)
		{
			int index = 0;
			float pct = 0;
			if (value == 0)
			{
				index = 0;
				pct = 0;
			}
			else if (value == this.maxValue)
			{
				index = this.sliderCount - 1;
				pct = 1;
			}
			else
			{
				float sliderEachValue = this.maxValue / this.sliderCount;
				index = (int)Mathf.Ceil(value / sliderEachValue);
				int intPart = (int)Mathf.Floor(value / sliderEachValue);
				float fractionalPart = value / sliderEachValue - intPart;
				pct = fractionalPart;
				if (intPart == index)
					pct = 1;
				index = index - 1;
			}

			return new SliderInfo(index, pct);
		}

		public void __SetSliderColor(int index)
		{
			var sliderBackColor = this.sliderColorList[index];
			var sliderFrontColor = this.sliderColorList[index + 1];
			this.sliderBackImage.color = sliderBackColor;
			this.sliderFrontImage.color = sliderFrontColor;
		}

		public Tween SlideTo(float toValue, Action<float, Tween> callback = null, float? maxValue = null,
		  int? sliderCount = null)
		{
			this.toValue = toValue;
			if (maxValue.HasValue)
				this.maxValue = maxValue.Value;
			if (sliderCount.HasValue)
				this.sliderCount = sliderCount.Value;
			if (this.sliderCat == null)
				return null;
			var sliderInfo = this.__GetSliderInfoByValue(toValue);
			return this.AddDOTween("UIBlood", this.sliderCat.SlideTo(sliderInfo.index, sliderInfo.pct,
			  (index, pct, nextTween) =>
			  {
				  this.__SetSliderColor(this.sliderCat.curIndex);
				  if (nextTween != null)
					  this.AddDOTween("UIBlood", nextTween);
				  if (callback != null)
				  {
					  var currentValue = this.sliderCat.GetCurrentValue() * (this.maxValue / this.sliderCount);
					  callback(currentValue, nextTween);
				  }
			  }));
		}

		protected override void _Destroy()
		{
			base._Destroy();
			graphicComponent.SetIsShow(false);
			Client.instance.uiManager.uiBloodManager.DespawnUIBloodGameObject(graphicComponent.gameObject);
		}
	}
}