using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		public virtual void Despawn()
		{
			_Despawn_();
			_Despawn_Child();
			_Despawn_Destroy();
			_Despawn_Enable();
			_Despawn_Pause();
			_Despawn_Reset();
			_Despawn_Update();
			_Despawn_Refresh();
		}
	}
}