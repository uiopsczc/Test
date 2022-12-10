namespace CsCat
{
	public class UIItemBaseTest
	{
		public static void Test(UIObject parent)
		{
			UIItemBase item = parent.AddChild<UIItemBase>(null, parent.GetTransform());
			item.InvokePostPrefabLoad(() => { item.Show("1", 2); });
		}
	}
}