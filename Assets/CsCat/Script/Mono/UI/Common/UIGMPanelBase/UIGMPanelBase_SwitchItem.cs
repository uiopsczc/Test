using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public partial class UIGMPanelBase
  {
    public class SwitchItem : UIObject
    {
      private Text desc_text;
      private Button no_btn;
      private Button yes_btn;

      private string desc;
      private Action yes_callback;
      private Action no_callback;

      public void Init(GameObject gameObject, string desc, Action yes_callback, Action no_callback = null)
      {
        base.Init();
        this.desc = desc;
        this.yes_callback = yes_callback;
        this.no_callback = no_callback;
        graphicComponent.SetGameObject(gameObject, true);
      }

      public override void InitGameObjectChildren()
      {
        base.InitGameObjectChildren();
        desc_text = graphicComponent.transform.FindComponentInChildren<Text>("desc");
        no_btn = graphicComponent.transform.FindComponentInChildren<Button>("no_btn");
        yes_btn = graphicComponent.transform.FindComponentInChildren<Button>("yes_btn");

        this.desc_text.text = this.desc;
        
      }

      protected override void AddUntiyEvnts()
      {
        base.AddUntiyEvnts();
        this.RegisterOnClick(yes_btn, () => { this.yes_callback(); });
        if (this.no_callback != null)
          this.RegisterOnClick(no_btn, () => { this.no_callback(); });
      }

    }
  }
}