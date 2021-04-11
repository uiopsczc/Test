using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  // 走马灯效果
  public class UILanternNotifyPanel : UIPanel
  {
    public override bool is_resident
    {
      get { return true; }
    }

    public override EUILayerName layerName
    {
      get { return EUILayerName.NotifyLayer; }
    }

    private Text desc_text;
    private RectTransform desc_rectTransform;
    private RectTransform mask_rectTransform;

    private Sequence sequence;

    private float move_to_center_duration = 1f;
    private float stay_center_duration = 1f;
    private float move_to_end_duration = 1f;

    public void Init(GameObject gameObject)
    {
      base.Init();
      graphicComponent.SetGameObject(gameObject,true);
    }

    public override void InitGameObjectChildren()
    {
      base.InitGameObjectChildren();
      desc_text = this.frame_transform.FindComponentInChildren<Text>("mask/desc");
      desc_rectTransform = desc_text.GetComponent<RectTransform>();
      mask_rectTransform = this.frame_transform.FindComponentInChildren<RectTransform>("mask");
    }

    public void Show(string desc)
    {
      graphicComponent.SetIsShow(true);
      desc_text.text = desc;
      LayoutRebuilder.ForceRebuildLayoutImmediate(desc_rectTransform); //计算desc_rtf的长度
      desc_rectTransform.SetAnchoredPositionX(mask_rectTransform.sizeDelta.x / 2 + desc_rectTransform.sizeDelta.x / 2);
      sequence = DOTween.Sequence();
      sequence.Append(desc_rectTransform.DOMoveX(0, move_to_center_duration));
      sequence.Append(desc_rectTransform.DOWait(stay_center_duration));
      sequence.Append(desc_rectTransform.DOAnchorPosX(
        -mask_rectTransform.sizeDelta.x / 2 - desc_rectTransform.sizeDelta.x, move_to_end_duration));
      sequence.OnComplete(() => { Reset(); });
    }

    protected override void __Reset()
    {
      base.__Reset();
      graphicComponent.SetIsShow(false);
      Client.instance.uiManager.uiNotifyManager.__LanternNotify();
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      sequence?.Kill();
    }
  }
}