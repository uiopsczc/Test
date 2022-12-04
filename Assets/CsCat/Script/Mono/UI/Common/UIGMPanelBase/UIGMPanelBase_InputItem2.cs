using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIGMPanelBase
	{
		public class InputItem2 : UIObject
		{
			private Text _TxtC_Desc;
			private InputField _inputField1;
			private InputField _inputField2;
			private Button _BtnYes;

			private string _desc;
			private Action<InputField, InputField> _yesCallback;


			protected void _Init(GameObject gameObject, string desc, Action<InputField, InputField> yesCallback)
			{
				base._Init();
				this._desc = desc;
				this._yesCallback = yesCallback;
				_SetGameObject(gameObject, true);
			}

			protected override void _InitGameObjectChildren()
			{
				base._InitGameObjectChildren();
				_TxtC_Desc = this.GetTransform().Find("TxtC_Desc").GetComponent<Text>();
				_inputField1 = this.GetTransform().Find("InputField1").GetComponent<InputField>();
				_inputField2 = this.GetTransform().Find("InputField2").GetComponent<InputField>();
				_BtnYes = this.GetTransform().Find("BtnYes").GetComponent<Button>();
			}

			protected override void _AddUnityListeners()
			{
				base._AddUnityListeners();
				this.RegisterOnClick(_BtnYes, () => { _yesCallback(_inputField1, _inputField2); });
			}

			protected override void _PostSetGameObject()
			{
				base._PostSetGameObject();
				this._TxtC_Desc.text = this._desc;
			}
		}
	}
}