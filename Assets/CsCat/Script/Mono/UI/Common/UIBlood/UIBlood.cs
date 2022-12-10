using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIBlood : UIObject
	{
		private float _slideFrom0To1Duration = 1;
		private float _maxValue;
		private int _sliderCount;
		private float _toValue;
		private List<Color> _sliderColorList;
		private Slider _Slider_Blood;
		private Image _ImgC_Fill;
		private Image _ImgC_Bg;
		private SliderCat _sliderCat;

		protected void _Init(Transform parentTransform, float maxValue, int? sliderCount, float? toValue, List<Color> sliderColorList = null)
		{
			base._Init();
			this.AddChild<DOTweenDictTreeNode>(null, new DOTweenDict());
			this.SetParentTransform(parentTransform);
			this.SetPrefabPath("Assets/PatchResources/UI/UIBlood/Prefab/UIBlood.prefab");
			InitBlood(maxValue, sliderCount, toValue, sliderColorList);
		}

		protected void InitBlood(float maxValue, int? sliderCount, float? toValue, List<Color> sliderColorList = null)
		{
			this._maxValue = maxValue;
			this._sliderCount = sliderCount.GetValueOrDefault(1);
			this._toValue = toValue.GetValueOrDefault(this._maxValue);
			this._sliderColorList = this._sliderColorList ?? UIBloodConst.Color_List1;
		}

		protected override GameObject _InstantiateGameObject(GameObject prefab)
		{
			return this.GetPoolManager().GetOrAddGameObjectPool("UIBloodPool", prefab, "UIBlood").SpawnValue();
		}

		protected override void _InitGameObjectChildren()
		{
			base._InitGameObjectChildren();
			this._Slider_Blood = this.GetTransform().Find("Slider_Blood").GetComponent<Slider>();
			this._ImgC_Bg = this.GetTransform().Find("Slider_Blood/ImgC_Bg").GetComponent<Image>();
			this._ImgC_Fill = this.GetTransform().Find("Slider_Blood/ImgC_Fg/ImgC_Fill").GetComponent<Image>();
		}

		// spawn的时候重用
		protected override void _PostSetGameObject()
		{
			base._PostSetGameObject();
			var sliderInfo = this.GetSliderInfoByValue(this._toValue);
			if (_sliderCat != null)
				this._sliderCat.Init(this._Slider_Blood, sliderInfo.index, this._slideFrom0To1Duration, sliderInfo.pct);
			else
				this._sliderCat =
				  new SliderCat(this._Slider_Blood, sliderInfo.index, this._slideFrom0To1Duration, sliderInfo.pct);
			this.SetSliderColor(this._sliderCat.curIndex);
		}

		public SliderInfo GetSliderInfoByValue(float value)
		{
			int index = 0;
			float pct = 0;
			if (value == 0)
			{
				index = 0;
				pct = 0;
			}
			else if (value == this._maxValue)
			{
				index = this._sliderCount - 1;
				pct = 1;
			}
			else
			{
				float sliderEachValue = this._maxValue / this._sliderCount;
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

		public void SetSliderColor(int index)
		{
			var sliderBackColor = this._sliderColorList[index];
			var sliderFrontColor = this._sliderColorList[index + 1];
			this._ImgC_Bg.color = sliderBackColor;
			this._ImgC_Fill.color = sliderFrontColor;
		}

		public Tween SlideTo(float toValue, Action<float, Tween> callback = null, float? maxValue = null,
		  int? sliderCount = null)
		{
			this._toValue = toValue;
			if (maxValue.HasValue)
				this._maxValue = maxValue.Value;
			if (sliderCount.HasValue)
				this._sliderCount = sliderCount.Value;
			if (this._sliderCat == null)
				return null;
			var sliderInfo = this.GetSliderInfoByValue(toValue);
			var dotweenDictTreeNode = this.GetChild<DOTweenDictTreeNode>();
			return dotweenDictTreeNode.AddDOTween("UIBlood", this._sliderCat.SlideTo(sliderInfo.index, sliderInfo.pct,
			  (index, pct, nextTween) =>
			  {
				  this.SetSliderColor(this._sliderCat.curIndex);
				  if (nextTween != null)
					  dotweenDictTreeNode.AddDOTween("UIBlood", nextTween);
				  if (callback != null)
				  {
					  var currentValue = this._sliderCat.GetCurrentValue() * (this._maxValue / this._sliderCount);
					  callback(currentValue, nextTween);
				  }
			  }));
		}

		protected override void _DestroyGameObject()
		{
			var gameObject = this.GetGameObject();
			if (gameObject != null)
				this.GetPoolManager().GetGameObjectPool("UIBloodPool").DespawnValue(gameObject);
		}
	}
}