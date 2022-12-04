using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIHUDTextBase : UIObject
	{

		float _textAlpha;
		public Text _TxtC_This;
		public Animation _Animation_This;

		protected void _Init(Transform parentTransform)
		{
			base._Init();
			this.SetParentTransform(parentTransform);
			this.SetPrefabPath("Assets/PatchResources/UI/UIHUD/Prefab/UIHUDText.prefab");
		}

		protected override void _InitGameObjectChildren()
		{
			base._InitGameObjectChildren();
			this._TxtC_This = this.GetGameObject().GetComponent<Text>();
			this._textAlpha = _TxtC_This.color.a;
			this._Animation_This = this.GetGameObject().GetComponent<Animation>();
		}

		protected override void _Reset()
		{
			base._Reset();
			this._TxtC_This.SetColorA(this._textAlpha);
		}
	}
}