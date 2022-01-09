using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGMPanelBase
	{
		public class InputItem2 : UIObject
		{
			private Text descText;
			private InputField inputField1;
			private InputField inputField2;
			private Button yesBtn;

			private string desc;
			private Action<InputField, InputField> yesCallback;


			public void Init(GameObject gameObject, string desc, Action<InputField, InputField> yesCallback)
			{
				base.Init();
				this.desc = desc;
				this.yesCallback = yesCallback;
				graphicComponent.SetGameObject(gameObject, true);
			}

			public override void InitGameObjectChildren()
			{
				base.InitGameObjectChildren();
				descText = graphicComponent.transform.FindComponentInChildren<Text>("desc");
				inputField1 = graphicComponent.transform.FindComponentInChildren<InputField>("InputField1");
				inputField2 = graphicComponent.transform.FindComponentInChildren<InputField>("InputField2");
				yesBtn = graphicComponent.transform.FindComponentInChildren<Button>("yes_btn");

				this.descText.text = this.desc;

			}

			protected override void AddUnityEvents()
			{
				base.AddUnityEvents();
				this.RegisterOnClick(yesBtn, () => { yesCallback(inputField1, inputField2); });
			}
		}
	}
}