using System;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
  public class UIHUDNumber : UIHUDTextBase
  {
    public Vector2 spawn_uiPosition;
    private bool is_showing;
    private bool is_fading;
    private const float default_show_duration = 1;
    public float duration;
    public float duration_half = default_show_duration * 0.5f;
    public float pos_diff_x;
    public float pos_diff_y;
    float __pos_diff_x;
    float __pos_diff_y;
    private RandomManager _randomManager;

    public override void Init()
    {
      base.Init();
      this.AddListener<float, float>(GlobalEventNameConst.Update, Update);
    }

    private RandomManager randomManager => _randomManager ?? Client.instance.randomManager;

    public void SetRandomManager(RandomManager randomManager)
    {
      this._randomManager = randomManager;
    }


    public void Show(Transform tf, string show_string, Color color)
    {
      Show(() => tf == null ? null : (Vector3?) tf.position, show_string, color);
    }

    public void Show(Vector3 position, string show_string, Color color)
    {
      Show(() => position, show_string, color);
    }

    public void Show(Func<Vector3?> spawn_worldPosition_func, string show_string, Color color)
    {
      InvokeAfterAllAssetsLoadDone(() =>
      {
        duration = default_show_duration;
        Vector3? spawn_worldPosition = spawn_worldPosition_func();
        if (spawn_worldPosition == null)
        {
          Reset();
          return;
        }

        this.spawn_uiPosition = CameraUtil.WorldToUIPos(null,
          Client.instance.combat.cameraManager.main_cameraBase.camera, spawn_worldPosition.Value);
        this.text_comp.text = show_string;
        this.text_comp.color = color;


        this.pos_diff_x = randomManager.RandomBoolean() ? randomManager.RandomFloat(30, 100f) : randomManager.RandomFloat(-100, -30);
        this.pos_diff_y = randomManager.RandomFloat(50, 100f);

        this.is_showing = true;
        graphicComponent.SetIsShow(true);
        UpdatePos(0);
      });
    }

    protected void Update(float deltaTime, float unscaledDeltaTime)
    {
      if (!this.IsCanUpdate())
        return;
      this.UpdatePos(deltaTime);
    }

    public void UpdatePos(float deltaTime)
    {
      if (!this.is_showing)
        return;
      duration = duration - deltaTime;
      if (duration <= 0)
      {
        this.Reset();
        return;
      }


      if (duration > duration_half)
        __pos_diff_y = EaseCat.Cubic.EaseOut2(0, this.pos_diff_y, (default_show_duration - duration) / duration_half);
      else
        __pos_diff_y = EaseCat.Cubic.EaseIn2(this.pos_diff_y, 0, (duration_half - duration) / duration_half);

      float pct = (default_show_duration - duration) / default_show_duration;
      __pos_diff_x = EaseCat.Linear.EaseNone2(0, this.pos_diff_x, pct);

      if (!is_fading && pct >= 0.8f)
      {
        is_fading = true;
        this.text_comp.DOFade(0.2f, duration);
      }

      Vector2 pos = spawn_uiPosition + new Vector2(__pos_diff_x, __pos_diff_y);
      graphicComponent.rectTransform.anchoredPosition = pos;
    }

    protected override void __Reset()
    {
      base.__Reset();
      duration = default_show_duration;
      this.is_showing = false;
      this.is_fading = false;
      graphicComponent.SetIsShow(false);
    }
  }
}