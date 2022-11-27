using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGMPanelBase
	{
		public class InputItem : UIObject
		{
			private Text _TxtC_Desc;
			private InputField _InputField1;
			private Button _BtnYes;

			private string _desc;
			private Action<InputField> _yesCallback;

			public void _Init(GameObject gameObject, string desc, Action<InputField> yesCallback)
			{
				base._Init();
				this._desc = desc;
				this._yesCallback = yesCallback;
				SetGameObject(gameObject, true);
			}

			protected override void InitGameObjectChildren()
			{
				base.InitGameObjectChildren();
				_TxtC_Desc = this.GetTransform().Find("TxtC_Desc").GetComponent<Text>();
				_InputField1 = this.GetTransform().Find("InputField1").GetComponent<InputField>();
				_BtnYes = this.GetTransform().Find("BtnYes").GetComponent<Button>();

				this._TxtC_Desc.text = _desc;
			}
			protected override void AddUnityListeners()
			{
				base.AddUnityListeners();
				this.RegisterOnClick(_BtnYes, () => { _yesCallback(_InputField1); });
			}


		}
	}
}