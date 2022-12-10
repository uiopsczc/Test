using System.Collections.Generic;

namespace CsCat
{
	public partial class CommonViewComponent
	{
		protected override void _Reset()
		{
			this._Reset_GameObject();
			this._Reset_Prefab();
			base._Reset();
		}
	}
}