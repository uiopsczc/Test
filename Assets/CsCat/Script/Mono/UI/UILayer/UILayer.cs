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

    public void Init(GameObject gameObject, UILayerConfig uiLayerConfig)
    {
      base.Init();
      this.uiLayerConfig = uiLayerConfig;
      this.graphicComponent.SetGameObject(gameObject, true);
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
        int sortingOrder = uiLayerConfig.order_in_layer + (i+1) * UILayerConst.Order_Per_Panel;
        
        if (panel is UIPanel uiPanel)
        {
          uiPanel.sortingOrder = sortingOrder;

          if (uiLayerConfig.uiLayerRule.IsHideLowerOrderUI())
            uiPanel.graphicComponent.SetIsShow(i== panel_list.Count-1);

        }
        else//lua UIPanel
        {
          LuaTable panel_luaTable = (LuaTable) panel;
          panel_luaTable.InvokeAction("SetSortingOrder", sortingOrder);

          if (uiLayerConfig.uiLayerRule.IsHideLowerOrderUI())
            panel_luaTable.InvokeAction("graphicComponent.SetIsShow", i == panel_list.Count - 1);
        }
      }


      if (uiLayerConfig.uiLayerRule.IsHideBackgroundUILayer())
        this.Broadcast(null, UIEventNameConst.SetIsHideUILayer, EUILayerName.BackgroundUILayer, panel_list.Count > 0);
      if(uiLayerConfig.uiLayerRule.IsHideFrontUILayer())
        this.Broadcast(null, UIEventNameConst.SetIsHideUILayer, EUILayerName.FrontUILayer, panel_list.Count > 0);
      if(uiLayerConfig.uiLayerRule.IsAddBlackMaskBehide())
        HandleLayerAddBlackMaskBehide();
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


    void HandleLayerAddBlackMaskBehide()
    {
      object target_panel = null;
      int target_panel_sorttingOrder = 0;
      for (int i = EnumUtil.GetCount<EUILayerName>() - 1; i >= 0; i--)
      {
        EUILayerName uiLayerName = (EUILayerName)i;
        var uiLayer = Client.instance.uiManager.uiLayerManager.GetUILayer(uiLayerName);
        if (uiLayer.graphicComponent.IsShow()&& uiLayer.uiLayerConfig.uiLayerRule.IsAddBlackMaskBehide())
        {
          for (int j = uiLayer.panel_list.Count - 1; j >= 0; j--)
          {
            var panel = uiLayer.panel_list[j];
            if (panel is UIPanel ui_panel)
            {
              if (!ui_panel.is_hide_blackMaskBehide)
              {
                target_panel = ui_panel;
                target_panel_sorttingOrder = ui_panel.sortingOrder;
                break;
              }
            }
            else
            {
              LuaTable panel_luaTable = (LuaTable)panel;
              if (!panel_luaTable.InvokeFunc<bool>("IsHideBlackMaskBehide"))
              {
                target_panel = panel;
                target_panel_sorttingOrder = panel_luaTable.InvokeFunc<int>("GetSortingOrder");
                break;
              }
            }
          }
        }
      }

      if (target_panel == null)
        this.Broadcast(null, UIEventNameConst.HideUIBlackMask);
      else
        this.Broadcast(null, UIEventNameConst.ShowUIBlackMask, target_panel_sorttingOrder, target_panel);
    }





  }
}