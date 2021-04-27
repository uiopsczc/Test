using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public class UIFadePanel : UIPanel
  {
    public override bool is_resident
    {
      get { return true; }
    }

    public override EUILayerName layerName
    {
      get { return EUILayerName.FadeUILayer; }
    }

    private Image fade_image;

    public void Init(GameObject gameObject)
    {
      base.Init();
      graphicComponent.SetGameObject(gameObject, true);
      graphicComponent.SetIsShow(false);
    }

    public override void InitGameObjectChildren()
    {
      base.InitGameObjectChildren();
      fade_image = this.frame_transform.Find("fade").GetComponent<Image>();
    }

    public void FadeInOut(float duration, Action callback)
    {
      graphicComponent.SetIsShow(true);
      Sequence sequence = DOTween.Sequence();
      fade_image.SetColorA(0);
      sequence.Append(fade_image.DOFade(1, duration * 0.25f)); //透明度从0-1
      sequence.Append(graphicComponent.transform.DOWait(0.45f)); //透明度在1的时候保持X * 0.4秒
      sequence.Append(fade_image.DOFade(1, duration * 0.3f)); //透明度从1 - 0
      sequence.OnComplete(() => { callback?.Invoke(); });
    }

    public void FadeTo(float toAplha, float duration, Action callback = null)
    {
      graphicComponent.SetIsShow(true);
      fade_image.DOFade(toAplha, duration).OnComplete(() => { callback?.Invoke(); });
    }

    public void FadeTo(float fromAplha, float toAplha, float duration, Action callback = null)
    {
      fade_image.SetColorA(fromAplha);
      FadeTo(toAplha, duration, callback);
    }

    protected override void __Reset()
    {
      base.__Reset();
      graphicComponent.SetIsShow(false);
      fade_image.SetColorA(1);
    }

    public void HideFade()
    {
      Reset();
    }
  }
}