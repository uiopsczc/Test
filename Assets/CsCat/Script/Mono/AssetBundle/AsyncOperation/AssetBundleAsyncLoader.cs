using System.Collections.Generic;

namespace CsCat
{
	/// <summary>
	///   功能：Assetbundle加载器，给逻辑层使用（预加载），支持协程操作
	/// </summary>
	public class AssetBundleAsyncLoader : BaseAssetBundleAsyncLoader
	{
		protected int _totalWaitingAssetBundleCatCount;

		protected Dictionary<string, AssetBundleCat> _waitingAssetBundleCatDict =
			new Dictionary<string, AssetBundleCat>();

		protected Dictionary<string, long> _assetBundleDownloadedBytesDict = new Dictionary<string, long>();
		protected long _needDownloadBytes;


		public void Init(AssetBundleCat assetBundleCat)
		{
			this._assetBundleName = _assetBundleName;
			this._assetBundleCat = assetBundleCat;
			// 只添加没有被加载过的
			if (!assetBundleCat.IsLoadDone())
			{
				_waitingAssetBundleCatDict[assetBundleCat.assetBundleName] = assetBundleCat;
				_assetBundleDownloadedBytesDict[assetBundleCat.assetBundleName] = 0;
			}

			for (var i = 0; i < assetBundleCat.dependenceAssetBundleCatList.Count; i++)
			{
				var dependenceAssetBundleCat = assetBundleCat.dependenceAssetBundleCatList[i];
				if (dependenceAssetBundleCat.IsLoadDone())
					continue;
				_waitingAssetBundleCatDict[dependenceAssetBundleCat.assetBundleName] = dependenceAssetBundleCat;
				_assetBundleDownloadedBytesDict[dependenceAssetBundleCat.assetBundleName] = 0;
			}

			_totalWaitingAssetBundleCatCount = _waitingAssetBundleCatDict.Count;


			_needDownloadBytes = 0;
			foreach (var keyValue in _assetBundleDownloadedBytesDict)
			{
				var curAssetBundleName = keyValue.Key;
				_needDownloadBytes += Client.instance.assetBundleManager.assetBundleMap.dict[curAssetBundleName];
			}

			if (_totalWaitingAssetBundleCatCount == 0)
			{
				resultInfo.isSuccess = true;
				return;
			}

			AddListener<ResourceWebRequester>(null, AssetBundleEventNameConst.On_ResourceWebRequester_Fail,
				OnResourceWebRequesterFail);
			AddListener<ResourceWebRequester>(null, AssetBundleEventNameConst.On_ResourceWebRequester_Success,
				OnResourceWebRequesterSuccess);
		}


		protected override float _GetProgress()
		{
			if (resultInfo.isDone)
				return 1.0f;

			var progressValue = (float) GetDownloadedBytes() / GetNeedDownloadBytes();
			return progressValue;
		}

		public override List<string> GetAssetBundlePathList()
		{
			List<string> result = new List<string>();
			foreach (var keyValue in _assetBundleDownloadedBytesDict)
			{
				var assetBundleName = keyValue.Key;
				result.Add(assetBundleName);
			}
			return result;
		}


		public override long GetNeedDownloadBytes()
		{
			return _needDownloadBytes;
		}

		public override long GetDownloadedBytes()
		{
			if (resultInfo.isDone)
				return GetNeedDownloadBytes();
			long downloadedBytes = 0;
			foreach (var keyValue in _assetBundleDownloadedBytesDict)
			{
				var assetBundleName = keyValue.Key;
				bool isLoadDone = !_waitingAssetBundleCatDict.ContainsKey(assetBundleName);
				if (isLoadDone) //已经下载完的assetBundle_name
					_assetBundleDownloadedBytesDict[assetBundleName] =
						Client.instance.assetBundleManager.assetBundleMap.GetAssetBundleBytes(assetBundleName);
				else
				{
					var resourceWebRequester = this._waitingAssetBundleCatDict[assetBundleName].resourceWebRequester;
					_assetBundleDownloadedBytesDict[assetBundleName] =
						resourceWebRequester?.GetDownloadedBytes() ??
						Client.instance.assetBundleManager.assetBundleMap.GetAssetBundleBytes(assetBundleName);
				}

				downloadedBytes += _assetBundleDownloadedBytesDict[assetBundleName];
			}

			return downloadedBytes;
		}

		public override void Update()
		{
		}

		private void OnResourceWebRequesterSuccess(ResourceWebRequester resourceWebRequester)
		{
			if (resultInfo.isDone)
				return;
			if (!_waitingAssetBundleCatDict.ContainsValue(resourceWebRequester.assetBundleCat) ||
			    resourceWebRequester._isNotCache) return;
			_waitingAssetBundleCatDict.Remove(resourceWebRequester.assetBundleCat.assetBundleName);
			if (_waitingAssetBundleCatDict.Count == 0)
				resultInfo.isSuccess = true;
		}

		private void OnResourceWebRequesterFail(ResourceWebRequester resourceWebRequester)
		{
			if (resultInfo.isDone)
				return;
			if (!_waitingAssetBundleCatDict.ContainsValue(resourceWebRequester.assetBundleCat) ||
			    resourceWebRequester._isNotCache) return;
			resultInfo.isFail = true;
			RemoveListener<ResourceWebRequester>(null, AssetBundleEventNameConst.On_ResourceWebRequester_Fail,
				OnResourceWebRequesterFail);
		}


		protected override void _OnSuccess()
		{
			base._OnSuccess();
			FireEvent(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Success, this);
		}

		protected override void _OnFail()
		{
			base._OnFail();
			FireEvent(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Fail, this);
		}

		protected override void _OnDone()
		{
			base._OnDone();
			FireEvent(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Done, this);
			this.Destroy();
			PoolCatManagerUtil.Despawn(this);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			_totalWaitingAssetBundleCatCount = 0;
			_waitingAssetBundleCatDict.Clear();
			_assetBundleDownloadedBytesDict.Clear();
			_needDownloadBytes = 0;
		}
	}
}