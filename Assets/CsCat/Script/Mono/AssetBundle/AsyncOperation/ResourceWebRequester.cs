using UnityEngine;
using UnityEngine.Networking;

namespace CsCat
{
	public class ResourceWebRequester : ResourceAsyncOperation
	{
		protected WWW www;
		public AssetBundleCat assetBundleCat;
		private long? _needDownloadBytes;


		public bool isNotCache { get; protected set; }
		public string url { get; protected set; }
		public AssetBundle assetBundle => www.assetBundle;
		public byte[] bytes => www.bytes;
		public string text => www.text;
		public string error => www.error.IsNullOrWhiteSpace() ? null : www.error;



		public void Init(string url, bool isNotCache = false)
		{
			this.url = url.WWWURLHandle();
			this.isNotCache = isNotCache;
		}


		public void Init(AssetBundleCat assetBundleCat, string url, bool isNotCache = false)
		{
			Init(url, isNotCache);
			this.assetBundleCat = assetBundleCat;
			this.assetBundleCat.resourceWebRequester = this;
		}

		public override void Start()
		{
			base.Start();
			www = new WWW(url);
			//    Debug.LogError("loading:"+url);
		}

		protected override float GetProgress()
		{
			if (resultInfo.isDone)
				return 1.0f;

			return www?.progress ?? 0f;
		}

		public override long GetDownloadedBytes()
		{
			return www?.bytesDownloaded ?? 0;
		}

		public override long GetNeedDownloadBytes()
		{
			if (_needDownloadBytes != null)
				return _needDownloadBytes.Value;
			var assetBundleName = assetBundleCat?.assetBundleName;
			//配置中有记录该assetBundle_name的大小
			if (assetBundleName != null && Client.instance.assetBundleManager.assetBundleMap != null &&
				Client.instance.assetBundleManager.assetBundleMap.dict.ContainsKey(assetBundleName))
				return Client.instance.assetBundleManager.assetBundleMap.dict[assetBundleName];
			//配置中没有记录该assetBundle_name的大小，且未Start
			if (www == null)
				return base.GetNeedDownloadBytes();
			//配置中没有记录该assetBundle_name的大小，开始了Start
			if (_needDownloadBytes != null)
				return _needDownloadBytes.Value;
			var contentLength = www.GetFieldValue<UnityWebRequest>("_uwr").GetResponseHeader("Content-Length");
			if (contentLength.IsNullOrEmpty()) return base.GetNeedDownloadBytes();
			_needDownloadBytes = long.Parse(contentLength);
			return _needDownloadBytes.Value;
		}

		public override void Update()
		{
			if (resultInfo.isDone || www == null)
				return;
			if (!string.IsNullOrEmpty(www.error))
				resultInfo.isFail = true;
			else
			{
				if (www.isDone)
					resultInfo.isSuccess = true;
			}
		}

		protected override void OnSuccess()
		{
			base.OnSuccess();
			// 无缓存，不计引用计数、Creater使用后由上层回收，所以这里不需要做任何处理
			if (assetBundleCat != null && !isNotCache)
			{
				// AB缓存
				// 说明：有错误也缓存下来，只不过资源为空
				// 1、避免再次错误加载
				// 2、如果不存下来加载器将无法判断什么时候结束
				assetBundleCat.assetBundle = assetBundle;
			}

			Broadcast(null, AssetBundleEventNameConst.On_ResourceWebRequester_Success, this);
		}

		protected override void OnFail()
		{
			base.OnFail();
			LogCat.warn("服务器连接失败[未启动?]", www.url, www.error);
			Broadcast(null, AssetBundleEventNameConst.On_ResourceWebRequester_Fail, this);
		}


		protected override void OnDone()
		{
			base.OnDone();
			//完成时不再需要resourceWebRequester
			if (assetBundleCat != null)
				assetBundleCat.resourceWebRequester = null;
			Broadcast(null, AssetBundleEventNameConst.On_ResourceWebRequester_Done, this);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			assetBundleCat = null;
			_needDownloadBytes = null;
			if (www != null)
			{
				www.Dispose();
				www = null;
			}
		}
	}
}