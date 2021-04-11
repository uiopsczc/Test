namespace CsCat
{
  public class StageTest : StageBase
  {
    public override bool is_show_fade => true;
    public override bool is_show_loading => false;
    public override string stage_name => "StageTest";
    public override string scene_path => "Assets/Resources/common/ui/scene/StageTestScene.unity";


    public override void LoadPanels()
    {
      base.LoadPanels();
      panel_list.Add(Client.instance.uiManager.CreateChildPanel("UITestPanel", default(UITestPanel)));
    }

    public override void Show()
    {
      base.Show();
      this.HideFade();
    }
  }
}



