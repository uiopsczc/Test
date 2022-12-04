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
		public List<UIPanel> panelList = new List<UIPanel>();


		public virtual bool isShowFade => false;
		public virtual bool isShowLoading => true;
		public virtual string stageName { get; }
		public virtual string scenePath { get; }
		public string sceneName => scenePath.WithoutSuffix().FileName();


		public Action onShowCallback;


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
			float lastPCT = 0;
			SetLoadingPct(lastPCT);


			LoadPanels();
			yield return WaitUntilAllPanelsLoadDone();
			lastPCT = 0.1f;
			SetLoadingPct(lastPCT);


			if (!scenePath.IsNullOrWhiteSpace())
				yield return SceneManager.LoadSceneAsync(scenePath, loadSceneMode);
			lastPCT = 0.2f;
			SetLoadingPct(lastPCT);


			yield return WaitUntilPreLoadAssetsLoadDone((pct) =>
			{
				SetLoadingPct(lastPCT + Mathf.Lerp(pct, 0, 0.9f - lastPCT));
			});


			yield return WaitUntilAllAssetsLoadDone();
			SetLoadingPct(1);


			yield return new WaitForSeconds(0.05f);
			yield return IEPreShow();
			HideLoading();


			this.FireEvent(null, StageEventNameConst.On_Stage_Loaded, this);
			Show();
		}

		public virtual IEnumerator IEPreShow()
		{
			yield return null;
		}

		public virtual void Show()
		{
			onShowCallback?.Invoke();
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
			if (isShowLoading)
				Client.instance.uiManager.SetLoadingPct(pct);
		}

		public void HideLoading()
		{
			if (isShowLoading)
				Client.instance.uiManager.HideLoading();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////
		public IEnumerator WaitUntilPreLoadAssetsLoadDone(Action<float> callback)
		{
			var assetAsyncloaderProsessingList = Client.instance.assetBundleManager.assetAsyncloaderProcessingList;
			var assetBundleAsyncLoaderProsessingList =
				Client.instance.assetBundleManager.assetBundleAsyncLoaderProcessingList;
			float total_loading_count =
				assetAsyncloaderProsessingList.Count + assetBundleAsyncLoaderProsessingList.Count;
			float curPCT = 0;
			float nextPCT = 0;
			while (assetAsyncloaderProsessingList.Count > 0 || assetBundleAsyncLoaderProsessingList.Count > 0)
			{
				curPCT = (assetAsyncloaderProsessingList.Count + assetBundleAsyncLoaderProsessingList.Count) /
				          total_loading_count;
				if (curPCT > nextPCT)
					nextPCT = curPCT;
				callback(curPCT);
				yield return null;
			}

			callback(1);
		}

		public IEnumerator WaitUntilAllAssetsLoadDone()
		{
			yield return new WaitUntil(() =>
			{
				if (isAllAssetsLoadDone)
					return true;
				return false;
			});
		}


		public IEnumerator WaitUntilAllPanelsLoadDone()
		{
			yield return new WaitUntil(() =>
			{
				for (var i = 0; i < panelList.Count; i++)
				{
					UIPanel panel = panelList[i];
					if (!panel.isAllAssetsLoadDone)
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
			Client.instance.uiManager.DoReset();
			PoolCatManager.instance.Trim(); //清理所有的对象池
			if (!scenePath.IsNullOrWhiteSpace())
			{
				yield return SceneManager.UnloadSceneAsync(sceneName);
			}

			yield return null;
		}
	}
}