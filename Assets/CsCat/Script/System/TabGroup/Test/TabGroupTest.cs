using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public static class TabGroupTest
	{
		private static readonly TabGroup _tabGroup = new TabGroup();
		private static readonly bool[] _toggles = new bool[3];

		public static void InitTabGroup()
		{
			_Clear();
			for (int i = 0; i < _toggles.Length; i++)
			{
				int j = i; //这样才能形成闭包，否则直接用i是形成不了闭包的
				_tabGroup.AddTab(new Tab(() => _toggles[j] = true, () => _toggles[j] = false));
			}

			_tabGroup.TriggerTab(1);
		}

		private static void _Clear()
		{
			_tabGroup.ClearTabs();
			for (int i = 0; i < _toggles.Length; i++)
				_toggles[i] = false;
		}

		public static void DrawTabGroup()
		{
			for (int i = 0; i < _toggles.Length; i++)
			{
				using (new GUIBackgroundColorScope(_toggles[i] ? Color.red : GUI.backgroundColor))
				{
					if (GUILayout.Button(i.ToString()))
					{
						_tabGroup.TriggerTab(i);
					}
				}
			}
		}
	}
}