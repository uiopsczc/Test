#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class TimelinableItemInfoBase
	{
		[NonSerialized] public bool isSelected;
		[NonSerialized] public Rect rect;
		[NonSerialized] public GUIToggleTween toggleTween = new GUIToggleTween();


		public virtual void DrawGUISetting(TimelinableTrackBase track)
		{
			string title = string.Format("{0}[{1}]<color=red>{2}</color>",
			  name, track.itemInfoes.IndexOf(this), isSelected ? "*选中" : "");
			using (new GUILayoutToggleAreaScope(toggleTween, title, () =>
			{
				using (new GUIEnabledScope(track.itemInfoLibrary != null && track.IsItemInfoCanAddToLibrary()))
				{
					if (GUILayout.Button("addToLibrary", GUILayout.Width(100)))
					{
						var to_add_ItemInfo = GetType().CreateInstance<TimelinableItemInfoBase>();
						to_add_ItemInfo.CopyFrom(this);
						track.itemInfoLibrary.AddItemInfo(to_add_ItemInfo);
					}
				}

				if (GUILayout.Button("add", GUILayout.Width(64)))
				{
					var to_add_ItemInfo = GetType().CreateInstance<TimelinableItemInfoBase>();
					to_add_ItemInfo.CopyFrom(this);
					to_add_ItemInfo.time = track.curTime;
					track.AddItemInfo(to_add_ItemInfo);
				}

				if (GUILayout.Button("delete", GUILayout.Width(64)))
				{
					isSelected = false;
					track.RemoveItemInfo(this);
				}
			}))
			{
				name = EditorGUILayout.TextField("itemInfo_name", name);
				time = EditorGUILayout.FloatField("time", time).Minimum(0);
				duration = EditorGUILayout.FloatField("duration", duration).Minimum(0);

				DrawGUISetting_Detail();
			}
		}

		public virtual void DrawGUISetting_Detail()
		{
		}
	}
}
#endif