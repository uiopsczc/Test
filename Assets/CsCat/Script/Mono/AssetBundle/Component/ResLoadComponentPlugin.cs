using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public class ResLoadComponentPlugin
	{
		private ResLoadComponent resLoadComponent;

		public Dictionary<AssetCat, Dictionary<object, bool>> assetCatDict =
			new Dictionary<AssetCat, Dictionary<object, bool>>();

		public ResLoadComponentPlugin(ResLoadComponent resLoadComponent)
		{
			this.resLoadComponent = resLoadComponent;
		}

		public bool IsAllLoadDone()
		{
			foreach (var keyValue in assetCatDict)
			{
				var assetCat = keyValue.Key;
				if (!assetCat.IsLoadDone())
					return false;
			}

			return true;
		}

		public IEnumerator IEIsAllLoadDone(Action onAllLoadDoneCallback = null)
		{
			yield return new WaitUntil(() => IsAllLoadDone());
			onAllLoadDoneCallback();
		}

		public void CheckIsAllLoadDone(Action onAllLoadDoneCallback = null)
		{
			resLoadComponent.StartCoroutine(IEIsAllLoadDone(onAllLoadDoneCallback));
		}


		// 加载某个资源
		public AssetCat GetOrLoadAsset(string assetPath, Action<AssetCat> onLoadSuccessCallback = null,
			Action<AssetCat> onLoadFailCallback = null, Action<AssetCat> onLoadDoneCallback = null,
			object callbackCause = null)
		{
			var assetCat = resLoadComponent.GetOrLoadAsset(assetPath, onLoadSuccessCallback, onLoadFailCallback,
				onLoadDoneCallback, callbackCause);
			__AddToAssetCatDict(assetCat, callbackCause);
			return assetCat;
		}


		public void CancelLoadCallback(AssetCat assetCat, object callbackCause = null)
		{
			this.resLoadComponent.CancelLoadCallback(assetCat, callbackCause);
			__RemoveFromAssetCatDict(assetCat, callbackCause);
		}

		public void CancelLoadAllCallback(AssetCat assetCat)
		{
			if (!assetCatDict.ContainsKey(assetCat))
				return;
			foreach (var keyValue1 in assetCatDict)
			{
				var callbackCauseDict = keyValue1.Value;
				foreach (var keyValue2 in callbackCauseDict)
				{
					var callbackCause = keyValue2.Key;
					resLoadComponent.CancelLoadCallback(assetCat, callbackCause.GetNullableKey());
				}
			}

			assetCatDict.Remove(assetCat);
		}


		void __AddToAssetCatDict(AssetCat assetCat, object callbackCause)
		{
			assetCatDict.GetOrAddDefault(assetCat, () => new Dictionary<object, bool>())[callbackCause.GetNotNullKey()]
				=
				true;
		}

		void __RemoveFromAssetCatDict(AssetCat assetCat, object callbakCause)
		{
			if (!assetCatDict.ContainsKey(assetCat))
				return;
			assetCatDict[assetCat].Remove(callbakCause.GetNotNullKey());
		}

		public void Destroy()
		{
			foreach (var keyValue1 in assetCatDict)
			{
				var assetCat = keyValue1.Key;
				foreach (var keyValue2 in assetCatDict[assetCat])
				{
					var callbackCause = keyValue2.Key;
					resLoadComponent.CancelLoadCallback(assetCat, callbackCause.GetNullableKey());
				}
			}

			assetCatDict.Clear();
			resLoadComponent = null;
		}
	}
}