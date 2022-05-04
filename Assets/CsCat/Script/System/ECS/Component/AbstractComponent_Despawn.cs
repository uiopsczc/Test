using System;

namespace CsCat
{
	public partial class AbstractComponent
	{
		public void OnDeSpawn()
		{
			_OnDespawn_();
			_OnDespawn_Destroy();
			_OnDespawn_Enable();
			_OnDespawn_Pause();
			_OnDespawn_Reset();
			_OnDespawn_Update();
		}
	}
}