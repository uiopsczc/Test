using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public class ResLoadDictTreeNode : TreeNode
	{
		private ResLoadDict _resLoadDict;
		

		protected void _Init(ResLoadDict resLoadDict)
		{
			base._Init();
			this._resLoadDict = resLoadDict;
		}

		public bool IsAllLoadDone()
		{
			return this._resLoadDict.IsAllLoadDone();
		}

		public IEnumerator IEIsAllLoadDone(Action onAllLoadDoneCallback = null)
		{
			yield return this._resLoadDict.IEIsAllLoadDone(onAllLoadDoneCallback);
		}

		public bool IsLoadDone(string assetPath)
		{
			return this._resLoadDict.IsLoadDone(assetPath);
		}

		public IEnumerator IEIsLoadDone(string assetPath, Action onLoadDoneCallback = null)
		{
			return this._resLoadDict.IEIsLoadDone(assetPath, onLoadDoneCallback);
		}


		// 加载某个资源
		public AssetCat GetOrLoadAsset(string assetPath, Action<AssetCat> onLoadSuccessCallback = null,
		  Action<AssetCat> onLoadFailCallback = null,
		  Action<AssetCat> onLoadDoneCallback = null, object callbackCause = null)
		{
			return this._resLoadDict.GetOrLoadAsset(assetPath,onLoadSuccessCallback, onLoadFailCallback, onLoadDoneCallback, callbackCause);
		}


		public void CancelLoadCallback(AssetCat assetCat, object callbackCause = null)
		{
			this._resLoadDict.CancelLoadCallback(assetCat, callbackCause);
		}

		public void CancelLoadAllCallback(AssetCat assetCat)
		{
			this._resLoadDict.CancelLoadCallback(assetCat);
		}


		protected  override  void _Reset()
		{
			base._Reset();
			this._resLoadDict.Reset();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			this._resLoadDict.Reset();
		}
	}
}