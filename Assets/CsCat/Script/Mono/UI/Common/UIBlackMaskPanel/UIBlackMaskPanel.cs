using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace CsCat
{
	public class UIBlackMaskPanel : UIPanel
	{
		private Image bg_image;
		private Action close_action;

		public override bool is_resident
		{
			get { return true; }
		}

		public override EUILayerName layerName
		{
			get { return EUILayerName.BlackMaskUILayer; }
		}


		public void Init(GameObject gameObject)
		{
			base.Init();
			graphicComponent.SetGameObject(gameObject, true);
			graphicComponent.SetIsShow(false);
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			bg_image = this.frame_transform.Find("bg").GetComponent<Image>();




		}

		protected override void AddUntiyEvnts()
		{
			base.AddUntiyEvnts();
			this.RegisterOnClick(bg_image,
			  () => { close_action?.Invoke(); });
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
				close_action = uiPanel.Close;
			else
				close_action = () => { ((LuaTable)target_panel).InvokeAction("Close"); };
		}


		void HideUIBlackMask()
		{
			graphicComponent.SetIsShow(false);
			SetIsRaycastTarget(true);
			close_action = null;
		}


		public void SetIsRaycastTarget(bool is_raycastTarget)
		{
			this.bg_image.raycastTarget = is_raycastTarget;
		}
	}
}