using System;
using UnityEngine;

namespace CsCat
{
	public partial class GraphicComponent
	{
		public GameObject prefab;
		protected string _prefabPath = null;
		public AssetCat prefabAssetCat;
		private bool isLoadDone;


		public string prefabPath => _prefabPath;


		public void SetPrefabPath(string prefabPath)
		{
			this._prefabPath = prefabPath;
			isLoadDone = prefabPath == null;
		}

		public void LoadPrefabPath()
		{
			if (!this.prefabPath.IsNullOrWhiteSpace())
				this.prefabAssetCat = resLoadComponentPlugin.GetOrLoadAsset(prefabPath, null, null,
					assetCat => { isLoadDone = true; }, this);
		}

		public bool IsLoadDone()
		{
			return this.isLoadDone;
		}

		public virtual void OnAllAssetsLoadDone()
		{
			if (!prefabPath.IsNullOrWhiteSpace())
			{
				GameObject prefab = prefabAssetCat.Get<GameObject>();
				GameObject clone = InstantiateGameObject(prefab);
				clone.name = prefab.name;
				clone.transform.CopyFrom(prefab.transform);
				SetGameObject(clone, null);
			}

			if (this.parentTransform != null)
				SetParentTransform(this.parentTransform);
		}
	}
}