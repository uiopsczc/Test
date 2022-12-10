using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class ViewTreeNode
	{
		protected override void _Destroy()
		{
			this._Destroy_();
			this._Destroy_Transform();
			base._Destroy();
		}
	}
}