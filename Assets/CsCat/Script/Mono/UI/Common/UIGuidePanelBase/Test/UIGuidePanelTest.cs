namespace CsCat
{
	public class UIGuidePanelTest
	{
		public static void Test()
		{
			UIGuidePanelBase panel = Client.instance.uiManager.CreateChildPanel(null, default(UIGuidePanelBase));
			panel.InvokePostPrefabLoad(() =>
			{
				panel.bgItem.Show();
				panel.CreateDialogRightItem().Show("hello");
			});
		}

	}
}