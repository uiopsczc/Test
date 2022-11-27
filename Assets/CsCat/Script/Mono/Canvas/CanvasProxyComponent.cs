using UnityEngine;

namespace CsCat
{
	public class CanvasProxyTreeNode:TreeNode
	{
		private readonly CanvasProxy _canvasProxy = new CanvasProxy();

		public void ApplyToCanvas(Canvas canvas)
		{
			_canvasProxy.ApplyToCanvas(canvas);
		}

		public void SetSortingOrder(int sortingOrder)
		{
			_canvasProxy.SetSortingOrder(sortingOrder);
		}

		public int GetSortingOrder()
		{
			return _canvasProxy.GetSortingOrder();
		}

		public void SetIsOverrideSorting(bool isOverrideSorting)
		{
			_canvasProxy.SetIsOverrideSorting(isOverrideSorting);
		}

		public bool IsOverrideSorting()
		{
			return _canvasProxy.IsOverrideSorting();
		}

		public void SetSortingLayerName(string sortingLayerName)
		{
			_canvasProxy.SetSortingLayerName(sortingLayerName);
		}

		public string GetSortingLayerName()
		{
			return _canvasProxy.GetSortingLayerName(); ;
		}
		

		protected override void _Reset()
		{
			this._Reset();
			this._canvasProxy.Reset();
		}

		protected override void _Destroy()
		{
			this._Destroy();
			this._canvasProxy.Reset();
		}
	}
}