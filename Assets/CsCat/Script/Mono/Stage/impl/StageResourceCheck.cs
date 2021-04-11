using System.Collections;
using UnityEngine;

namespace CsCat
{
  public class StageResourceCheck : StageBase
  {
    public override bool is_show_fade => true;
    public override bool is_show_loading => false;
    public override string stage_name => "StageResourceCheck";


    public override void Show()
    {
      base.Show();
      StartCoroutine(IEResourceCheck());
    }

    private IEnumerator IEResourceCheck()
    {
      if (!EditorModeConst.Is_Editor_Mode)
      {
        HideFade();
        Client.instance.uiManager.uiLoadingPanel.SetDesc("下载资源中");
        yield return AssetBundleCheck();
        Client.instance.uiManager.SetLoadingPct(1);
        Client.instance.uiManager.uiLoadingPanel.SetDesc("加载资源中");
        yield return Client.instance.assetBundleManager.Initialize();
        yield return LuaRequireLoader.LoadLuaFiles();
      }

      XLuaManager.instance.OnInit();
      XLuaManager.instance.StartXLua();
      Client.instance.Goto<StageTest>(0,
        () => { Client.instance.uiManager.uiLoadingPanel.Reset(); });
    }


    IEnumerator AssetBundleCheck()
    {
      StartCoroutine(Client.instance.assetBundleUpdater.CheckUpdate());
      yield return new WaitUntil(() =>
      {
        bool is_update_finish = Client.instance.assetBundleUpdater.is_update_finish;
        if (!is_update_finish)
        {
          int downloading_count = Client.instance.assetBundleUpdater.need_download_dict.Count;
          if (downloading_count == 0)
            Client.instance.uiManager.SetLoadingPct(0);
          else
          {
            int cur_loaded_count = 0;
            foreach (var key in Client.instance.assetBundleUpdater.need_download_dict.Keys)
            {
              if (Client.instance.assetBundleUpdater.need_download_dict[key]
                .GetOrGetDefault("is_finished", () => false))
                cur_loaded_count++;
            }

            Client.instance.uiManager.SetLoadingPct(cur_loaded_count / (float) downloading_count);


          }
        }

        return is_update_finish;
      });
    }
  }
}



