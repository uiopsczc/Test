using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class AssetBundleCat
	{
		private AssetBundle _assetBundle;
		public string assetBundleName;
		public ResultInfo _resultInfo;
		public List<AssetBundleCat> dependenceAssetBundleCatList = new List<AssetBundleCat>();
		public ResourceWebRequester resourceWebRequester;


		public int refCount { get; private set; }
		public ResultInfo resultInfo => _resultInfo ?? (_resultInfo = new ResultInfo(() => onSuccessCallback?.Invoke(this), () => onFailCallback?.Invoke(this), () => onDoneCallback?.Invoke(this)));
		public AssetBundle assetBundle
		{
			set
			{
				_assetBundle = value;
				if (assetBundle != null)
					resultInfo.isSuccess = true;
			}
			private get => _assetBundle;
		}


		public Action<AssetBundleCat> onSuccessCallback;
		public Action<AssetBundleCat> onFailCallback;
		public Action<AssetBundleCat> onDoneCallback;

		public AssetBundleCat(string assetBundleName, AssetBundle assetBundle = null)
		{
			this.assetBundleName = assetBundleName;
			this.assetBundle = assetBundle;
		}

		public void AddDependenceAssetBundleCat(AssetBundleCat dependenceAssetBundleCat)
		{
			this.dependenceAssetBundleCatList.Add(dependenceAssetBundleCat);
		}

		public AssetBundle Get()
		{
			return assetBundle;
		}

		public bool IsLoadSuccess()
		{
			return resultInfo.isSuccess;
		}

		public bool IsLoadFail()
		{
			return resultInfo.isFail;
		}

		public bool IsLoadDone()
		{
			return resultInfo.isDone;
		}

		public string[] GetAllDependencePaths()
		{
			return Client.instance.assetBundleManager.manifest.GetAllDependencies(assetBundleName);
		}

		public void AddRefCountOfDependencies()
		{
			for (var i = 0; i < this.dependenceAssetBundleCatList.Count; i++)
			{
				var dependenceAssetBundleCat = this.dependenceAssetBundleCatList[i];
				dependenceAssetBundleCat.AddRefCount();
			}
		}

		public void AddRefCount()
		{
			refCount++;
		}

		public void SubRefCount()
		{
			refCount--;
			if (refCount <= 0)
			{
				refCount = 0;
				Client.instance.assetBundleManager.RemoveAssetBundleCat(this);
			}
		}

		public void SubRefCountOfDependencies()
		{
			for (var i = 0; i < this.dependenceAssetBundleCatList.Count; i++)
			{
				var dependenceAssetBundleCat = this.dependenceAssetBundleCatList[i];
				dependenceAssetBundleCat.SubRefCount();
			}
		}

	}
}