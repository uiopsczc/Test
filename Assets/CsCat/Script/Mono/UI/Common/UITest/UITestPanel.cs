using UnityEngine.UI;

namespace CsCat
{
	public class UITestPanel : UIBackgroundPanel
	{
		private Button _BtnGM;
		private Button _BtnTest;
		private Button _BtnCombatTest;

		protected override void _Init()
		{
			base._Init();
			this.SetPrefabPath("Assets/PatchResources/UI/UITest/Prefab/UITestPanel.prefab");
		}

		protected override void _InitGameObjectChildren()
		{
			base._InitGameObjectChildren();
			_BtnGM = this._frameTransform.Find("BtnGM").GetComponent<Button>();
			_BtnTest = this._frameTransform.Find("BtnTest").GetComponent<Button>();
			_BtnCombatTest = this._frameTransform.Find("BtnCombatTest").GetComponent<Button>();
		}

		protected override void _AddUnityListeners()
		{
			base._AddUnityListeners();
			this.RegisterOnClick(_BtnGM,
			  () => { Client.instance.uiManager.CreateChildPanel("UIGMPanel", default(UIGMTestPanel)); });
			this.RegisterOnClick(_BtnCombatTest, () =>
			{
				Client.instance.Goto<CombatStageTest>(0.5f,
			() => { Client.instance.uiManager.uiLoadingPanel.DoReset(); });
			});
			this.RegisterOnClick(_BtnTest, Test);
		}

		protected override void _PostSetGameObject()
		{
			base._PostSetGameObject();
			UIItemBaseTest.Test(this);
			UIGuidePanelTest.Test();
		}

		void Test()
		{
		}
	}
}