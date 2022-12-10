using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		public virtual void Despawn()
		{
			_Despawn_Component();
			_Despawn_();
			_Despawn_Destroy();
		}
	}
}