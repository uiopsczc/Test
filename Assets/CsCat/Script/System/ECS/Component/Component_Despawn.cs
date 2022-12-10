using System;

namespace CsCat
{
	public partial class Component
	{
		public void Despawn()
		{
			_Despawn_();
			_Despawn_Destroy();
		}
	}
}