using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class AssetBundleCat
	{
		private AssetBundle _assetBundle;
		public string assetBundle_name;
		public ResultInfo _resultInfo;
		public List<AssetBundleCat> dependence_assetBundleCat_list = new List<AssetBundleCat>();
		public ResourceWebRequester resourceWebRequester;


		public int ref_count { get; private set; }
		public ResultInfo resultInfo => _resultInfo ?? (_resultInfo = new ResultInfo(() => on_success_callback?.Invoke(this), () => on_fail_callback?.Invoke(this), () => on_done_callback?.Invoke(this)));
		public AssetBundle assetBundle
		{
			set
			{
				_assetBundle = value;
				if (assetBundle != null)
					resultInfo.isSuccess = true;
			}
			private get { return _assetBundle; }
		}


		public Action<AssetBundleCat> on_success_callback;
		public Action<AssetBundleCat> on_fail_callback;
		public Action<AssetBundleCat> on_done_callback;

		public AssetBundleCat(string assetBundle_name, AssetBundle assetBundle = null)
		{
			this.assetBundle_name = assetBundle_name;
			this.assetBundle = assetBundle;
		}

		public void AddDependenceAssetBundleCat(AssetBundleCat dependence_assetBundleCat)
		{
			this.dependence_assetBundleCat_list.Add(dependence_assetBundleCat);
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
			return Client.instance.assetBundleManager.manifest.GetAllDependencies(assetBundle_name);
		}

		public void AddRefCountOfDependencies()
		{
			foreach (var dependence_assetBundleCat in this.dependence_assetBundleCat_list)
				dependence_assetBundleCat.AddRefCount();
		}

		public void AddRefCount()
		{
			ref_count++;
		}

		public void SubRefCount()
		{
			ref_count--;
			if (ref_count <= 0)
			{
				ref_count = 0;
				Client.instance.assetBundleManager.RemoveAssetBundleCat(this);
			}
		}

		public void SubRefCountOfDependencies()
		{
			foreach (var dependence_assetBundleCat in this.dependence_assetBundleCat_list)
				dependence_assetBundleCat.SubRefCount();
		}

	}
}