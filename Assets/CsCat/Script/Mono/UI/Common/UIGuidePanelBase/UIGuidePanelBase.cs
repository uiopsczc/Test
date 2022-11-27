using UnityEngine;

namespace CsCat
{
	public partial class UIGuidePanelBase : UIPopUpPanel
	{

		private GameObject _Nego_Bg;
		private GameObject _Nego_DialogRight;
		private GameObject _Nego_DialogLeft;
		private GameObject _Nego_Finger;
		private GameObject _Nego_Arrow;
		private GameObject _Nego_Desc;

		public UIGuidePanelBase.BgItem bgItem;

		public override bool isHideBlackMaskBehind => true;

		protected override void _Init()
		{
			base._Init();
			SetPrefabPath("Assets/PatchResources/UI/UIGuidePanelBase/Prefab/UIGuidePanelBase.prefab");
		}

		protected override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			_Nego_Bg = this._frameTransform.Find("Nego_Bg").gameObject;
			_Nego_DialogRight = this._frameTransform.Find("Nego_DialogRight").gameObject;
			_Nego_DialogLeft = this._frameTransform.Find("Nego_DialogLeft").gameObject;
			_Nego_Finger = this._frameTransform.Find("Nego_Finger").gameObject;
			_Nego_Arrow = this._frameTransform.Find("Nego_Arrow").gameObject;
			_Nego_Desc = this._frameTransform.Find("Nego_Desc").gameObject;

			bgItem = this.AddChild<UIGuidePanelBase.BgItem>(null, _Nego_Bg);
		}

		public UIGuidePanelBase.DialogItem CreateDialogLeftItem()
		{
			GameObject clone = GameObject.Instantiate(_Nego_DialogLeft, this.GetTransform());
			clone.SetActive(true);
			return this.AddChild<UIGuidePanelBase.DialogItem>(null, clone);
		}

		public UIGuidePanelBase.DialogItem CreateDialogRightItem()
		{
			GameObject clone = Object.Instantiate(_Nego_DialogRight, this.GetTransform());
			clone.SetActive(true);
			return this.AddChild<UIGuidePanelBase.DialogItem>(null, clone);
		}

		public UIGuidePanelBase.FingerItem CreateFingerItem()
		{
			GameObject clone = Object.Instantiate(_Nego_Finger, this.GetTransform());
			clone.SetActive(true);
			return this.AddChild<UIGuidePanelBase.FingerItem>(null, clone);
		}

		public UIGuidePanelBase.ArrowItem CreateArrowItem()
		{
			GameObject clone = Object.Instantiate(_Nego_Arrow, this.GetTransform());
			clone.SetActive(true);
			return this.AddChild<UIGuidePanelBase.ArrowItem>(null, clone);
		}

		public UIGuidePanelBase.DescItem CreateDescItem()
		{
			GameObject clone = Object.Instantiate(_Nego_Desc, this.GetTransform());
			clone.SetActive(true);
			return this.AddChild<UIGuidePanelBase.DescItem>(null, clone);
		}

	}
}