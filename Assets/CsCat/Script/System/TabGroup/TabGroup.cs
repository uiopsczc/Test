using System;
using System.Collections.Generic;

namespace CsCat
{
	public class TabGroup
	{
		private readonly List<Tab> _tabList = new List<Tab>();
		public int curSelectedIndex = -1; //默认全部不选中
		private int _preSelectedIndex = -1;

		public TabGroup()
		{
		}

		public void AddTab(Tab tab)
		{
			_tabList.Add(tab);
		}

		public Tab GetTab(int index)
		{
			return _tabList[index];
		}

		public void ClearTabs()
		{
			_tabList.Clear();
		}

		public void TriggerTab(int index)
		{
			if (index == curSelectedIndex)
				return;
			if (_preSelectedIndex != -1)
				_tabList[_preSelectedIndex].UnSelect();
			_preSelectedIndex = curSelectedIndex;

			curSelectedIndex = index;
			_tabList[curSelectedIndex].Select();
		}
	}
}