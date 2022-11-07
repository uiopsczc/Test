using UnityEngine;

namespace CsCat
{
	public partial class CommonViewComponent
	{
		protected string _prefabPath = null;
		public AssetCat _prefabAssetCat;
		private bool _isLoadDone;


		public string prefabPath => _prefabPath;


		public void SetPrefabPath(string prefabPath)
		{
			this._prefabPath = prefabPath;
			_isLoadDone = prefabPath == null;
		}

		public void LoadPrefabPath()
		{
			if (!this.prefabPath.IsNullOrWhiteSpace())
				this._prefabAssetCat = this.GetComponent<ResLoadDictComponent>().GetOrLoadAsset(prefabPath, null, null,
					assetCat => { _isLoadDone = true; }, this);
		}

		public bool IsLoadDone()
		{
			return this._isLoadDone;
		}

		public virtual void OnAllAssetsLoadDone()
		{
			if (!prefabPath.IsNullOrWhiteSpace())
			{
				GameObject prefab = _prefabAssetCat.Get<GameObject>();
				GameObject clone = InstantiateGameObject(prefab);
				clone.name = prefab.name;
				SetParentTransform(this._parentTransform);
				Transform transform = clone.transform;
				transform.CopyFrom(prefab.transform);
				SetGameObject(clone, null);
				return;
			}
			SetParentTransform(this._parentTransform);
		}
	}
}