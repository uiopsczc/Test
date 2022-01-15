using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class AssetBundleManager
	{
		private bool CreateAssetBundleAsync(string assetBundleName)
		{
			if (__IsAssetBundleLoadSuccess(assetBundleName) || resourceWebRequesterAllDict.ContainsKey(assetBundleName))
				return false;


			// webRequester持有的引用,webRequester结束后会删除该次引用(在AssetBunldeMananger中的OnProsessingWebRequester的进行删除本次引用操作)
			var assetBundleCat = new AssetBundleCat(assetBundleName);
			assetBundleCat.AddRefCount();
			AddAssetBundleCat(assetBundleCat);

			var resourceWebRequester = PoolCatManagerUtil.Spawn<ResourceWebRequester>();
			var url = assetBundleName.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
			resourceWebRequester.Init(assetBundleCat, url);
			resourceWebRequesterAllDict[assetBundleName] = resourceWebRequester;
			resourceWebRequesterWaitingQueue.Enqueue(resourceWebRequester);

			return true;
		}

		// 异步请求Assetbundle资源
		private BaseAssetBundleAsyncLoader __LoadAssetBundleAsync(string assetBundleName)
		{
			if (Application.isEditor && EditorModeConst.IsEditorMode)
				return new EditorAssetBundleAsyncLoader(assetBundleName);

			var assetBundleAsyncLoader = PoolCatManagerUtil.Spawn<AssetBundleAsyncLoader>();
			assetBundleAsyncLoaderProcessingList.Add(assetBundleAsyncLoader);
			var dependanceNames = manifest.GetAllDependencies(assetBundleName);
			List<AssetBundleCat> dependanceAssetBundleCatList = new List<AssetBundleCat>();
			for (var i = 0; i < dependanceNames.Length; i++)
			{
				var dependanceName = dependanceNames[i];
				CreateAssetBundleAsync(dependanceName);
				var dependanceAssetBundleCat = __GetAssetBundleCat(dependanceName);
				// A依赖于B，A对B持有引用
				dependanceAssetBundleCat.AddRefCount();
				dependanceAssetBundleCatList.Add(dependanceAssetBundleCat);
			}

			CreateAssetBundleAsync(assetBundleName);
			var assetBundleCat = __GetAssetBundleCat(assetBundleName);
			for (var i = 0; i < dependanceAssetBundleCatList.Count; i++)
			{
				var dependanceAssetBundleCat = dependanceAssetBundleCatList[i];
				assetBundleCat.AddDependenceAssetBundleCat(dependanceAssetBundleCat);
			}

			assetBundleAsyncLoader.Init(assetBundleCat);
			// 添加assetBundleAsyncLoader加载器对AB持有的引用,assetBundleAsyncLoader结束后会删除该次引用(在AssetBunldeMananger中的OnProsessingAssetBundleAsyncLoader的进行删除本次引用操作)
			assetBundleCat.AddRefCount();
			return assetBundleAsyncLoader;
		}


		private void OnAssetBundleAsyncLoaderFail(AssetBundleAsyncLoader assetBundleAsyncLoader)
		{
			if (!assetBundleAsyncLoaderProcessingList.Contains(assetBundleAsyncLoader))
				return;

			//assetCat的OnFail中反过来回调减引用
		}

		private void OnAssetBundleAsyncLoaderDone(AssetBundleAsyncLoader assetBundleAsyncLoader)
		{
			if (!assetBundleAsyncLoaderProcessingList.Contains(assetBundleAsyncLoader))
				return;

			// 解除assetBundleAsyncLoader加载器对AB持有的引用
			assetBundleAsyncLoader.assetBundleCat.SubRefCount();
			assetBundleAsyncLoaderProcessingList.Remove(assetBundleAsyncLoader);
		}

		private bool __IsAssetBundleLoadSuccess(string assetBundleName)
		{
			return assetBundleCatDict.ContainsKey(assetBundleName) && __GetAssetBundleCat(assetBundleName).IsLoadSuccess();
		}

		private AssetBundleCat __GetAssetBundleCat(string assetBundleName)
		{
			assetBundleCatDict.TryGetValue(assetBundleName, out var target);
			return target;
		}

		public void RemoveAssetBundleCat(string assetBundleName)
		{
			RemoveAssetBundleCat(this.__GetAssetBundleCat(assetBundleName));
		}

		public void RemoveAssetBundleCat(AssetBundleCat assetBundleCat)
		{
			if (assetBundleCat == null)
				return;
			LogCat.log(assetBundleCat.assetBundleName + " removed");
			assetBundleCat.Get()?.Unload(true);//最关键的一步
			assetBundleCatDict.RemoveByFunc((key, value) => value == assetBundleCat);
		}


		protected void AddAssetBundleCat(AssetBundleCat assetBundle)
		{
			assetBundleCatDict[assetBundle.assetBundleName] = assetBundle;
		}

		public ResourceWebRequester GetAssetBundleAsyncWebRequester(string assetBundleName)
		{
			resourceWebRequesterAllDict.TryGetValue(assetBundleName, out var webRequester);
			return webRequester;
		}
	}
}