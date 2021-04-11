using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public partial class UIGMPanelBase
  {
    public class InputItem2 : UIObject
    {
      private Text desc_text;
      private InputField inputField1;
      private InputField inputField2;
      private Button yes_btn;

      private string desc;
      private Action<InputField, InputField> yes_callback;


      public void Init(GameObject gameObject, string desc, Action<InputField, InputField> yes_callback)
      {
        base.Init();
        this.desc = desc;
        this.yes_callback = yes_callback;
        graphicComponent.SetGameObject(gameObject, true);
      }

      public override void InitGameObjectChildren()
      {
        base.InitGameObjectChildren();
        desc_text = graphicComponent.transform.FindComponentInChildren<Text>("desc");
        inputField1 = graphicComponent.transform.FindComponentInChildren<InputField>("InputField1");
        inputField2 = graphicComponent.transform.FindComponentInChildren<InputField>("InputField2");
        yes_btn = graphicComponent.transform.FindComponentInChildren<Button>("yes_btn");

        this.desc_text.text = this.desc;
        this.RegisterOnClick(yes_btn, () => { yes_callback(inputField1, inputField2); });
      }
    }
  }
}