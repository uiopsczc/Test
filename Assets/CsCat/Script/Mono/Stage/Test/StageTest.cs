namespace CsCat
{
	public class StageTest : StageBase
	{
		public override bool isShowFade => true;
		public override bool isShowLoading => false;
		public override string stageName => "StageTest";
		public override string scenePath => "Assets/Resources/common/ui/scene/StageTestScene.unity";


		public override void LoadPanels()
		{
			base.LoadPanels();
			panelList.Add(Client.instance.uiManager.CreateChildPanel("UITestPanel", default(UITestPanel)));
		}

		public override void Show()
		{
			base.Show();
			this.HideFade();
		}
	}
}



