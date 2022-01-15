using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace CsCat
{
	public class ResLoadComponent : GameComponent
	{
		public ResLoad resLoad;
		public void Init(ResLoad resLoad)
		{
			base.Init();
			this.resLoad = resLoad;
		}

		public bool IsAllLoadDone()
		{
			return this.resLoad.IsAllLoadDone();
		}

		public IEnumerator IEIsAllLoadDone(Action onAllLoadDoneCallback = null)
		{
			yield return this.resLoad.IEIsAllLoadDone(onAllLoadDoneCallback);
		}

		public void CheckIsAllLoadDone(Action onAllLoadDoneCallback = null)
		{
			this.StartCoroutine(IEIsAllLoadDone(onAllLoadDoneCallback));
		}



		// 加载某个资源
		public AssetCat GetOrLoadAsset(string assetPath, Action<AssetCat> onLoadSuccessCallback = null, Action<AssetCat> onLoadFailCallback = null, Action<AssetCat> onLoadDoneCallback = null, object callbackCause = null)
		{
			return this.resLoad.GetOrLoadAsset(assetPath, onLoadSuccessCallback, onLoadFailCallback, onLoadDoneCallback, callbackCause);
		}


		public void CancelLoadCallback(AssetCat assetCat, object callbackCause = null)
		{
			this.resLoad.CancelLoadCallback(assetCat, callbackCause);
		}

		public void CancelLoadAllCallbacks(AssetCat assetCat)
		{
			this.resLoad.CancelLoadAllCallbacks(assetCat);
		}

		protected override void _Reset()
		{
			base._Reset();
			this.resLoad.Reset();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			this.resLoad.Destroy();
		}


	}
}