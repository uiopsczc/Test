namespace CsCat
{
  public class UIHUDNumberPanel : UIHUDPanel
  {
    public override void Init()
    {
      base.Init();
      this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UINumberPanel.prefab");

    }
  }
}