using System.Collections.Generic;

namespace CsCat
{
	public partial class UINotifyManager
	{
		//走马灯效果
		private readonly List<string> _lanternNotifyDescCacheList = new List<string>();

		public void LanternNotify(string desc)
		{
			_lanternNotifyDescCacheList.Add(desc);
			if (!Client.instance.uiManager.GetChildPanel<UILanternNotifyPanel>("UILanternNotifyPanel").IsShow())
				LanternNotify();
		}

		public void LanternNotify()
		{
			if (_lanternNotifyDescCacheList.Count > 0)
			{
				string desc = _lanternNotifyDescCacheList.RemoveFirst();
				UILanternNotifyPanel panel =
				  Client.instance.uiManager.GetChildPanel<UILanternNotifyPanel>("UILanternNotifyPanel");
				panel.Show(desc);
			}
		}


	}
}




