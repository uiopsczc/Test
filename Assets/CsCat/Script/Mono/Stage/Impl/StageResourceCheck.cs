using System.Collections;
using UnityEngine;

namespace CsCat
{
	public class StageResourceCheck : StageBase
	{
		public override bool isShowFade => true;
		public override bool isShowLoading => false;
		public override string stageName => "StageResourceCheck";


		public override void Show()
		{
			base.Show();
			StartCoroutine(IEResourceCheck());
		}

		private IEnumerator IEResourceCheck()
		{
			if (!EditorModeConst.IsEditorMode)
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
				() => { Client.instance.uiManager.uiLoadingPanel.DoReset(); });
		}


		IEnumerator AssetBundleCheck()
		{
			StartCoroutine(Client.instance.assetBundleUpdater.CheckUpdate());
			yield return new WaitUntil(() =>
			{
				bool isUpdateFinish = Client.instance.assetBundleUpdater.isUpdateFinish;
				if (!isUpdateFinish)
				{
					int downloadingCount = Client.instance.assetBundleUpdater.needDownloadDict.Count;
					if (downloadingCount == 0)
						Client.instance.uiManager.SetLoadingPct(0);
					else
					{
						int cur_loaded_count = 0;
						foreach (var key in Client.instance.assetBundleUpdater.needDownloadDict.Keys)
						{
							if (Client.instance.assetBundleUpdater.needDownloadDict[key]
								.GetOrGetDefault2("is_finished", () => false))
								cur_loaded_count++;
						}

						Client.instance.uiManager.SetLoadingPct(cur_loaded_count / (float) downloadingCount);
					}
				}

				return isUpdateFinish;
			});
		}
	}
}