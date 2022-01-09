using UnityEngine.UI;

namespace CsCat
{
	public class UITestPanel : UIBackgroundPanel
	{
		private Button gm_btn;
		Button test_btn;
		Button combat_test_btn;

		public override void Init()
		{
			base.Init();
			this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UITestPanel.prefab");
		}

		public override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			gm_btn = this.frameTransform.FindComponentInChildren<Button>("gm_btn");
			test_btn = this.frameTransform.FindComponentInChildren<Button>("test_btn");
			combat_test_btn = this.frameTransform.FindComponentInChildren<Button>("combat_test_btn");


			UIItemBaseTest.Test(this);
			UIGuidePanelTest.Test();

		}

		protected override void AddUnityEvents()
		{
			base.AddUnityEvents();
			this.RegisterOnClick(gm_btn,
			  () => { Client.instance.uiManager.CreateChildPanel("UIGMPanel", default(UIGMTestPanel)); });
			this.RegisterOnClick(combat_test_btn, () =>
			{
				Client.instance.Goto<CombatStageTest>(0.5f,
			() => { Client.instance.uiManager.uiLoadingPanel.Reset(); });
			});
			this.RegisterOnClick(test_btn, Test);
		}

		void Test()
		{
		}
	}
}