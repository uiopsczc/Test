using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public partial class UIGuidePanelBase
  {
    public class BgItem : UIObject
    {
      public Image image;
      public Button button;

      public void Init(GameObject gameObject)
      {
        base.Init();
        graphicComponent.SetGameObject(gameObject, true);
      }

      public override void InitGameObjectChildren()
      {
        base.InitGameObjectChildren();
        image = graphicComponent.gameObject.GetComponent<Image>();
        button = graphicComponent.gameObject.GetComponent<Button>();
      }

      public void Show(bool is_clickable = true, Action<UIPanel> click_callback = null, bool is_visible = true)
      {
        if (!is_visible)
          image.SetAlpha(0.007f);
        if (is_clickable)
        {
          if (click_callback == null)
          {
            this.parent_uiPanel.RegisterOnClick(button, () => { parent_uiPanel.Close(); });
          }
          else
            this.parent_uiPanel.RegisterOnClick(button, () => { click_callback(parent_uiPanel); });
        }

      }
    }
  }
}