using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class ResLoadDict
	{
		public Dictionary<AssetCat, Dictionary<object, bool>> assetCatDict =
		  new Dictionary<AssetCat, Dictionary<object, bool>>();

		private ResLoad _resLoad;

		public ResLoadDict(ResLoad resLoad)
		{
			this._resLoad = resLoad;
		}

		public bool IsAllLoadDone()
		{
			foreach (var kv in assetCatDict)
			{
				var assetCat = kv.Key;
				if (!assetCat.IsLoadDone())
					return false;
			}
			return true;
		}

		public IEnumerator IEIsAllLoadDone(Action onAllLoadDoneCallback = null)
		{
			yield return new WaitUntil(() => { return IsAllLoadDone(); });
			onAllLoadDoneCallback?.Invoke();
		}


		// 加载某个资源
		public AssetCat GetOrLoadAsset(string assetPath, Action<AssetCat> onLoadSuccessCallback = null,
		  Action<AssetCat> onLoadFailCallback = null,
		  Action<AssetCat> onLoadDoneCallback = null, object callbackCause = null)
		{
			var assetCat = GetOrLoadAsset(assetPath, onLoadSuccessCallback, onLoadFailCallback, onLoadDoneCallback,
			  callbackCause);
			if (!this.assetCatDict.ContainsKey(assetCat))
				this.assetCatDict[assetCat] = new Dictionary<object, bool>();
			this.assetCatDict[assetCat][callbackCause.GetNotNullKey()] = true;
			return assetCat;
		}


		public void CancelLoadCallback(AssetCat assetCat, object callbackCause = null)
		{
			this.assetCatDict[assetCat].Remove(callbackCause.GetNotNullKey());
			if (this.assetCatDict[assetCat].Count == 0)
				this.assetCatDict.Remove(assetCat);
			this._resLoad.CancelLoadCallback(assetCat, callbackCause);
		}

		public void CancelLoadAllCallback(AssetCat assetCat)
		{
			foreach (var keyValue in assetCatDict[assetCat])
			{
				var callbackCause = keyValue.Value;
				if (callbackCause.Equals(NullUtil.GetDefaultString()))
					this._resLoad.CancelLoadCallback(assetCat, null);
				else
					this._resLoad.CancelLoadCallback(assetCat, callbackCause);
			}
			assetCatDict.Remove(assetCat);
		}


		public void Reset()
		{
			foreach (var keyValue1 in assetCatDict)
			{
				var assetCat = keyValue1.Key;
				foreach (var keyValue2 in assetCatDict[assetCat])
				{
					var callbackCause = keyValue2.Value;
					if (callbackCause.Equals(NullUtil.GetDefaultString()))
						this._resLoad.CancelLoadCallback(assetCat, null);
					else
						this._resLoad.CancelLoadCallback(assetCat, callbackCause);
				}
			}

			assetCatDict.Clear();
		}

		public void Destroy()
		{
			Reset();
		}
	}
}