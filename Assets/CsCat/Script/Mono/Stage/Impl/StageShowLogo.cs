namespace CsCat
{
	public class StageShowLogo : StageBase
	{
		public override bool isShowFade => false;
		public override bool isShowLoading => false;
		public override string stageName => "StageShowLogo";


		public override void LoadPanels()
		{
			Client.instance.audioManager.PlayBGMSound("Assets/Resources/common/audio/bgm.mp3");
		}

		public override void Show()
		{
			base.Show();
			Client.instance.Goto<StageResourceCheck>(FadeConst.Stage_Fade_Default_Appear_Duration);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			Client.instance.uiManager.uiShowLogoPanel.graphicComponent.gameObject.SetActive(false);

		}


	}
}



