using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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
				onLoadSuccessCallback?.Invoke(this);
				RemoveAllOnLoadSuccessCallback();
			}, () =>
			{
				onLoadFailCallback?.Invoke(this);
				RemoveAllOnLoadFailCallback();
			}, () =>
			{
				onLoadDoneCallback?.Invoke(this);
				RemoveAllOnLoadDoneCallback();
			}));

		public int refCount { get; private set; }


		private Action<AssetCat> onLoadSuccessCallback;
		private Action<AssetCat> onLoadFailCallback;
		private Action<AssetCat> onLoadDoneCallback;

		//用来追溯来源，就是取消下载的时候，把那些关于它自己的部分的callback删除
		private ValueListDictionary<object, Action<AssetCat>> onLoadSuccessCallbackListDict =
			new ValueListDictionary<object, Action<AssetCat>>();

		private ValueListDictionary<object, Action<AssetCat>> onLoadFailCallbackListDict =
			new ValueListDictionary<object, Action<AssetCat>>();

		private ValueListDictionary<object, Action<AssetCat>> onLoadDoneCallbackListDict =
			new ValueListDictionary<object, Action<AssetCat>>();

		public AssetCat(string assetPath)
		{
			this.assetPath = assetPath;
		}

		public void AddOnLoadSuccessCallback(Action<AssetCat> onLoadSuccessCallback, object callbackCause = null)
		{
			if (onLoadSuccessCallback == null)
				return;

			this.onLoadSuccessCallback += onLoadSuccessCallback;
			if (callbackCause == null)
				callbackCause = this;
			onLoadSuccessCallbackListDict.Add(callbackCause, onLoadSuccessCallback);
		}

		public void AddOnLoadFailCallback(Action<AssetCat> onLoadFailCallback, object callbackCause = null)
		{
			if (onLoadFailCallback == null)
				return;

			this.onLoadFailCallback += onLoadFailCallback;
			if (callbackCause == null)
				callbackCause = this;
			onLoadFailCallbackListDict.Add(callbackCause, onLoadFailCallback);
		}

		public void AddOnLoadDoneCallback(Action<AssetCat> onLoadDoneCallback, object callbackCause = null)
		{
			if (onLoadDoneCallback == null)
				return;
			this.onLoadDoneCallback += onLoadDoneCallback;
			if (callbackCause == null)
				callbackCause = this;
			onLoadDoneCallbackListDict.Add(callbackCause, onLoadDoneCallback);
		}


		public void RemoveOnLoadSuccessCallback(Action<AssetCat> onLoadSuccessCallback, object callbackCause = null)
		{
			if (onLoadSuccessCallback == null)
				return;

			this.onLoadSuccessCallback -= onLoadSuccessCallback;
			if (callbackCause == null)
				callbackCause = this;
			onLoadSuccessCallbackListDict.Remove(callbackCause, onLoadSuccessCallback);
		}

		public void RemoveOnLoadFailCallback(Action<AssetCat> onLoadFailCallback, object callbackCause = null)
		{
			if (onLoadFailCallback == null)
				return;

			this.onLoadFailCallback -= onLoadFailCallback;
			if (callbackCause == null)
				callbackCause = this;
			onLoadFailCallbackListDict.Remove(callbackCause, onLoadFailCallback);
		}

		public void RemoveOnLoadDoneCallback(Action<AssetCat> onLoadDoneCallback, object callbackCause = null)
		{
			if (onLoadDoneCallback == null)
				return;

			this.onLoadDoneCallback -= onLoadDoneCallback;
			if (callbackCause == null)
				callbackCause = this;
			onLoadDoneCallbackListDict.Remove(callbackCause, onLoadDoneCallback);
		}

		public void RemoveOnLoadSuccessCallback(object callbackCause = null)
		{
			if (callbackCause == null)
				callbackCause = this;
			onLoadSuccessCallbackListDict.Foreach(callbackCause,
				callback => { this.onLoadSuccessCallback -= callback; });
			onLoadSuccessCallbackListDict.Remove(callbackCause);
		}

		public void RemoveOnLoadFailCallback(object callbackCause = null)
		{
			if (callbackCause == null)
				callbackCause = this;
			onLoadFailCallbackListDict.Foreach(callbackCause,
				callback => { this.onLoadFailCallback -= callback; });
			onLoadFailCallbackListDict.Remove(callbackCause);
		}

		public void RemoveOnLoadDoneCallback(object callbackCause = null)
		{
			if (callbackCause == null)
				callbackCause = this;
			onLoadDoneCallbackListDict.Foreach(callbackCause,
				callback => { this.onLoadDoneCallback -= callback; });
			onLoadDoneCallbackListDict.Remove(callbackCause);
		}

		public void RemoveAllOnLoadSuccessCallback()
		{
			onLoadSuccessCallbackListDict.Clear();
			this.onLoadSuccessCallback = null;
		}

		public void RemoveAllOnLoadFailCallback()
		{
			onLoadFailCallbackListDict.Clear();
			this.onLoadFailCallback = null;
		}

		public void RemoveAllOnLoadDoneCallback()
		{
			onLoadDoneCallbackListDict.Clear();
			this.onLoadDoneCallback = null;
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
					onLoadSuccessCallback += asset_cat =>
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
				var mat = renderers[j].sharedMaterial;
				if (mat == null)
					continue;
				HandleAssetShader_Mat(ref mat);
			}
		}

#endif
	}
}