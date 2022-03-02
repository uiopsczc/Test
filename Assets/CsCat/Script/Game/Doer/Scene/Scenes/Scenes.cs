using System;
using System.Collections;

namespace CsCat
{
	public class Scenes
	{
		private readonly Doer _parentDoer;
		private readonly string _subDoerKey;

		public Scenes(Doer parentDoer, string subDoerKey)
		{
			this._parentDoer = parentDoer;
			this._subDoerKey = subDoerKey;
		}

		////////////////////DoXXX/////////////////////////////////
		//卸载
		public void DoRelease()
		{
			SubDoerUtil3.DoReleaseSubDoer<Scene>(this._parentDoer, this._subDoerKey);
		}

		//保存
		public void DoSave(Hashtable dict, Hashtable dictTmp, string saveKey = null)
		{
			saveKey = saveKey ?? "scenes";
			var scenes = this.GetScenes();
			var dictScenes = new Hashtable();
			var dictScenesTmp = new Hashtable();
			for (var i = 0; i < scenes.Length; i++)
			{
				var scene = scenes[i];
				var dictScene = new Hashtable();
				var dictSceneTmp = new Hashtable();
				scene.PrepareSave(dictScene, dictSceneTmp);
				string rid = scene.GetRid();
				dictScenes[rid] = dictScene;
				if (!dictSceneTmp.IsNullOrEmpty())
					dictScenesTmp[rid] = dictSceneTmp;
			}

			if (!dictScenes.IsNullOrEmpty())
				dict[saveKey] = dictScenes;
			if (!dictScenesTmp.IsNullOrEmpty())
				dictTmp[saveKey] = dictScenesTmp;
		}

		//还原
		public void DoRestore(Hashtable dict, Hashtable dictTmp, string restoreKey = null)
		{
			restoreKey = restoreKey ?? "scenes";
			this.ClearScenes();
			var dictScenes = dict.Remove3<Hashtable>(restoreKey);
			var dictScenesTmp = dictTmp?.Remove3<Hashtable>(restoreKey);
			if (!dictScenes.IsNullOrEmpty())
			{
				foreach (string rid in dictScenes.Keys)
				{
					var sceneDict = this.GetSceneDict_ToEdit();
					Hashtable dictScene = dictScenes[rid] as Hashtable;
					Scene scene = Client.instance.sceneFactory.NewDoer(rid) as Scene;
					scene.SetEnv(this._parentDoer);
					Hashtable dictSceneTmp = null;
					if (dictScenesTmp != null && dictScenesTmp.ContainsKey(rid))
						dictSceneTmp = dictScenesTmp[rid] as Hashtable;
					scene.FinishRestore(dictScene, dictSceneTmp);
					sceneDict[rid] = scene;
				}
			}
		}
		///////////////////////////////OnXXX/////////////////////////////////////////////


		////////////////////////////////////////////////////////////////////////////
		public Scene[] GetScenes(string id = null, Func<Scene, bool> filterFunc = null)
		{
			return SubDoerUtil3.GetSubDoers(_parentDoer, _subDoerKey, id, filterFunc);
		}

		public Hashtable GetSceneDict_ToEdit() //可以直接插入删除
		{
			return SubDoerUtil3.GetSubDoerDict_ToEdit(_parentDoer, _subDoerKey);
		}


		public Scene GetScene(string idOrRid)
		{
			return SubDoerUtil3.GetSubDoer<Scene>(_parentDoer, _subDoerKey, idOrRid);
		}

		public void ClearScenes()
		{
			SubDoerUtil3.ClearSubDoers<Scene>(this._parentDoer, this._subDoerKey, scene => { });
		}
	}
}