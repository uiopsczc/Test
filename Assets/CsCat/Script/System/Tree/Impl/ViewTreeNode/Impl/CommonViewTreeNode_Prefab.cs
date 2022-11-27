using System;
using UnityEngine;

namespace CsCat
{
	public partial class CommonViewTreeNode
	{
		protected string _prefabPath;
		public AssetCat _prefabAssetCat;
		private bool _isPrefabLoadDone;
		private Action _prefabLoadDoneCallback;

		public string prefabPath => _prefabPath;
		
		public void SetPrefabPath(string prefabPath)
		{
			this._prefabPath = prefabPath;
			_isPrefabLoadDone = prefabPath == null;
		}

		private void _LoadPrefabPath()
		{
			if (!this.prefabPath.IsNullOrWhiteSpace())
			{
				this._prefabAssetCat = this.GetChild<ResLoadDictTreeNode>().GetOrLoadAsset(prefabPath, null, null,
					assetCat =>
					{
						OnPrefabLoadDone();
						_prefabLoadDoneCallback?.Invoke();
						PostPrefabLoadDone();
					}, this);
			}
		}

		protected virtual void PostPrefabLoadDone()
		{
		}

		protected bool IsPrefabLoadDone()
		{
			return this._isPrefabLoadDone;
		}

		public void InvokeAfterPrefabLoadDone(Action callback)
		{
			if (this._isPrefabLoadDone)
				callback();
			else
				_prefabLoadDoneCallback = callback;
		}

		protected virtual void OnPrefabLoadDone()
		{
			_isPrefabLoadDone = true;
			GameObject prefab = _prefabAssetCat.Get<GameObject>();
			GameObject clone = InstantiateGameObject(prefab);
			clone.name = prefab.name;
			Transform transform = clone.transform;
			transform.CopyFrom(prefab.transform);
			SetGameObject(clone, null);
			this._OnInstantiateGameObject();
		}


		private void _Reset_Prefab()
		{
			_prefabPath = null;
			_prefabAssetCat = null;
			_isPrefabLoadDone = false;
			_prefabLoadDoneCallback = null;
		}

		protected void _Destroy_Prefab()
		{
			_prefabPath = null;
			_prefabAssetCat = null;
			_isPrefabLoadDone = false;
			_prefabLoadDoneCallback = null;
		}
	}
}