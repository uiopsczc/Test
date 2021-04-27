using UnityEngine;

namespace CsCat
{
  public partial class UIGuidePanelBase : UIPopUpPanel
  {

    private GameObject bg_prefab;
    private GameObject dialog_right_prefab;
    private GameObject dialog_left_prefab;
    private GameObject finger_prefab;
    private GameObject arrow_prefab;
    private GameObject desc_prefab;

    public UIGuidePanelBase.BgItem bgItem;

    public override bool is_hide_blackMaskBehide { get=>true; }

    public override void Init()
    {
      base.Init();
      graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIGuidePanelBase.prefab");
    }

    public override void InitGameObjectChildren()
    {
      base.InitGameObjectChildren();
      bg_prefab = this.frame_transform.Find("bg").gameObject;
      dialog_right_prefab = this.frame_transform.Find("dialog_right").gameObject;
      dialog_left_prefab = this.frame_transform.Find("dialog_left").gameObject;
      finger_prefab = this.frame_transform.Find("finger").gameObject;
      arrow_prefab = this.frame_transform.Find("arrow").gameObject;
      desc_prefab = this.frame_transform.Find("desc").gameObject;

      bgItem = this.AddChild<UIGuidePanelBase.BgItem>(null, bg_prefab);
    }

    public UIGuidePanelBase.DialogItem CreateDialogLeftItem()
    {
      GameObject clone = GameObject.Instantiate(dialog_left_prefab, graphicComponent.transform);
      clone.SetActive(true);
      return this.AddChild<UIGuidePanelBase.DialogItem>(null, clone);
    }

    public UIGuidePanelBase.DialogItem CreateDialogRightItem()
    {
      GameObject clone = GameObject.Instantiate(dialog_right_prefab, graphicComponent.transform);
      clone.SetActive(true);
      return this.AddChild<UIGuidePanelBase.DialogItem>(null, clone);
    }

    public UIGuidePanelBase.FingerItem CreateFingerItem()
    {
      GameObject clone = GameObject.Instantiate(finger_prefab, graphicComponent.transform);
      clone.SetActive(true);
      return this.AddChild<UIGuidePanelBase.FingerItem>(null, clone);
    }

    public UIGuidePanelBase.ArrowItem CreateArrowItem()
    {
      GameObject clone = GameObject.Instantiate(arrow_prefab, graphicComponent.transform);
      clone.SetActive(true);
      return this.AddChild<UIGuidePanelBase.ArrowItem>(null, clone);
    }

    public UIGuidePanelBase.DescItem CreateDescItem()
    {
      GameObject clone = GameObject.Instantiate(desc_prefab, graphicComponent.transform);
      clone.SetActive(true);
      return this.AddChild<UIGuidePanelBase.DescItem>(null, clone);
    }

  }
}