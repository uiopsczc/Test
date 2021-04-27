namespace CsCat
{
  public class UIHUDTextPanel : UIHUDPanel
  {

    public override void Init()
    {
      base.Init();
      this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UITestPanel.prefab");
    }
  }
}