using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace CsCat
{
	public class UIBlackMaskPanel : UIPanel
	{
		private Image _bgImage;
		private Action _closeAction;

		public override bool isResident => true;

		public override EUILayerName layerName => EUILayerName.BlackMaskUILayer;


		protected void _Init(GameObject gameObject)
		{
			this.SetGameObject(gameObject, true);
		}

		protected override void _PostInit()
		{
			base._PostInit();
			this.SetIsShow(false);
		}

		protected override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			_bgImage = this._frameTransform.Find("Nego_Bg").GetComponent<Image>();
		}

		protected override void AddUnityListeners()
		{
			base.AddUnityListeners();
			this.RegisterOnClick(_bgImage,
			  () => { _closeAction?.Invoke(); });
		}

		protected override void AddGameListeners()
		{
			base.AddGameListeners();
			this.AddListener<int, object>(null, UIEventNameConst.ShowUIBlackMask, ShowUIBlackMask);
			this.AddListener(null, UIEventNameConst.HideUIBlackMask, HideUIBlackMask);
		}

		void ShowUIBlackMask(int targetPanelSortingOrder, object targetPanel)
		{
			this.SetIsShow(true);
			this.sortingOrder = targetPanelSortingOrder + UIBlackMaskPanelConst.Offset;


			if (targetPanel is UIPanel uiPanel)
				_closeAction = uiPanel.Close;
			else
				_closeAction = () => { ((LuaTable)targetPanel).InvokeAction("Close"); };
		}


		void HideUIBlackMask()
		{
			SetIsShow(false);
			SetIsRaycastTarget(true);
			_closeAction = null;
		}


		public void SetIsRaycastTarget(bool isRaycastTarget)
		{
			this._bgImage.raycastTarget = isRaycastTarget;
		}

		protected override void _Reset()
		{
			base._Reset();
			SetIsShow(false);
			_bgImage = null;
			_closeAction = null;
		}

		protected override void _Destroy()
		{
			base._Destroy();
			SetIsShow(false);
			_bgImage = null;
			_closeAction = null;
		}
	}
}