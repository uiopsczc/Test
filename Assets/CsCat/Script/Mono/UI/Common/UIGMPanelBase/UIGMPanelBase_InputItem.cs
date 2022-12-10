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
				DoSetGameObject(gameObject);
			}

			protected override void _InitGameObjectChildren()
			{
				base._InitGameObjectChildren();
				_TxtC_Desc = this.GetTransform().Find("TxtC_Desc").GetComponent<Text>();
				_InputField1 = this.GetTransform().Find("InputField1").GetComponent<InputField>();
				_BtnYes = this.GetTransform().Find("BtnYes").GetComponent<Button>();
			}

			protected override void _AddUnityListeners()
			{
				base._AddUnityListeners();
				this.RegisterOnClick(_BtnYes, () => { _yesCallback(_InputField1); });
			}

			protected override void _PostSetGameObject()
			{
				base._PostSetGameObject();
				this._TxtC_Desc.text = _desc;
			}

			protected override void _DestroyGameObject()
			{
			}
		}
	}
}