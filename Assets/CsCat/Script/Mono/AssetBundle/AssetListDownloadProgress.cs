using System;
using System.Collections.Generic;

namespace CsCat
{
	/// <summary>
	/// 用来获取assetList的下载进度【包括需要下载多少bytes，已下载多少bytes，进度[0,1]】
	/// </summary>
	public class AssetListDownloadProgress
	{
		public List<string> assetPathList;
		public Dictionary<string, long> assetBundleDownloadedBytesDict = new Dictionary<string, long>();
		private readonly long _needDownloadBytes;

		public AssetListDownloadProgress(List<string> assetPathList)
		{
			assetPathList.Unique();
			List<string> assetBundleNameList = new List<string>();
			for (var i = 0; i < assetPathList.Count; i++)
			{
				var assetPath = assetPathList[i];
				var assetAsyncloader = Client.instance.assetBundleManager.assetAsyncloaderProcessingList.Find(
					_assetAsyncloader =>
						_assetAsyncloader._assetCat.assetPath.Equals(assetPath));
				if (assetAsyncloader != null && !assetAsyncloader.resultInfo.isDone)
				{
					if (!assetAsyncloader.GetAssetBundlePathList().IsNullOrEmpty())
						assetBundleNameList.AddRange(assetAsyncloader.GetAssetBundlePathList());
				}
			}

			assetBundleNameList.Unique();
			for (var i = 0; i < assetBundleNameList.Count; i++)
			{
				var assetBundleName = assetBundleNameList[i];
				assetBundleDownloadedBytesDict[assetBundleName] = 0;
				_needDownloadBytes += Client.instance.assetBundleManager.assetBundleMap.dict[assetBundleName];
			}
		}

		public long GetNeedDownloadBytes()
		{
			return this._needDownloadBytes;
		}

		public long GetDownloadedBytes()
		{
			long downloadedBytes = 0;
			foreach (var keyValue in assetBundleDownloadedBytesDict)
			{
				var assetBundleName = keyValue.Key;
				var webRequesting = Client.instance.assetBundleManager.GetAssetBundleAsyncWebRequester(assetBundleName);
				assetBundleDownloadedBytesDict[assetBundleName] =
				  webRequesting?.GetDownloadedBytes() ??
				  Client.instance.assetBundleManager.assetBundleMap.dict[assetBundleName];
				downloadedBytes += assetBundleDownloadedBytesDict[assetBundleName];
			}
			return downloadedBytes;
		}

		public float GetProgress()
		{
			if (GetNeedDownloadBytes() == 0)
				return 1;
			return (float)GetDownloadedBytes() / GetNeedDownloadBytes();
		}
	}
}