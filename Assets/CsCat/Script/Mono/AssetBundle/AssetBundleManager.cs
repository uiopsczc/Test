using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace CsCat
{
	/// <summary>
	///   功能：assetBundle管理类，为外部提供统一的资源加载界面、协调Assetbundle各个子系统的运行
	///   注意：
	///   1、抛弃Resources目录的使用，官方建议：https://unity3d.com/cn/learn/tutorials/temas/best-practices/resources-folder?playlist=30089
	///   2、提供Editor和Simulate模式，前者不适用Assetbundle，直接加载资源，快速开发；后者使用Assetbundle，用本地服务器模拟资源更新
	///   3、场景不进行打包，场景资源打包为预设
	///   4、只提供异步接口，所有加载按异步进行
	///   5、采用LZMA压缩方式，性能瓶颈在Assetbundle加载上，ab加载异步，asset加载同步，ab加载后导出全部asset并卸载ab
	///   7、随意卸载公共ab包可能导致内存资源重复，最好在切换场景时再手动清理不需要的公共ab包
	///   8、常驻包（公共ab包）引用计数不为0时手动清理无效，正在等待加载的所有ab包不能强行终止---一旦发起创建就一定要等操作结束，异步过程进行中清理无效
	///   9、切换场景时最好预加载所有可能使用到的资源，所有加载器用完以后记得Dispose回收，清理GC时注意先释放所有Asset缓存
	///   10、逻辑层所有Asset路径带文件类型后缀，且是AssetBundleConfig.ResourcesFolderName下的相对路径，注意：路径区分大小写
	/// </summary>
	public partial class AssetBundleManager : TickObject
	{
		// 最大同时进行的ab创建数量
		private const int _Max_AssetBundle_Create_Num = 20;

		private readonly List<AssetCat>
			_assetCatOfNoRefList =
				new List<AssetCat>(); //这里使用延迟删除处理，和GameObject.Destroy类似,检查因为没有ref而需要删除的asset（如果有ref则不会删除）

		// 逻辑层正在等待的asset加载异步句柄
		public readonly List<AssetAsyncLoader> assetAsyncloaderProcessingList = new List<AssetAsyncLoader>();


		// 逻辑层正在等待的ab加载异步句柄
		public readonly List<AssetBundleAsyncLoader> assetBundleAsyncLoaderProcessingList =
			new List<AssetBundleAsyncLoader>();

		// 加载数据请求：正在prosessing或者等待prosessing的资源请求
		public readonly Dictionary<string, ResourceWebRequester> resourceWebRequesterAllDict =
			new Dictionary<string, ResourceWebRequester>();

		// 正在处理的资源请求
		private readonly List<ResourceWebRequester> _resourceWebRequesterProcessingList =
			new List<ResourceWebRequester>();


		// 等待处理的资源请求
		private readonly Queue<ResourceWebRequester> _resourceWebRequesterWaitingQueue =
			new Queue<ResourceWebRequester>();

		// 常驻内存的(游戏中一直存在的)asset
		public Dictionary<string, bool> assetResidentDict = new Dictionary<string, bool>();

		// AssetBundle缓存包
		public Dictionary<string, AssetBundleCat> assetBundleCatDict = new Dictionary<string, AssetBundleCat>();

		// asset缓存
		public Dictionary<string, AssetCat> assetCatDict = new Dictionary<string, AssetCat>();

		// 资源路径相关的映射表
		public AssetPathMap assetPathMap;
		public AssetBundleMap assetBundleMap;

		// manifest：提供依赖关系查找以及hash值比对
		public Manifest manifest;


		public string downloadURL => URLSetting.Server_Resource_URL;

		public override void Init()
		{
			base.Init();

			AddListener<ResourceWebRequester>(null, AssetBundleEventNameConst.On_ResourceWebRequester_Done,
				_OnResourceWebRequesterDone);

			AddListener<AssetBundleAsyncLoader>(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Fail,
				_OnAssetBundleAsyncLoaderFail);
			AddListener<AssetBundleAsyncLoader>(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Done,
				_OnAssetBundleAsyncLoaderDone);

			AddListener<AssetAsyncLoader>(null, AssetBundleEventNameConst.On_AssetAsyncLoader_Fail,
				_OnAssetAsyncLoaderFail);
			AddListener<AssetAsyncLoader>(null, AssetBundleEventNameConst.On_AssetAsyncLoader_Done,
				_OnAssetAsyncLoaderDone);
		}


		public IEnumerator Initialize()
		{
			if (Application.isEditor && EditorModeConst.IsEditorMode)
				yield break;

			manifest = new Manifest();
			assetPathMap = new AssetPathMap();
			assetBundleMap = new AssetBundleMap();

			// 说明：同时请求资源可以提高加载速度
			var manifestRequest =
				DownloadFileAsyncNoCache(
					manifest.assetBundleName.WithRootPath(FilePathConst.PersistentAssetBundleRoot));
			var assetPathMapRequest =
				DownloadFileAsyncNoCache(
					BuildConst.AssetPathMap_File_Name.WithRootPath(FilePathConst.PersistentAssetBundleRoot));
			var assetBundleMapRequest =
				DownloadFileAsyncNoCache(
					BuildConst.AssetBundleMap_File_Name.WithRootPath(FilePathConst.PersistentAssetBundleRoot));

			yield return manifestRequest;
			if (manifestRequest.error.IsNullOrWhiteSpace())
			{
				var assetBundle = manifestRequest.assetBundle;
				manifest.LoadFromAssetBundle(assetBundle);
				assetBundle.Unload(false);
				manifestRequest.Destroy();
				PoolCatManagerUtil.Despawn(manifestRequest);
			}


			yield return assetPathMapRequest;
			if (assetPathMapRequest.error.IsNullOrWhiteSpace())
			{
				assetPathMap.Initialize(assetPathMapRequest.text);
				assetPathMapRequest.Destroy();
				PoolCatManagerUtil.Despawn(assetPathMapRequest);
			}

			yield return assetBundleMapRequest;
			if (assetBundleMapRequest.error.IsNullOrWhiteSpace())
			{
				assetBundleMap.Initialize(assetBundleMapRequest.text);
				assetBundleMapRequest.Destroy();
				PoolCatManagerUtil.Despawn(assetBundleMapRequest);
			}

			LogCat.Log("AssetMananger init finished");
		}


		public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base.Update(deltaTime, unscaledDeltaTime);
			_OnProcessingResourceWebRequester();
			_OnProcessingAssetBundleAsyncLoader();
			_OnProcessingAssetAsyncLoader();
			_CheckAssetOfNoRefList();
		}

		private void _OnProcessingResourceWebRequester()
		{
			var slot_count = _resourceWebRequesterProcessingList.Count;
			while (slot_count < _Max_AssetBundle_Create_Num && _resourceWebRequesterWaitingQueue.Count > 0)
			{
				var resourceWebRequester = _resourceWebRequesterWaitingQueue.Dequeue();
				resourceWebRequester.Start();
				_resourceWebRequesterProcessingList.Add(resourceWebRequester);
				slot_count++;
			}

			for (var i = _resourceWebRequesterProcessingList.Count - 1; i >= 0; i--)
				_resourceWebRequesterProcessingList[i].Update();
		}

		private void _OnProcessingAssetBundleAsyncLoader()
		{
		}

		private void _OnProcessingAssetAsyncLoader()
		{
		}


		private void _OnResourceWebRequesterDone(ResourceWebRequester resourceWebRequester)
		{
			if (!_resourceWebRequesterProcessingList.Contains(resourceWebRequester))
				return;
			_resourceWebRequesterProcessingList.Remove(resourceWebRequester);
			resourceWebRequesterAllDict.RemoveByFunc((k, v) => v == resourceWebRequester);
			// 无缓存，不计引用计数、Creater使用后由上层回收，所以这里不需要做任何处理
			if (!resourceWebRequester.isNotCache)
			{
				var assetBundleCat = resourceWebRequester.assetBundleCat;
				// 解除webRequester对AB持有的引用
				assetBundleCat?.SubRefCount();
				resourceWebRequester.Destroy();
				PoolCatManagerUtil.Despawn(resourceWebRequester);
			}
		}


		//  public AssetCat GetAssetCat(Object asset)
		//  {
		//    foreach (var asset_path in assetCatDict.Keys)
		//      if (assetCatDict[asset_path].Get() == asset ||
		//          assetCatDict[asset_path].subAssetDict.Values.values.Contains(asset))
		//        return assetCatDict[asset_path];
		//    return null;
		//  }
	}
}