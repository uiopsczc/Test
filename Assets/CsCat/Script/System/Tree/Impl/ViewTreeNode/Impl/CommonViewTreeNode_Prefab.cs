using System;
using UnityEngine;

namespace CsCat
{
	public partial class CommonViewTreeNode
	{
		protected string _prefabPath;
		public AssetCat _prefabAssetCat;
		private bool _isPostPrefabLoad;
		private Action _postPrefabLoadCallback;

		public string prefabPath => _prefabPath;
		
		public void SetPrefabPath(string prefabPath)
		{
			this._prefabPath = prefabPath;
			_isPostPrefabLoad = prefabPath == null;
		}

		private void _LoadPrefabPath()
		{
			if (!this.prefabPath.IsNullOrWhiteSpace())
			{
				this._prefabAssetCat = this.GetChild<ResLoadDictTreeNode>().GetOrLoadAsset(prefabPath, null, null,
					assetCat =>
					{
						_isPostPrefabLoad = true;
						_PostPrefabLoad();
						_postPrefabLoadCallback?.Invoke();
					}, this);
			}
		}

		protected bool _IsPostPrefabLoad()
		{
			return this._isPostPrefabLoad;
		}

		public void InvokePostPrefabLoad(Action postPrefabLoadCallback)
		{
			if (this._isPostPrefabLoad)
				postPrefabLoadCallback();
			else
				_postPrefabLoadCallback = postPrefabLoadCallback;
		}

		protected virtual void _PostPrefabLoad()
		{
			GameObject prefab = _prefabAssetCat.Get<GameObject>();
			GameObject clone = _DoInstantiateGameObject(prefab);
			clone.name = prefab.name;
			Transform transform = clone.transform;
			transform.CopyFrom(prefab.transform);
			DoSetGameObject(clone, null);
		}


		private void _Reset_Prefab()
		{
			_prefabPath = null;
			_prefabAssetCat = null;
			_isPostPrefabLoad = false;
			_postPrefabLoadCallback = null;
		}

		protected void _Destroy_Prefab()
		{
			_prefabPath = null;
			_prefabAssetCat = null;
			_isPostPrefabLoad = false;
			_postPrefabLoadCallback = null;
		}
	}
}