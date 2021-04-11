namespace CsCat
{
  public class UIItemBaseTest
  {
    public static void Test(UIObject parent)
    {
      UIItemBase item = parent.AddChild<UIItemBase>(null, parent.graphicComponent.transform);
      item.InvokeAfterAllAssetsLoadDone(() => { item.Show("1", 2); });
    }
  }
}