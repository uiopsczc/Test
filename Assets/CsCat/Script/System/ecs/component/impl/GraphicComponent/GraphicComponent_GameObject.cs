using System;
using UnityEngine;

namespace CsCat
{
  public partial class GraphicComponent
  {
    public Transform parent_transform;
    public GameObject gameObject;
    private bool is_hide = false;
    private bool is_not_destroy_gameObject;


    public Transform transform
    {
      get { return cache.GetOrAddDefault(() => { return this.gameObject.transform; }); }
    }

    public RectTransform rectTransform
    {
      get { return cache.GetOrAddDefault(() => { return this.gameObject.GetComponent<RectTransform>(); }); }
    }


    public void SetParentTransform(Transform parent_transform)
    {
      this.parent_transform = parent_transform;
      if (this.gameObject != null)
        this.transform.SetParent(this.parent_transform, !LayerMask.LayerToName(this.gameObject.layer).Equals("UI"));
    }


    public virtual GameObject InstantiateGameObject(GameObject prefab)
    {
      return GameObject.Instantiate(prefab);
    }

    public virtual void SetIsShow(bool is_show)
    {
      this.is_hide = !is_show;
      if (this.gameObject != null)
        this.gameObject.SetActive(!this.is_hide);
    }

    protected virtual void InitGameObjectChildren()
    {
      this.GetEntity<GameEntity>().InitGameObjectChildren();
    }


    public virtual void SetGameObject(GameObject gameObject, bool? is_not_destroy_gameObject)
    {
      this.cache.Remove2(typeof(Transform).ToString());
      this.cache.Remove2(typeof(RectTransform).ToString());
      this.gameObject = gameObject;
      if (gameObject == null)
        return;
      if (is_not_destroy_gameObject != null)
        this.is_not_destroy_gameObject = is_not_destroy_gameObject.Value;
      InitGameObjectChildren();
      SetIsShow(gameObject.activeSelf);
    }


    public virtual void DestroyGameObject()
    {
      if (this.gameObject != null && !is_not_destroy_gameObject)
        gameObject.Destroy();
    }
  }
}