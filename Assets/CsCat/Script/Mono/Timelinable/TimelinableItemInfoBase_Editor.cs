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
						var toAddItemInfo = GetType().CreateInstance<TimelinableItemInfoBase>();
						toAddItemInfo.CopyFrom(this);
						track.itemInfoLibrary.AddItemInfo(toAddItemInfo);
					}
				}

				if (GUILayout.Button("add", GUILayout.Width(64)))
				{
					var toAddItemInfo = GetType().CreateInstance<TimelinableItemInfoBase>();
					toAddItemInfo.CopyFrom(this);
					toAddItemInfo.time = track.curTime;
					track.AddItemInfo(toAddItemInfo);
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

				DrawGUISettingDetail();
			}
		}

		public virtual void DrawGUISettingDetail()
		{
		}
	}
}
#endif