using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGMPanelBase
	{
		public class InputItem : UIObject
		{
			private Text desc_text;
			private InputField inputField;
			private Button yes_btn;

			private string desc;
			private Action<InputField> yes_callback;

			public void Init(GameObject gameObject, string desc, Action<InputField> yes_callback)
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
				inputField = graphicComponent.transform.FindComponentInChildren<InputField>("InputField");
				yes_btn = graphicComponent.transform.FindComponentInChildren<Button>("yes_btn");

				this.desc_text.text = desc;
			}
			protected override void AddUntiyEvnts()
			{
				base.AddUntiyEvnts();
				this.RegisterOnClick(yes_btn, () => { yes_callback(inputField); });
			}


		}
	}
}