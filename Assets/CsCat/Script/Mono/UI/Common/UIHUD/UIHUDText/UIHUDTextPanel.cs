namespace CsCat
{
  public class UIHUDTextPanel : UIHUDLayerPanel
  {

    public override void Init()
    {
      base.Init();
      this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UITestPanel.prefab");
    }
  }
}