using UnityEngine;

namespace CsCat
{
	public class CanvasProxy
	{
		private Canvas _canvas;
		private int _sortingOrder;
		private bool _isOverrideSorting;
		private string _sortingLayerName;

		public CanvasProxy()
		{
		}

		public void ApplyToCanvas(Canvas canvas)
		{
			this._canvas = canvas;
			canvas.overrideSorting = this._isOverrideSorting;
			canvas.sortingOrder = this._sortingOrder;
		}

		public void SetSortingOrder(int sortingOrder)
		{
			if (this._sortingOrder == sortingOrder)
				return;
			this._sortingOrder = sortingOrder;
			if (this._canvas)
				_canvas.sortingOrder = sortingOrder;
		}

		public int GetSortingOrder()
		{
			return this._sortingOrder;
		}

		public void SetIsOverrideSorting(bool isOverrideSorting)
		{
			if (this._isOverrideSorting == isOverrideSorting)
				return;
			this._isOverrideSorting = isOverrideSorting;
			if (this._canvas)
				_canvas.overrideSorting = isOverrideSorting;
		}

		public bool IsOverrideSorting()
		{
			return this._isOverrideSorting;
		}

		public void SetSortingLayerName(string sortingLayerName)
		{
			if (this._sortingLayerName == sortingLayerName)
				return;
			this._sortingLayerName = sortingLayerName;
			if (this._canvas)
				_canvas.sortingLayerName = sortingLayerName;
		}

		public string GetSortingLayerName()
		{
			return _sortingLayerName;
		}
		

		public void Reset()
		{
			this._canvas = null;
			_sortingOrder = 0;
			_isOverrideSorting = false;
			_sortingLayerName = null;
		}
	}
}