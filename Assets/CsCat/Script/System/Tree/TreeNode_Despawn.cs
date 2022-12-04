using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		public virtual void Despawn()
		{
			_OnDespawn_();
			_OnDespawn_Child();
			_OnDespawn_Destroy();
			_OnDespawn_Enable();
			_OnDespawn_Pause();
			_OnDespawn_Reset();
			_OnDespawn_Update();
			_OnDespawn_Refresh();
		}
	}
}