using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//canvas有被设置，用canvas的sortingOrder，否则用gameObject的parent中的Canvas
	public class UISortingOrderRelative : MonoBehaviour
	{
		public Canvas canvas;
		private readonly List<Renderer> childRendererList = new List<Renderer>();
		public int lastSortingOrder = -1;

		public int relativeSortingOrder = 0;

		public void Start()
		{
			if (canvas == null)
			{
				canvas = gameObject.GetComponentInParent<Canvas>();
				if (canvas == null)
					return;
			}


			gameObject.GetComponentsInChildren(true, childRendererList);

			var sortingOrder = canvas.sortingOrder + relativeSortingOrder;
			UpdateChildrenSortingOrder(sortingOrder);
		}


		private void UpdateChildrenSortingOrder(int sorting_order)
		{
			for (var i = 0; i < childRendererList.Count; ++i)
				if (childRendererList[i] != null)
					childRendererList[i].sortingOrder = sorting_order;
			lastSortingOrder = sorting_order;
		}


		private void Update()
		{
			if (canvas == null)
				return;

			var sortingOrder = canvas.sortingOrder + relativeSortingOrder;
			if (lastSortingOrder == sortingOrder)
				return;

			UpdateChildrenSortingOrder(sortingOrder);
		}
	}
}