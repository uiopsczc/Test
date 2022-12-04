using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIPanel : UIObject
	{
		/// <summary>
		/// 是否是常驻的,即不被销毁
		/// </summary>
		public virtual bool isResident => false;
		public virtual EUILayerName layerName => EUILayerName.PopUpUILayer;
		public UILayer uiLayer => Client.instance.uiManager.uiLayerManager.uiLayerDict[layerName];
		protected Transform _frameTransform => this._cache.GetOrAddDefault("frameTransform", () => this.GetTransform().Find("Nego_Frame"));
		protected override Transform _contentTransform
		{
			get { return this._cache.GetOrAddDefault("contentTransform", () => _frameTransform.Find("Nego_Content")); }
		}

		public int sortingOrder
		{
			get => this.GetChild<CanvasProxyTreeNode>().GetSortingOrder();
			set
			{
				this.GetChild<CanvasProxyTreeNode>().SetSortingOrder(value);
				_OnSortingOrderChange();
			}
		}

		public bool isOverrideSorting
		{
			get => this.GetChild<CanvasProxyTreeNode>().IsOverrideSorting();
			set => this.GetChild<CanvasProxyTreeNode>().SetIsOverrideSorting(value);
		}

		public string sortingLayerName
		{
			get => this.GetChild<CanvasProxyTreeNode>().GetSortingLayerName();
			set => this.GetChild<CanvasProxyTreeNode>().SetSortingLayerName(value);
		}

		public virtual bool isHideBlackMaskBehind => false;

		protected override void _Init()
		{
			base._Init();
			this.AddChild<CanvasProxyTreeNode>(null);
		}

		protected override void _PostInit()
		{
			this.SetParentTransform(this.uiLayer.GetTransform());
			uiLayer.AddPanel(this);
		}


		protected virtual void _OnSortingOrderChange()
		{
		}

		protected override void _SetGameObject(GameObject gameObject, bool? isNotDestroyGameObject = false)
		{
			base._SetGameObject(gameObject, isNotDestroyGameObject);
			if (gameObject != null)
			{
				var canvas = gameObject.GetOrAddComponent<Canvas>();
				gameObject.GetOrAddComponent<GraphicRaycaster>();
				this.isOverrideSorting = true;
				this.sortingLayerName = "UI";
				this.GetChild<CanvasProxyTreeNode>().ApplyToCanvas(canvas);
				_OnSortingOrderChange();
			}
		}

		public void SetToTop()
		{
			this.uiLayer.SetPanelToTop(this);
		}

		public void SetToBottom()
		{
			this.uiLayer.SetPanelToBottom(this);
		}

		public void SetPanelIndex(int newIndex)
		{
			this.uiLayer.SetPanelIndex(this, newIndex);
		}

		protected override void _PostSetGameObject()
		{
			base._PostSetGameObject();
			this._CheckPopupAnimation();
		}

		public virtual void Close()
		{
			if (_isHiding)
				return;
			if (!this.IsShow())
			{
				Client.instance.uiManager.CloseChildPanel(this.GetKey());
				return;
			}
			if (!this._IsHasPopup())
			{
				var onPopupHideFinishAction = this._onPopupHideFinishAction;
				Client.instance.uiManager.CloseChildPanel(this.GetKey());
				onPopupHideFinishAction?.Invoke();
				return;
			}
			this._PlayPopupAnimation(AnimationNameConst.Quit);
		}

		protected override void _Reset()
		{
			base._Reset();
			this.uiLayer.RemovePanel(this);
			_Reset_Popup();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			this.uiLayer.RemovePanel(this);
			_Destroy_Popup();
		}
	}

}


