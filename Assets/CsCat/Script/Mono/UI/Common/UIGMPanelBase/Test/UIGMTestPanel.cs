using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class UIGMTestPanel : UIGMPanelBase
	{
		public override void InitConfigList()
		{
			base.InitConfigList();
			config_list.Add(new Dictionary<string, object>
			{
				{"type", "switch_item"},
				{"desc", "GMTest"},
				{"yes_callback", new Action(() => GMTest())},
			});
		}

		public void GMTest()
		{
			LogCat.LogWarning("Hello,GMTest");
		}
	}
}