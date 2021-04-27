using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public class UIPanel : UIObject
  {
    private int _sortingOrder = int.MinValue;

    /// <summary>
    /// 是否是常驻的,即不被销毁
    /// </summary>
    public virtual bool is_resident => false;
    public virtual EUILayerName layerName => EUILayerName.PopUpUILayer;
    public UILayer uiLayer => Client.instance.uiManager.uiLayerManager.uiLayer_dict[layerName];
    protected Transform frame_transform => this.cache.GetOrAddDefault("frameTransform", () => this.graphicComponent.gameObject.transform.Find("frame"));
    protected Transform content_transform => this.cache.GetOrAddDefault("contentTransform", () => frame_transform.Find("content"));
    protected Canvas canvas => this.cache.GetOrAddDefault("canvas", () => graphicComponent.gameObject.GetOrAddComponent<Canvas>());
    public int sortingOrder
    {
      get => this._sortingOrder;
      set
      {
        if (_sortingOrder == value)
          return;
        _sortingOrder = value;
        OnSortingOrderChange();
      }
    }

    public virtual bool is_hide_blackMaskBehide
    {
      get => false;
    }

    protected virtual void OnSortingOrderChange()
    {
      if (graphicComponent.gameObject == null)
        return;
      canvas.sortingOrder = sortingOrder;
    }

    public void SetToTop()
    {
      this.uiLayer.SetPanelToTop(this);
    }

    public void SetToBottom()
    {
      this.uiLayer.SetPanelToBottom(this);
    }

    public void SetPanelIndex(int new_index)
    {
      this.uiLayer.SetPanelIndex(this, new_index);
    }


    public override void OnAllAssetsLoadDone()
    {
      //    LogCat.LogWarning(prefabPath);
      base.OnAllAssetsLoadDone();
      canvas.overrideSorting = true;
      canvas.sortingLayerName = "UI";
      graphicComponent.gameObject.GetOrAddComponent<GraphicRaycaster>();
      OnSortingOrderChange();
    }

    public void OnInitPanel(Transform parent_transform)
    {
      graphicComponent.SetParentTransform(parent_transform == null ? uiLayer.graphicComponent.transform : parent_transform);
      this.uiLayer.AddPanel(this);
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      _sortingOrder = int.MinValue;
    }


    public virtual void Close()
    {
      this.uiLayer.RemovePanel(this);
      this.parent_uiObject.CloseChildPanel(this.key);
    }

  }

}


