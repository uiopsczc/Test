using System.Collections.Generic;

namespace CsCat
{
	public partial class CommonViewComponent
	{
		protected override void _Destroy()
		{
			this._Destroy_GameObject();
			this._Destroy_Prefab();
			base._Destroy();
		}
	}
}