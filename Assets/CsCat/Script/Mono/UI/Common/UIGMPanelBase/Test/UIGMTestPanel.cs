using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class UIGMTestPanel : UIGMPanelBase
	{
		public override void InitConfigList()
		{
			base.InitConfigList();
			configList.Add(new Dictionary<string, object>
			{
				{"type", "SwitchItem"},
				{"desc", "GMTest"},
				{"yesCallback", new Action(() => GMTest())},
			});
		}

		public void GMTest()
		{
			LogCat.LogWarning("Hello,GMTest");
		}
	}
}