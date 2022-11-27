using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class UIGMTestPanel2 : UIGMPanelBase
	{
		public override void InitConfigList()
		{
			base.InitConfigList();
			this.GetGameObject().name = "test2";
			configList.Add(new Dictionary<string, object>
			{
				{"type", "InputItem"},
				{"desc", "KKK"},
				{"yesCallback", new Action(() => LogCat.LogWarning("KKK"))},
			});
		}
	}
}