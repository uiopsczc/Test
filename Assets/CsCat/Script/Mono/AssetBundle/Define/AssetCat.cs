using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public class AssetCat
	{
		public Dictionary<string, Dictionary<Type, Object>> assetDict =
			new Dictionary<string, Dictionary<Type, Object>>();

		public AssetBundleCat assetBundleCat;
		public string assetPath;
		private ResultInfo _resultInfo;


		public ResultInfo resultInfo => _resultInfo ?? (_resultInfo = new ResultInfo(
			() =>
			{
				_onLoadSuccessCallback?.Invoke(this);
				RemoveAllOnLoadSuccessCallback();
			}, () =>
			{
				_onLoadFailCallback?.Invoke(this);
				RemoveAllOnLoadFailCallback();
			}, () =>
			{
				_onLoadDoneCallback?.Invoke(this);
				RemoveAllOnLoadDoneCallback();
			}));

		public int refCount { get; private set; }


		private Action<AssetCat> _onLoadSuccessCallback;
		private Action<AssetCat> _onLoadFailCallback;
		private Action<AssetCat> _onLoadDoneCallback;

		//用来追溯来源，就是取消下载的时候，把那些关于它自己的部分的callback删除
		private ValueListDictionary<object, Action<AssetCat>> _onLoadSuccessCallbackListDict =
			new ValueListDictionary<object, Action<AssetCat>>();

		private ValueListDictionary<object, Action<AssetCat>> _onLoadFailCallbackListDict =
			new ValueListDictionary<object, Action<AssetCat>>();

		private ValueListDictionary<object, Action<AssetCat>> _onLoadDoneCallbackListDict =
			new ValueListDictionary<object, Action<AssetCat>>();

		public AssetCat(string assetPath)
		{
			this.assetPath = assetPath;
		}

		public void AddOnLoadSuccessCallback(Action<AssetCat> onLoadSuccessCallback, object callbackCause = null)
		{
			if (onLoadSuccessCallback == null)
				return;

			this._onLoadSuccessCallback += onLoadSuccessCallback;
			if (callbackCause == null)
				callbackCause = this;
			_onLoadSuccessCallbackListDict.Add(callbackCause, onLoadSuccessCallback);
		}

		public void AddOnLoadFailCallback(Action<AssetCat> onLoadFailCallback, object callbackCause = null)
		{
			if (onLoadFailCallback == null)
				return;

			this._onLoadFailCallback += onLoadFailCallback;
			if (callbackCause == null)
				callbackCause = this;
			_onLoadFailCallbackListDict.Add(callbackCause, onLoadFailCallback);
		}

		public void AddOnLoadDoneCallback(Action<AssetCat> onLoadDoneCallback, object callbackCause = null)
		{
			if (onLoadDoneCallback == null)
				return;
			this._onLoadDoneCallback += onLoadDoneCallback;
			if (callbackCause == null)
				callbackCause = this;
			_onLoadDoneCallbackListDict.Add(callbackCause, onLoadDoneCallback);
		}


		public void RemoveOnLoadSuccessCallback(Action<AssetCat> onLoadSuccessCallback, object callbackCause = null)
		{
			if (onLoadSuccessCallback == null)
				return;

			this._onLoadSuccessCallback -= onLoadSuccessCallback;
			if (callbackCause == null)
				callbackCause = this;
			_onLoadSuccessCallbackListDict.Remove(callbackCause, onLoadSuccessCallback);
		}

		public void RemoveOnLoadFailCallback(Action<AssetCat> onLoadFailCallback, object callbackCause = null)
		{
			if (onLoadFailCallback == null)
				return;

			this._onLoadFailCallback -= onLoadFailCallback;
			if (callbackCause == null)
				callbackCause = this;
			_onLoadFailCallbackListDict.Remove(callbackCause, onLoadFailCallback);
		}

		public void RemoveOnLoadDoneCallback(Action<AssetCat> onLoadDoneCallback, object callbackCause = null)
		{
			if (onLoadDoneCallback == null)
				return;

			this._onLoadDoneCallback -= onLoadDoneCallback;
			if (callbackCause == null)
				callbackCause = this;
			_onLoadDoneCallbackListDict.Remove(callbackCause, onLoadDoneCallback);
		}

		public void RemoveOnLoadSuccessCallback(object callbackCause = null)
		{
			if (callbackCause == null)
				callbackCause = this;
			_onLoadSuccessCallbackListDict.Foreach(callbackCause,
				callback => { this._onLoadSuccessCallback -= callback; });
			_onLoadSuccessCallbackListDict.Remove(callbackCause);
		}

		public void RemoveOnLoadFailCallback(object callbackCause = null)
		{
			if (callbackCause == null)
				callbackCause = this;
			_onLoadFailCallbackListDict.Foreach(callbackCause,
				callback => { this._onLoadFailCallback -= callback; });
			_onLoadFailCallbackListDict.Remove(callbackCause);
		}

		public void RemoveOnLoadDoneCallback(object callbackCause = null)
		{
			if (callbackCause == null)
				callbackCause = this;
			_onLoadDoneCallbackListDict.Foreach(callbackCause,
				callback => { this._onLoadDoneCallback -= callback; });
			_onLoadDoneCallbackListDict.Remove(callbackCause);
		}

		public void RemoveAllOnLoadSuccessCallback()
		{
			_onLoadSuccessCallbackListDict.Clear();
			this._onLoadSuccessCallback = null;
		}

		public void RemoveAllOnLoadFailCallback()
		{
			_onLoadFailCallbackListDict.Clear();
			this._onLoadFailCallback = null;
		}

		public void RemoveAllOnLoadDoneCallback()
		{
			_onLoadDoneCallbackListDict.Clear();
			this._onLoadDoneCallback = null;
		}

		public void RemoveCallback(object callbackCause = null)
		{
			RemoveOnLoadSuccessCallback(callbackCause);
			RemoveOnLoadFailCallback(callbackCause);
			RemoveOnLoadSuccessCallback(callbackCause);
		}

		public void RemoveAllCallback()
		{
			RemoveAllOnLoadSuccessCallback();
			RemoveAllOnLoadFailCallback();
			RemoveAllOnLoadSuccessCallback();
		}


		public Object Get(string subAssetPath = null, Type type = null)
		{
			//    LogCat.logWarning(sub_asset_path, asset_dict);
			if (assetDict.Count == 0)
				return null;
			if (subAssetPath.IsNullOrWhiteSpace())
				return assetDict.Values.ToArray()[0].Values.ToArray()[0];

			if (type != null)
				return assetDict[subAssetPath][type];


			return assetDict[subAssetPath].Values.ToArray()[0];
		}

		public T Get<T>(string subAssetPath = null) where T : Object
		{
			return (T) Get(subAssetPath, typeof(T));
		}

		public bool IsLoadSuccess()
		{
			return this.resultInfo.isSuccess;
		}

		public bool IsLoadFail()
		{
			return this.resultInfo.isFail;
		}

		public bool IsLoadDone()
		{
			return this.resultInfo.isDone;
		}


		public void AddRefCount()
		{
			refCount++;
			//Debug.LogError(asset_path+"++: "+refCount);
		}

		public int SubRefCount(int subValue = 1, bool isRemoveAssetIfNoRef = true)
		{
			subValue = Math.Abs(subValue);
			refCount = refCount - subValue;

			//Debug.LogError(asset_path + "--: " + refCount);
			if (refCount < 0)
				refCount = 0;
			if (isRemoveAssetIfNoRef && refCount == 0)
			{
				if (IsLoadDone())
				{
					if (Application.isPlaying)
						Client.instance.assetBundleManager.AddAssetCatOfNoRef(this); //延迟删除
				}
				else
					_onLoadSuccessCallback += asset_cat =>
					{
						Client.instance.assetBundleManager.AddAssetCatOfNoRef(this);
					}; //延迟删除;
			}

			//AssetBundleManager.Instance.RemoveAssetCat(asset_path);
			return refCount;
		}

		public void CheckNoRef()
		{
			SubRefCount(0, true);
		}

		public void SetAssets(Object[] assets)
		{
			for (var i = 0; i < assets.Length; i++)
			{
				var asset = assets[i];
				if (!assetDict.ContainsKey(asset.name))
					assetDict[asset.name] = new Dictionary<Type, Object>();
				assetDict[asset.name][asset.GetType()] = asset;
			}

			OnSetAssets();
		}

		private void OnSetAssets()
		{
#if UNITY_EDITOR
			// 说明：在Editor模拟时，Shader要重新指定
			HandleAssetShader();
#endif
			this.resultInfo.isSuccess = true;
		}
#if UNITY_EDITOR
		private void HandleAssetShader()
		{
			var mat = Get() as Material;
			if (mat != null)
				HandleAssetShader_Mat(ref mat);
			var go = Get() as GameObject;
			if (go != null)
				HandleAssetShader_GameObject(go);
		}

		private void HandleAssetShader_Mat(ref Material mat)
		{
			var shader = mat.shader;
			if (shader != null)
			{
				var shaderName = shader.name;
				mat.shader = ShaderUtilCat.FindShader(shaderName);
			}
		}

		private void HandleAssetShader_GameObject(GameObject go)
		{
			var renderers = go.GetComponentsInChildren<Renderer>();
			for (var j = 0; j < renderers.Length; j++)
			{
				var material = renderers[j].sharedMaterial;
				if (material == null)
					continue;
				HandleAssetShader_Mat(ref material);
			}
		}

#endif
	}
}