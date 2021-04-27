using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class UIManager : UIObject
  {
    public UILayerManager uiLayerManager;
    public UINotifyManager uiNotifyManager;
    public UIWaitingPanel uiWaitingPanel;
    public UILoadingPanel uiLoadingPanel;
    public UILanternNotifyPanel uiLanternNotifyPanel;
    public UIShowLogoPanel uiShowLogoPanel;
    public UIFadePanel uiFadePanel;
    public UIBloodManager uiBloodManager;
    public UIBlackMaskPanel uiBlackMaskPanel;


    public Camera uiCamera => cache.GetOrAddDefault("uiCamera",
      () => GameObject.Find(UIConst.UICamera_Path).GetComponent<Camera>());
    public Canvas uiCanvas => cache.GetOrAddDefault("uiCanvas",
      () => GameObject.Find(UIConst.UICanvas_Path).GetComponent<Canvas>());
    public RectTransform uiCanvas_rectTransform => cache.GetOrAddDefault("uiCanvas_rectTransform",
      () => uiCanvas.GetComponent<RectTransform>());



    public override void Init()
    {
      base.Init();
      var gameObject = GameObject.Find(UIConst.UIManager_Path);
      graphicComponent.SetGameObject(gameObject, true);
      if (Application.isPlaying)
        MonoBehaviour.DontDestroyOnLoad(graphicComponent.gameObject);
    }

    public override void PostInit()
    {
      uiLayerManager = this.AddChild<UILayerManager>("UILayerManager", this);
      InitListeners();
      InitPanels();
      uiNotifyManager = this.AddChild<UINotifyManager>("UINotifyManager");
      uiBloodManager = this.AddChild<UIBloodManager>("UIBloodManager");
      base.PostInit();
    }

    void InitPanels()
    {
      uiBlackMaskPanel = CreateChildPanel("UIBlackMaskPanel", default(UIBlackMaskPanel), null,
        GameObject.Find("UIManager/UICanvas/BlackMaskUILayer/UIBlackMaskPanel"));

      uiShowLogoPanel = CreateChildPanel("UIShowLogoPanel", default(UIShowLogoPanel), null,
        GameObject.Find("UIManager/UICanvas/BackgroundUILayer/UIShowLogoPanel"));

      uiLoadingPanel = CreateChildPanel("UILoadingPanel", default(UILoadingPanel), null,
        GameObject.Find("UIManager/UICanvas/LoadingUILayer/UILoadingPanel"));

      uiFadePanel = CreateChildPanel("UIFadePanel", default(UIFadePanel), null,
        GameObject.Find("UIManager/UICanvas/FadeUILayer/UIFadePanel"));

      uiWaitingPanel = CreateChildPanel("UIWaitingPanel", default(UIWaitingPanel), null,
        GameObject.Find("UIManager/UICanvas/WaitingUILayer/UIWaitingPanel"));
      
      uiLanternNotifyPanel = CreateChildPanel("UILanternNotifyPanel", default(UILanternNotifyPanel), null,
        GameObject.Find("UIManager/UICanvas/NotifyUILayer/UILanternNotifyPanel"));
    }



    void InitListeners()
    {

    }

    protected override void __Reset()
    {
      base.__Reset();
      this.CloseAllChildPanels(true);
    }

    //////////////////////////////////////////////////////////////////////
    // UIBlood
    //////////////////////////////////////////////////////////////////////
    public UIBlood AddUIBlood(Transform parent_transform, float max_value, int? slider_count, float? to_value, List<Color> slider_color_list = null)
    {
      return this.uiBloodManager.AddUIBlood(parent_transform, max_value, slider_count, to_value, slider_color_list);
    }

    public void RemoveUIBlood(UIBlood uiBlood)
    {
      this.uiBloodManager.RemoveChild(uiBlood.key);
    }

    //////////////////////////////////////////////////////////////////////
    // Notify
    //////////////////////////////////////////////////////////////////////
    public void Notify(string desc, Transform parent_transform = null, bool is_add_to_child_panel_stack = false)
    {
      uiNotifyManager.Notify(desc, parent_transform, is_add_to_child_panel_stack);
    }

    public void LanternNotify(string desc)
    {
      uiNotifyManager.LanternNotify(desc);
    }

    //////////////////////////////////////////////////////////////////////
    // Fade
    //////////////////////////////////////////////////////////////////////
    public void HideFade()
    {
      uiFadePanel.HideFade();
    }

    public void FadeInOut(float duration, Action callback)
    {
      uiFadePanel.FadeInOut(duration, callback);
    }

    public void FadeTo(float toAplha, float duration, Action callback = null)
    {
      uiFadePanel.FadeTo(toAplha, duration, callback);
    }

    public void FadeTo(float fromAplha, float toAplha, float duration, Action callback = null)
    {
      uiFadePanel.FadeTo(fromAplha, toAplha, duration, callback);
    }

    //////////////////////////////////////////////////////////////////////
    // Loading
    //////////////////////////////////////////////////////////////////////
    public void SetLoadingPct(float pct)
    {
      this.uiLoadingPanel.SetPct(pct);
    }

    public void HideLoading()
    {
      uiLoadingPanel.HideLoading();
    }

    //////////////////////////////////////////////////////////////////////
    // Waiting
    //////////////////////////////////////////////////////////////////////
    public void StartWaiting()
    {
      uiWaitingPanel.StartWaiting();
    }

    public void EndWaiting()
    {
      uiWaitingPanel.EndWaiting();
    }

    public void HideWaiting()
    {
      uiWaitingPanel.HideWaiting();
    }
  }
}




