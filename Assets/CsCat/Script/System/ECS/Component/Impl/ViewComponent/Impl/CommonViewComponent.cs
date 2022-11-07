using System.Collections.Generic;

namespace CsCat
{
	public partial class CommonViewComponent:ViewComponent
	{
		protected override void _Reset()
		{
			this.DestroyGameObject();
			base._Reset();
			_isNotDestroyGameObject = false;
			_prefabPath = null;
			_prefabAssetCat = null;
			_isLoadDone = false;
			_parentTransform = null;
		}

		protected override void _Destroy()
		{
			this.DestroyGameObject();
			base._Destroy();
			_isNotDestroyGameObject = false;
			_prefabPath = null;
			_prefabAssetCat = null;
			_isLoadDone = false;
			_parentTransform = null;
		}
	}
}