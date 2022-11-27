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
			private Button _BtnNo;
			private Button _BtnYes;

			private string _desc;
			private Action _yesCallback;
			private Action _noCallback;

			public void _Init(GameObject gameObject, string desc, Action yesCallback, Action noCallback = null)
			{
				base._Init();
				this._desc = desc;
				this._yesCallback = yesCallback;
				this._noCallback = noCallback;
				SetGameObject(gameObject, true);
			}

			protected override void InitGameObjectChildren()
			{
				base.InitGameObjectChildren();
				descText = GetTransform().Find("TxtC_Desc").GetComponent<Text>();
				_BtnNo = GetTransform().Find("BtnNo").GetComponent<Button>();
				_BtnYes = GetTransform().Find("BtnYes").GetComponent<Button>();
				this.descText.text = this._desc;
			}

			protected override void AddUnityListeners()
			{
				base.AddUnityListeners();
				this.RegisterOnClick(_BtnYes, () => { this._yesCallback(); });
				if (this._noCallback != null)
					this.RegisterOnClick(_BtnNo, () => { this._noCallback(); });
			}

		}
	}
}