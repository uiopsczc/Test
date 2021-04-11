using System;
using DG.Tweening;
using UnityEngine.UI;

namespace CsCat
{
  public class SliderCat
  {
    private Slider slider;
    private float duration; // slide from 0 to 1 duration
    public int cur_index;

    public SliderCat(Slider slider, int cur_index, float duration, float? cur_pct = null)
    {
      Init(slider, cur_index, duration, cur_pct);
    }

    public void Init(Slider slider, int cur_index, float duration, float? cur_pct = null)
    {
      this.slider = slider;
      this.cur_index = cur_index;
      this.duration = duration;
      if (cur_pct.HasValue)
        this.slider.value = cur_pct.Value;
    }

    public float GetCurrentValue()
    {
      return this.cur_index + this.slider.value;
    }

    public Tween SlideTo(int to_index, float to_pct, Action<int, float, Tween> callback = null)
    {
      if (cur_index == to_index)
      {
        float tween_druation = Math.Abs(this.slider.value - to_pct) * this.duration;
        Tween tween = this.slider.DOValue(to_pct, tween_druation).SetEase(Ease.Linear);
        if (callback != null)
          tween.OnComplete(() => callback(to_index, to_pct, null));
        return tween;
      }
      else
      {
        bool is_to_large_index = cur_index < to_index; //是否是向更大的index滑动
        float tween_to_pct = is_to_large_index ? 1 : 0;
        float tween_druation = Math.Abs(this.slider.value - tween_to_pct);
        Tween tween = this.slider.DOValue(tween_to_pct, tween_druation).SetEase(Ease.Linear);
        tween.OnComplete(() =>
        {
          if (is_to_large_index) //向更大的index滑动
          {
            this.slider.value = 0;
            this.cur_index = this.cur_index + 1;
          }
          else
          {
            this.slider.value = 1;
            this.cur_index = this.cur_index - 1;
          }

          Tween next_tween = SlideTo(to_index, to_pct, callback);
          callback?.Invoke(this.cur_index, this.slider.value, next_tween);
        });
        return tween;
      }
    }
  }
}