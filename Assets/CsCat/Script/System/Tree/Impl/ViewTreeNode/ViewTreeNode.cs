using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class ViewTreeNode : TreeNode
	{
		public bool SetIsShow(bool isShow)
		{
			var isChange = _transformInfoProxy.SetIsShow(isShow);
			if (isChange)
			{
				if (isShow)
					_OnShow();
				else
					_OnHide();
			}
			return isChange;
		}


		public bool IsShow()
		{
			return this._transformInfoProxy.IsShow();
		}

		protected virtual void _OnShow()
		{

		}

		protected virtual void _OnHide()
		{

		}

		private void _Reset_()
		{
			this.SetIsShow(false);
			
		}

		private void _Destroy_()
		{
			this.SetIsShow(false);
		}
	}
}