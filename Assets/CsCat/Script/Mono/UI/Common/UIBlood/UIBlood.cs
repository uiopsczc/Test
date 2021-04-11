using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public partial class UIBlood : UIObject
  {

    private float slide_from_0_to_1_duration = 1;
    private float max_value;
    private int slider_count;
    private float to_value;
    private List<Color> slider_color_list;
    private Slider slider;
    private Image slider_front_image;
    private Image slider_back_image;
    private SliderCat sliderCat;

    public void Init(Transform parent_transform, float max_value, int? slider_count, float? to_value, List<Color> slider_color_list = null)
    {
      base.Init();
      this.graphicComponent.SetParentTransform(parent_transform);
      this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIBoold.prefab");
      InitBlood(max_value, slider_count, to_value, slider_color_list);
    }

    protected void InitBlood(float max_value, int? slider_count, float? to_value, List<Color> slider_color_list = null)
    {
      this.max_value = max_value;
      this.slider_count = slider_count.GetValueOrDefault(1);
      this.to_value = to_value.GetValueOrDefault(this.max_value);
      this.slider_color_list = this.slider_color_list ?? UIBloodConst.Color_List1;
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
      this.slider_back_image = graphicComponent.transform.FindChildRecursive("Background").GetComponent<Image>();
      this.slider_front_image = graphicComponent.transform.FindChildRecursive("Fill").GetComponent<Image>();
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
      var slider_info = this.__GetSliderInfoByValue(this.to_value);
      if (sliderCat != null)
        this.sliderCat.Init(this.slider, slider_info.index, this.slide_from_0_to_1_duration, slider_info.pct);
      else
        this.sliderCat =
          new SliderCat(this.slider, slider_info.index, this.slide_from_0_to_1_duration, slider_info.pct);
      this.__SetSliderColor(this.sliderCat.cur_index);
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
      else if (value == this.max_value)
      {
        index = this.slider_count - 1;
        pct = 1;
      }
      else
      {
        float slider_each_value = this.max_value / this.slider_count;
        index = (int) Mathf.Ceil(value / slider_each_value);
        int int_part = (int) Mathf.Floor(value / slider_each_value);
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
      var slider_back_color = this.slider_color_list[index];
      var slider_front_color = this.slider_color_list[index + 1];
      this.slider_back_image.color = slider_back_color;
      this.slider_front_image.color = slider_front_color;
    }

    public Tween SlideTo(float to_value, Action<float, Tween> callback = null, float? max_value = null,
      int? slider_count = null)
    {
      this.to_value = to_value;
      if (max_value.HasValue)
        this.max_value = max_value.Value;
      if (slider_count.HasValue)
        this.slider_count = slider_count.Value;
      if (this.sliderCat == null)
        return null;
      var slider_info = this.__GetSliderInfoByValue(to_value);
      return this.AddDOTween("UIBlood", this.sliderCat.SlideTo(slider_info.index, slider_info.pct,
        (index, pct, next_tween) =>
        {
          this.__SetSliderColor(this.sliderCat.cur_index);
          if (next_tween != null)
            this.AddDOTween("UIBlood", next_tween);
          if (callback != null)
          {
            var current_value = this.sliderCat.GetCurrentValue() * (this.max_value / this.slider_count);
            callback(current_value, next_tween);
          }
        }));
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      graphicComponent.SetIsShow(false);
      Client.instance.uiManager.uiBloodManager.DespawnUIBloodGameObject(graphicComponent.gameObject);
    }
  }
}