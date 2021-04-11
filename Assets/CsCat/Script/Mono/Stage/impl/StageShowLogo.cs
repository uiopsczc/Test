namespace CsCat
{
  public class StageShowLogo : StageBase
  {
    public override bool is_show_fade => false;
    public override bool is_show_loading => false;
    public override string stage_name => "StageShowLogo";


    public override void LoadPanels()
    {
      Client.instance.audioManager.PlayBGMSound("Assets/Resources/common/audio/bgm.mp3");
    }

    public override void Show()
    {
      base.Show();
      Client.instance.Goto<StageResourceCheck>(FadeConst.Stage_Fade_Default_Appear_Duration);
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      Client.instance.uiManager.uiShowLogoPanel.graphicComponent.gameObject.SetActive(false);
      
    }


  }
}



