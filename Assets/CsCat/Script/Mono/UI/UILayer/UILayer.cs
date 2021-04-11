using System.Collections.Generic;
using UnityEngine;
using XLua;
using RectTransform = UnityEngine.RectTransform;

namespace CsCat
{
  public class UILayer : UIObject
  {
    public UILayerConfig uiLayerConfig;
    public List<object> panel_list = new List<object>();
    private LuaFunction SetSortingOrder;

    public void Init(GameObject gameObject, UILayerConfig uiLayerConfig)
    {
      base.Init();
      this.uiLayerConfig = uiLayerConfig;
      this.graphicComponent.SetGameObject(gameObject,true);
      gameObject.name = uiLayerConfig.name.ToString();
      gameObject.layer = LayerMask.NameToLayer("UI");

      RectTransform rectTransform = graphicComponent.gameObject.GetOrAddComponent<RectTransform>();
      rectTransform.anchorMin = new Vector2(0, 0);
      rectTransform.anchorMax = new Vector2(1, 1);
      rectTransform.sizeDelta = new Vector2(0, 0);
      rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
      rectTransform.localScale = new Vector3(1, 1, 1);
    }

    public override void Refresh()
    {
      base.Refresh();
      for (var i = 0; i < panel_list.Count; i++)
      {
        object panel = panel_list[i];
        int sortingOrder = uiLayerConfig.order_in_layer + i * UILayerConst.Order_Per_Panel;
        if (panel is UIPanel uiPanel)
          uiPanel.sortingOrder = sortingOrder;
        else//lua UIPanel
        {
          ((LuaTable)panel).Get("SetSortingOrder", out SetSortingOrder);
          SetSortingOrder.Action(panel, sortingOrder);
          SetSortingOrder.Dispose();
        }
      }
      SetSortingOrder = null;
    }

    public void RemovePanel(object panel)
    {
      var index = this.panel_list.IndexOf(panel);
      if (index == -1)
        return;
      this.panel_list.RemoveAt(index);
      Refresh();
    }

    public void AddPanel(object panel)
    {
      panel_list.Add(panel);
      Refresh();
    }

    public void SetPanelIndex(object panel, int new_index)
    {
      var index = this.panel_list.IndexOf(panel);
      if (index == -1 || index == new_index)
        return;
      this.panel_list.Insert(new_index, panel);
      if (index < new_index)
        this.panel_list.RemoveAt(index);
      else
        this.panel_list.RemoveAt(index + 1);
      Refresh();
    }

    public void SetPanelToTop(object panel)
    {
      var top_index = panel_list.Count;
      SetPanelIndex(panel, top_index);
    }

    public void SetPanelToBottom(object panel)
    {
      SetPanelIndex(panel, 0);
    }
  }
}