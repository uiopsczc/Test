using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace CsCat
{
	public class ResLoadTreeNode : TreeNode
	{ 
		private ResLoad _resLoad;
		protected void _Init(ResLoad resLoad)
		{
			base._Init();
			this._resLoad = resLoad;
		}

		public ResLoad GetResLoad()
		{
			return this._resLoad;
		}

		public bool IsAllLoadDone()
		{
			return this._resLoad.IsAllLoadDone();
		}

		public IEnumerator IEIsAllLoadDone(Action onAllLoadDoneCallback = null)
		{
			return this._resLoad.IEIsAllLoadDone(onAllLoadDoneCallback);
		}

		public bool IsLoadDone(string assetPath)
		{
			return this._resLoad.IsLoadDone(assetPath);
		}

		public IEnumerator IEIsLoadDone(string assetPath, Action onLoadDoneCallback = null)
		{
			return this._resLoad.IEIsLoadDone(assetPath);
		}


		// 加载某个资源
		public AssetCat GetOrLoadAsset(string assetPath, Action<AssetCat> onLoadSuccessCallback = null, Action<AssetCat> onLoadFailCallback = null, Action<AssetCat> onLoadDoneCallback = null, object callbackCause = null)
		{
			return this._resLoad.GetOrLoadAsset(assetPath, onLoadSuccessCallback, onLoadFailCallback, onLoadDoneCallback, callbackCause);
		}


		public void CancelLoadCallback(AssetCat assetCat, object callbackCause = null)
		{
			this._resLoad.CancelLoadCallback(assetCat, callbackCause);
		}

		public void CancelLoadAllCallbacks(AssetCat assetCat)
		{
			this._resLoad.CancelLoadAllCallbacks(assetCat);
		}

		protected override void _Reset()
		{
			this._resLoad.Reset();
			base._Reset();
		}

		protected override void _Destroy()
		{
			this._resLoad.Destroy();
			base._Destroy();
		}


	}
}