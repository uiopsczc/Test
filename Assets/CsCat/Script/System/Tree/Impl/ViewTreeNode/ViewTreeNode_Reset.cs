using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class ViewTreeNode
	{
		protected override void _Reset()
		{
			this._Reset_();
			this._Reset_Transform();
			base._Reset();
		}
	}
}