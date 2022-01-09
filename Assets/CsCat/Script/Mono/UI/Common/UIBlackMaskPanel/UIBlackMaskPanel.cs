using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace CsCat
{
	public class UIBlackMaskPanel : UIPanel
	{
		private Image bgImage;
		private Action closeAction;

		public override bool isResident => true;

		public override EUILayerName layerName => EUILayerName.BlackMaskUILayer;


		public void Init(GameObject gameObject)
		{
			base.Init();
			graphicComponent.SetGameObject(gameObject, true);
			graphicComponent.SetIsShow(false);
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			bgImage = this.frameTransform.Find("bg").GetComponent<Image>();




		}

		protected override void AddUnityEvents()
		{
			base.AddUnityEvents();
			this.RegisterOnClick(bgImage,
			  () => { closeAction?.Invoke(); });
		}

		protected override void AddGameEvents()
		{
			base.AddGameEvents();
			this.AddListener<int, object>(null, UIEventNameConst.ShowUIBlackMask, ShowUIBlackMask);
			this.AddListener(null, UIEventNameConst.HideUIBlackMask, HideUIBlackMask);
		}

		void ShowUIBlackMask(int target_panel_sorttingOrder, object target_panel)
		{
			this.graphicComponent.SetIsShow(true);
			this.canvas.sortingOrder = target_panel_sorttingOrder + UIBlackMaskPanelConst.Offset;


			if (target_panel is UIPanel uiPanel)
				closeAction = uiPanel.Close;
			else
				closeAction = () => { ((LuaTable)target_panel).InvokeAction("Close"); };
		}


		void HideUIBlackMask()
		{
			graphicComponent.SetIsShow(false);
			SetIsRaycastTarget(true);
			closeAction = null;
		}


		public void SetIsRaycastTarget(bool isRaycastTarget)
		{
			this.bgImage.raycastTarget = isRaycastTarget;
		}
	}
}