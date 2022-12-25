using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace CsCat
{
	public class ResourceWebRequester : ResourceAsyncOperation
	{
		protected UnityWebRequest _unityWebRequest;
		public AssetBundleCat assetBundleCat;
		private long? _needDownloadBytes;


		protected bool _isNotCache;
		protected string _url;
		public AssetBundle assetBundle => AssetBundle.LoadFromMemory(bytes);
		public byte[] bytes => _unityWebRequest.downloadHandler.data;
		public string text => _unityWebRequest.downloadHandler.text;
		public string error => _unityWebRequest.error.IsNullOrWhiteSpace() ? null : _unityWebRequest.error;



		public void Init(string url, bool isNotCache = false)
		{
			this._url = url.WWWURLHandle();
			this._isNotCache = isNotCache;
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
			_unityWebRequest = UnityWebRequest.Head(_url);
			//    Debug.LogError("loading:"+url);
		}

		public bool IsNotCache()
		{
			return this._isNotCache;
		}

		public string GetURL()
		{
			return this._url;
		}

		protected override float _GetProgress()
		{
			if (resultInfo.isDone)
				return 1.0f;

			return _unityWebRequest?.downloadProgress ?? 0f;
		}

		public override long GetDownloadedBytes()
		{
			return _unityWebRequest?.downloadHandler.data.Length ?? 0;
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
			if (_unityWebRequest == null)
				return base.GetNeedDownloadBytes();
			//配置中没有记录该assetBundle_name的大小，开始了Start
			if (_needDownloadBytes != null)
				return _needDownloadBytes.Value;
			var contentLength = _unityWebRequest.GetResponseHeader("Content-Length");
			if (contentLength.IsNullOrEmpty()) return base.GetNeedDownloadBytes();
			_needDownloadBytes = long.Parse(contentLength);
			return _needDownloadBytes.Value;
		}

		public override void Update()
		{
			if (resultInfo.isDone || _unityWebRequest == null)
				return;
			if (!string.IsNullOrEmpty(_unityWebRequest.error))
				resultInfo.isFail = true;
			else
			{
				if (_unityWebRequest.isDone)
					resultInfo.isSuccess = true;
			}
		}

		protected override void _OnSuccess()
		{
			base._OnSuccess();
			// 无缓存，不计引用计数、Creater使用后由上层回收，所以这里不需要做任何处理
			if (assetBundleCat != null && !_isNotCache)
			{
				// AB缓存
				// 说明：有错误也缓存下来，只不过资源为空
				// 1、避免再次错误加载
				// 2、如果不存下来加载器将无法判断什么时候结束
				assetBundleCat.assetBundle = assetBundle;
			}

			FireEvent(null, AssetBundleEventNameConst.On_ResourceWebRequester_Success, this);
		}

		protected override void _OnFail()
		{
			base._OnFail();
			LogCat.warn("服务器连接失败[未启动?]", _unityWebRequest.url, _unityWebRequest.error);
			FireEvent(null, AssetBundleEventNameConst.On_ResourceWebRequester_Fail, this);
		}


		protected override void _OnDone()
		{
			base._OnDone();
			//完成时不再需要resourceWebRequester
			if (assetBundleCat != null)
				assetBundleCat.resourceWebRequester = null;
			FireEvent(null, AssetBundleEventNameConst.On_ResourceWebRequester_Done, this);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			assetBundleCat = null;
			_needDownloadBytes = null;
			if (_unityWebRequest != null)
			{
				_unityWebRequest.Dispose();
				_unityWebRequest = null;
			}
		}
	}
}