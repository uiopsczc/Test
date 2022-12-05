using System.Collections.Generic;

namespace CsCat
{
	public partial class CommonViewComponent:ViewComponent
	{
		protected override void _Reset()
		{
			this.DestroyGameObject();
			_isNotDestroyGameObject = false;
			_prefabPath = null;
			_prefabAssetCat = null;
			_isLoadDone = false;
			_parentTransform = null;
			base._Reset();
		}

		protected override void _Destroy()
		{
			this.DestroyGameObject();
			_isNotDestroyGameObject = false;
			_prefabPath = null;
			_prefabAssetCat = null;
			_isLoadDone = false;
			_parentTransform = null;
			base._Destroy();
		}
	}
}