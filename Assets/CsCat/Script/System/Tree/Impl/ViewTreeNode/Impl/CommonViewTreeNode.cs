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

		protected override bool _Refresh(bool isInit = false)
		{
			if (!base._Refresh(isInit))
				return false;
			if (!this._IsGameObjectInited())
				return false;
			return true;
		}



		protected override void _Reset()
		{
			base._Reset();
			this.DestroyGameObject();
			this._Reset_GameObject();
			this._Reset_Prefab();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			this.DestroyGameObject();
			this._Destroy_GameObject();
			this._Destroy_Prefab();
		}
	}
}