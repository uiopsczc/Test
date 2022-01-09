using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGMPanelBase
	{
		public class InputItem : UIObject
		{
			private Text descText;
			private InputField inputField;
			private Button yesBtn;

			private string desc;
			private Action<InputField> yesCallback;

			public void Init(GameObject gameObject, string desc, Action<InputField> yesCallback)
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
				inputField = graphicComponent.transform.FindComponentInChildren<InputField>("InputField");
				yesBtn = graphicComponent.transform.FindComponentInChildren<Button>("yes_btn");

				this.descText.text = desc;
			}
			protected override void AddUnityEvents()
			{
				base.AddUnityEvents();
				this.RegisterOnClick(yesBtn, () => { yesCallback(inputField); });
			}


		}
	}
}