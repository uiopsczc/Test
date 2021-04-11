using System;
using UnityEngine;

namespace CsCat
{
  public partial class GraphicComponent
  {
    public GameObject prefab;
    protected string _prefab_path = null;
    public AssetCat prefab_assetCat;
    private bool is_load_done;


    public string prefab_path { get => _prefab_path; }
    

    public void SetPrefabPath(string prefab_path)
    {
      this._prefab_path = prefab_path;
      is_load_done = prefab_path == null;
    }

    public void LoadPrefabPath()
    {
      if (!this.prefab_path.IsNullOrWhiteSpace())
        this.prefab_assetCat = resLoadComponentPlugin.GetOrLoadAsset(prefab_path, null ,null,
          assetCat => { is_load_done = true;},this);
    }

    public bool IsLoadDone()
    {
      return this.is_load_done;
    }

    public virtual void OnAllAssetsLoadDone()
    {
      if (!prefab_path.IsNullOrWhiteSpace())
      {
        GameObject prefab = prefab_assetCat.Get<GameObject>();
        GameObject clone = InstantiateGameObject(prefab);
        clone.name = prefab.name;
        clone.transform.CopyFrom(prefab.transform);
        SetGameObject(clone,null);
      }

      if (this.parent_transform != null)
        SetParentTransform(this.parent_transform);
    }
  }
}