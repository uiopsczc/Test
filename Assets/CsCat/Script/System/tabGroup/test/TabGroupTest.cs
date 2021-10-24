using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
    public static class TabGroupTest
    {
        private static TabGroup tabGroup = new TabGroup();
        private static bool[] toggles = new bool[3];

        public static void InitTabGroup()
        {
            Clear();
            for (int i = 0; i < toggles.Length; i++)
            {
                int j = i; //这样才能形成闭包，否则直接用i是形成不了闭包的
                tabGroup.AddTab(new Tab(() => toggles[j] = true, () => toggles[j] = false));
            }

            tabGroup.TriggerTab(1);
        }

        private static void Clear()
        {
            tabGroup.ClearTabs();
            for (int i = 0; i < toggles.Length; i++)
                toggles[i] = false;
        }

        public static void DrawTabGroup()
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                using (new GUIBackgroundColorScope(toggles[i] ? Color.red : GUI.backgroundColor))
                {
                    if (GUILayout.Button(i.ToString()))
                    {
                        tabGroup.TriggerTab(i);
                    }
                }
            }
        }
    }
}