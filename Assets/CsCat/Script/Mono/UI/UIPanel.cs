using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIPanel : UIObject
	{
		private int _sortingOrder = int.MinValue;

		/// <summary>
		/// 是否是常驻的,即不被销毁
		/// </summary>
		public virtual bool isResident => false;
		public virtual EUILayerName layerName => EUILayerName.PopUpUILayer;
		public UILayer uiLayer => Client.instance.uiManager.uiLayerManager.uiLayerDict[layerName];
		protected Transform frameTransform => this.cache.GetOrAddDefault("frameTransform", () => this.graphicComponent.gameObject.transform.Find("frame"));
		protected Transform contentTransform => this.cache.GetOrAddDefault("contentTransform", () => frameTransform.Find("content"));
		protected Canvas canvas => this.cache.GetOrAddDefault("canvas", () => graphicComponent.gameObject.GetOrAddComponent<Canvas>());
		public int sortingOrder
		{
			get => this._sortingOrder;
			set
			{
				if (_sortingOrder == value)
					return;
				_sortingOrder = value;
				OnSortingOrderChange();
			}
		}

		public virtual bool isHideBlackMaskBehind => false;

		protected virtual void OnSortingOrderChange()
		{
			if (graphicComponent.gameObject == null)
				return;
			canvas.sortingOrder = sortingOrder;
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


		public override void OnAllAssetsLoadDone()
		{
			//    LogCat.LogWarning(prefabPath);
			base.OnAllAssetsLoadDone();
			canvas.overrideSorting = true;
			canvas.sortingLayerName = "UI";
			graphicComponent.gameObject.GetOrAddComponent<GraphicRaycaster>();
			OnSortingOrderChange();
		}

		public void OnInitPanel(Transform parentTransform)
		{
			graphicComponent.SetParentTransform(parentTransform == null ? uiLayer.graphicComponent.transform : parentTransform);
			this.uiLayer.AddPanel(this);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			_sortingOrder = int.MinValue;
		}


		public virtual void Close()
		{
			this.uiLayer.RemovePanel(this);
			this.parentUIObject.CloseChildPanel(this.key);
		}

	}

}


