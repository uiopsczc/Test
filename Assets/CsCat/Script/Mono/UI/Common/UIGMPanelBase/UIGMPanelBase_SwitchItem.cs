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
				DoSetGameObject(gameObject);
			}

			protected override void _InitGameObjectChildren()
			{
				base._InitGameObjectChildren();
				descText = GetTransform().Find("TxtC_Desc").GetComponent<Text>();
				_BtnNo = GetTransform().Find("BtnNo").GetComponent<Button>();
				_BtnYes = GetTransform().Find("BtnYes").GetComponent<Button>();
			}

			protected override void _AddUnityListeners()
			{
				base._AddUnityListeners();
				this.RegisterOnClick(_BtnYes, () => { this._yesCallback(); });
				if (this._noCallback != null)
					this.RegisterOnClick(_BtnNo, () => { this._noCallback(); });
			}

			protected override void _PostSetGameObject()
			{
				base._PostSetGameObject();
				this.descText.text = this._desc;
			}

			protected override void _DestroyGameObject()
			{
			}
		}
	}
}