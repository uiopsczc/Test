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
			var slider_info = this.__GetSliderInfoByValue(this.toValue);
			if (sliderCat != null)
				this.sliderCat.Init(this.slider, slider_info.index, this.slideFrom0To1Duration, slider_info.pct);
			else
				this.sliderCat =
				  new SliderCat(this.slider, slider_info.index, this.slideFrom0To1Duration, slider_info.pct);
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
				float slider_each_value = this.maxValue / this.sliderCount;
				index = (int)Mathf.Ceil(value / slider_each_value);
				int int_part = (int)Mathf.Floor(value / slider_each_value);
				float fractional_part = value / slider_each_value - int_part;
				pct = fractional_part;
				if (int_part == index)
					pct = 1;
				index = index - 1;
			}

			return new SliderInfo(index, pct);
		}

		public void __SetSliderColor(int index)
		{
			var slider_back_color = this.sliderColorList[index];
			var slider_front_color = this.sliderColorList[index + 1];
			this.sliderBackImage.color = slider_back_color;
			this.sliderFrontImage.color = slider_front_color;
		}

		public Tween SlideTo(float to_value, Action<float, Tween> callback = null, float? max_value = null,
		  int? slider_count = null)
		{
			this.toValue = to_value;
			if (max_value.HasValue)
				this.maxValue = max_value.Value;
			if (slider_count.HasValue)
				this.sliderCount = slider_count.Value;
			if (this.sliderCat == null)
				return null;
			var slider_info = this.__GetSliderInfoByValue(to_value);
			return this.AddDOTween("UIBlood", this.sliderCat.SlideTo(slider_info.index, slider_info.pct,
			  (index, pct, next_tween) =>
			  {
				  this.__SetSliderColor(this.sliderCat.curIndex);
				  if (next_tween != null)
					  this.AddDOTween("UIBlood", next_tween);
				  if (callback != null)
				  {
					  var current_value = this.sliderCat.GetCurrentValue() * (this.maxValue / this.sliderCount);
					  callback(current_value, next_tween);
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