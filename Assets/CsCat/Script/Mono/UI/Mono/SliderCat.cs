using System;
using DG.Tweening;
using UnityEngine.UI;

namespace CsCat
{
	public class SliderCat
	{
		private Slider slider;
		private float duration; // slide from 0 to 1 duration
		public int curIndex;

		public SliderCat(Slider slider, int curIndex, float duration, float? curPCT = null)
		{
			Init(slider, curIndex, duration, curPCT);
		}

		public void Init(Slider slider, int curIndex, float duration, float? curPCT = null)
		{
			this.slider = slider;
			this.curIndex = curIndex;
			this.duration = duration;
			if (curPCT.HasValue)
				this.slider.value = curPCT.Value;
		}

		public float GetCurrentValue()
		{
			return this.curIndex + this.slider.value;
		}

		public Tween SlideTo(int toIndex, float toPCT, Action<int, float, Tween> callback = null)
		{
			if (curIndex == toIndex)
			{
				float tweenDuration = Math.Abs(this.slider.value - toPCT) * this.duration;
				Tween tween = this.slider.DOValue(toPCT, tweenDuration).SetEase(Ease.Linear);
				if (callback != null)
					tween.OnComplete(() => callback(toIndex, toPCT, null));
				return tween;
			}
			else
			{
				bool isToLargeIndex = curIndex < toIndex; //是否是向更大的index滑动
				float tweenToPCT = isToLargeIndex ? 1 : 0;
				float tweenDuration = Math.Abs(this.slider.value - tweenToPCT);
				Tween tween = this.slider.DOValue(tweenToPCT, tweenDuration).SetEase(Ease.Linear);
				tween.OnComplete(() =>
				{
					if (isToLargeIndex) //向更大的index滑动
					{
						this.slider.value = 0;
						this.curIndex = this.curIndex + 1;
					}
					else
					{
						this.slider.value = 1;
						this.curIndex = this.curIndex - 1;
					}

					Tween nextTween = SlideTo(toIndex, toPCT, callback);
					callback?.Invoke(this.curIndex, this.slider.value, nextTween);
				});
				return tween;
			}
		}
	}
}