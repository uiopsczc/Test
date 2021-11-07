using DG.Tweening;
using UnityEngine.UI;

namespace CsCat
{
  public class UINotifyPanel : UIPanel
  {
    public override EUILayerName layerName
    {
      get { return EUILayerName.NotifyUILayer; }
    }


    private Text desc_text;
    private Image bg_image;


    private string desc;
    private bool is_moving_up;
    private bool is_rised;
    private float position;
    private bool is_created;

    private float move_up_delay_duration = 1f;
    private float close_delay_duration = 2.8f;

    public void Init(string desc)
    {
      base.Init();
      this.desc = desc;
      graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UINotifyPanel.prefab");
    }

    public override void InitGameObjectChildren()
    {
      base.InitGameObjectChildren();
      desc_text = graphicComponent.transform.FindComponentInChildren<Text>("desc");
      bg_image = graphicComponent.transform.FindComponentInChildren<Image>("bg");

      this.AddTimer(MoveUp, this.move_up_delay_duration);
      this.AddTimer((args) =>
      {
        this.Close();
        return false;
      }, this.close_delay_duration);
      this.is_created = true;
      if (this.is_rised)
        Rise();
    }

    public override void Refresh()
    {
      base.Refresh();
      desc_text.text = desc;
    }

    public bool MoveUp(params object[] args)
    {
      graphicComponent.transform.DOBlendableMoveYBy(5, 1);
      desc_text.DOFade(0, 1);
      bg_image.DOFade(0, 1);
      is_moving_up = true;
      return false;
    }

    public void Rise()
    {
      if (!is_created)
      {
        is_rised = true;
        this.position = this.position + 0.5f;
        return;
      }

      if (this.is_moving_up)
        return;

      graphicComponent.transform.DOBlendableMoveYBy(this.position + 0.5f, 0.2f);
      this.position = 0;

    }

    protected override void _Destroy()
    {
      base._Destroy();
      is_moving_up = false;
      is_rised = false;
      is_created = false;
    }
  }
}