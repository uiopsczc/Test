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
			StartCoroutine(_IEResourceCheck());
		}

		private IEnumerator _IEResourceCheck()
		{
			if (!EditorModeConst.IsEditorMode)
			{
				HideFade();
				Client.instance.uiManager.uiLoadingPanel.SetDesc("下载资源中");
				yield return _AssetBundleCheck();
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


		IEnumerator _AssetBundleCheck()
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
						int curLoadedCount = 0;
						foreach (var key in Client.instance.assetBundleUpdater.needDownloadDict.Keys)
						{
							if (Client.instance.assetBundleUpdater.needDownloadDict[key]
								.GetOrGetDefault2("isFinished", () => false))
								curLoadedCount++;
						}

						Client.instance.uiManager.SetLoadingPct(curLoadedCount / (float) downloadingCount);
					}
				}

				return isUpdateFinish;
			});
		}
	}
}