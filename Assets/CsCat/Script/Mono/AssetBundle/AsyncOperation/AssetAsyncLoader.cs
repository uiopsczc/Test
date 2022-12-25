

using System.Collections.Generic;

namespace CsCat
{
	public class AssetAsyncLoader : BaseAssetAsyncLoader
	{
		protected BaseAssetBundleAsyncLoader _assetBundleLoader;



		//通过AssetBundleLoader获取
		public void Init(AssetCat assetCat, BaseAssetBundleAsyncLoader assetBundleLoader)
		{
			this._assetCat = assetCat;
			this._assetBundleLoader = assetBundleLoader;

			AddListener<AssetBundleAsyncLoader>(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Fail,
			  _OnAssetBundleAsyncLoaderFail);
			AddListener<AssetBundleAsyncLoader>(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Success,
			  _OnAssetBundleAsyncLoaderSuccess);
		}


		protected override float _GetProgress()
		{
			return resultInfo.isDone ? 1.0f : _assetBundleLoader.progress;
		}

		public override long GetNeedDownloadBytes()
		{
			return _assetBundleLoader.GetNeedDownloadBytes();
		}
		public override long GetDownloadedBytes()
		{
			return _assetBundleLoader.GetDownloadedBytes();
		}

		public override List<string> GetAssetBundlePathList()
		{
			return _assetBundleLoader.GetAssetBundlePathList();
		}


		public override void Update()
		{
		}

		private void _OnAssetBundleAsyncLoaderSuccess(AssetBundleAsyncLoader assetBundleAsyncLoader)
		{
			if (_assetBundleLoader != assetBundleAsyncLoader)
				return;
			resultInfo.isSuccess = true;
			RemoveListener<AssetBundleAsyncLoader>(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Success,
			  _OnAssetBundleAsyncLoaderSuccess);
		}

		private void _OnAssetBundleAsyncLoaderFail(AssetBundleAsyncLoader assetBundleAsyncLoader)
		{
			if (_assetBundleLoader != assetBundleAsyncLoader)
				return;
			resultInfo.isFail = true;
			RemoveListener<AssetBundleAsyncLoader>(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Fail,
			  _OnAssetBundleAsyncLoaderFail);
		}

		protected override void _OnSuccess()
		{
			base._OnSuccess();

			var assetBundleCat = this._assetBundleLoader.GetAssetBundleCat();
			var assetBundle = assetBundleCat.Get();
			var assets = assetBundle.LoadAssetWithSubAssets(_assetCat.assetPath);
			_assetCat.SetAssets(assets);

			FireEvent(null, AssetBundleEventNameConst.On_AssetAsyncLoader_Success, this);
		}

		protected override void _OnFail()
		{
			base._OnFail();
			FireEvent(null, AssetBundleEventNameConst.On_AssetAsyncLoader_Fail, this);
		}

		protected override void _OnDone()
		{
			base._OnDone();
			FireEvent(null, AssetBundleEventNameConst.On_AssetAsyncLoader_Done, this);
		}

		//需要手动释放
		public override void Reset()
		{
			_assetBundleLoader = null;
			base.Reset();
		}
	}
}