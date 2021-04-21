
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CsCat
{
  public class StageBase : TickObject
  {
    public LoadSceneMode loadSceneMode = LoadSceneMode.Additive;
    public List<UIPanel> panel_list = new List<UIPanel>();


    public virtual bool is_show_fade => false;
    public virtual bool is_show_loading => true;
    public virtual string stage_name { get; }
    public virtual string scene_path { get; }
    public string scene_name => scene_path.WithoutSuffix().FileName();


    public Action on_show_callback;


    public virtual void LoadPanels()
    {
    }



    public override void Start()
    {
      base.Start();
      StartCoroutine(StartLoading(), this.GetType().ToString());
    }

    public IEnumerator StartLoading()
    {
      float last_pct = 0;
      SetLoadingPct(last_pct);


      LoadPanels();
      yield return WaitUntilAllPanelsLoadDone();
      last_pct = 0.1f;
      SetLoadingPct(last_pct);


      if (!scene_path.IsNullOrWhiteSpace())
        yield return SceneManager.LoadSceneAsync(scene_path, loadSceneMode);
      last_pct = 0.2f;
      SetLoadingPct(last_pct);


      yield return WaitUntilPreLoadAssetsLoadDone((pct) =>
      {
        SetLoadingPct(last_pct + Mathf.Lerp(pct, 0, 0.9f - last_pct));
      });


      yield return WaitUntilAllAssetsLoadDone();
      SetLoadingPct(1);


      yield return new WaitForSeconds(0.05f);
      yield return IEPreShow();
      HideLoading();


      this.Broadcast(StageEventNameConst.On_Stage_Loaded, this);
      Show();

    }

    public virtual IEnumerator IEPreShow()
    {
      yield return null;
    }

    public virtual void Show()
    {
      on_show_callback?.Invoke();
    }

    //////////////////////////////////////////////////////////////////////
    // Fade
    //////////////////////////////////////////////////////////////////////
    public void HideFade()
    {
      if (Client.instance.uiManager.uiFadePanel.graphicComponent.gameObject.activeInHierarchy)
        Client.instance.uiManager.FadeTo(0, FadeConst.Stage_Fade_Default_Hide_Duration,
          () => { Client.instance.uiManager.HideFade(); });
    }

    //////////////////////////////////////////////////////////////////////
    // Loading
    //////////////////////////////////////////////////////////////////////
    public void SetLoadingPct(float pct)
    {
      if (is_show_loading)
        Client.instance.uiManager.SetLoadingPct(pct);
    }

    public void HideLoading()
    {
      if (is_show_loading)
        Client.instance.uiManager.HideLoading();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////
    public IEnumerator WaitUntilPreLoadAssetsLoadDone(Action<float> callback)
    {
      var assetAsyncloader_prosessing_list = Client.instance.assetBundleManager.assetAsyncloader_prosessing_list;
      var assetBundleAsyncLoader_prosessing_list =
        Client.instance.assetBundleManager.assetBundleAsyncLoader_prosessing_list;
      float total_loading_count = assetAsyncloader_prosessing_list.Count + assetBundleAsyncLoader_prosessing_list.Count;
      float cur_pct = 0;
      float next_pct = 0;
      while (assetAsyncloader_prosessing_list.Count > 0 || assetBundleAsyncLoader_prosessing_list.Count > 0)
      {
        cur_pct = (assetAsyncloader_prosessing_list.Count + assetBundleAsyncLoader_prosessing_list.Count) /
                  total_loading_count;
        if (cur_pct > next_pct)
          next_pct = cur_pct;
        callback(cur_pct);
        yield return null;
      }

      callback(1);
    }

    public IEnumerator WaitUntilAllAssetsLoadDone()
    {
      yield return new WaitUntil(() =>
      {
        if (is_all_assets_load_done)
          return true;
        return false;
      });
    }


    public IEnumerator WaitUntilAllPanelsLoadDone()
    {
      yield return new WaitUntil(() =>
      {
        foreach (UIPanel panel in panel_list)
        {
          if (!panel.is_all_assets_load_done)
            return false;
        }

        return true;
      });
    }

    public override void Init()
    {
      base.Init();
    }

    public IEnumerator IEPreDestroy()
    {
      Client.instance.uiManager.Reset();
      PoolCatManager.instance.Trim();//清理所有的对象池
      if (!scene_path.IsNullOrWhiteSpace())
      {
        yield return SceneManager.UnloadSceneAsync(scene_name);
      }

      yield return null;
    }


  }
}



