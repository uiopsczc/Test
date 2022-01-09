using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGMPanelBase
	{
		public class SwitchItem : UIObject
		{
			private Text descText;
			private Button noBtn;
			private Button yesBtn;

			private string desc;
			private Action yesCallback;
			private Action noCallback;

			public void Init(GameObject gameObject, string desc, Action yesCallback, Action noCallback = null)
			{
				base.Init();
				this.desc = desc;
				this.yesCallback = yesCallback;
				this.noCallback = noCallback;
				graphicComponent.SetGameObject(gameObject, true);
			}

			public override void InitGameObjectChildren()
			{
				base.InitGameObjectChildren();
				descText = graphicComponent.transform.FindComponentInChildren<Text>("desc");
				noBtn = graphicComponent.transform.FindComponentInChildren<Button>("no_btn");
				yesBtn = graphicComponent.transform.FindComponentInChildren<Button>("yes_btn");

				this.descText.text = this.desc;

			}

			protected override void AddUnityEvents()
			{
				base.AddUnityEvents();
				this.RegisterOnClick(yesBtn, () => { this.yesCallback(); });
				if (this.noCallback != null)
					this.RegisterOnClick(noBtn, () => { this.noCallback(); });
			}

		}
	}
}