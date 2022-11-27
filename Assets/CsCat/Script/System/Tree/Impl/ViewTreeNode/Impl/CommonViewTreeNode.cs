using System.Collections.Generic;

namespace CsCat
{
	public partial class CommonViewTreeNode : ViewTreeNode
	{
		protected override void _Init()
		{
			base._Init();
			this.AddChild<CoroutineDictTreeNode>(null, new CoroutineDict(Main.instance));
		}

		protected override void _PostInit()
		{
			base._PostInit();
			this._LoadPrefabPath();
			this.SetIsShow(true);
		}



		protected override void _Reset()
		{
			this.DestroyGameObject();
			this._Reset_GameObject();
			this._Reset_Prefab();
			base._Reset();
			
		}

		protected override void _Destroy()
		{
			this.DestroyGameObject();
			this._Destroy_GameObject();
			this._Destroy_Prefab();
			base._Destroy();
			
		}
	}
}