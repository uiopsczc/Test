#if UNITY_EDITOR
using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class TimelinableSequenceBase
	{
		public Vector2 scroll_position;


		public virtual void DrawGUISetting_Detail()
		{

		}

		public virtual void Save()
		{
			foreach (var track in tracks)
			{
				Array.Sort(track.itemInfoes);
				if (track.itemInfoLibrary != null)
					EditorUtility.SetDirty(track.itemInfoLibrary);
			}
			EditorUtility.SetDirty(this);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}


		public virtual void DrawGUISetting_Tracks(float play_time)
		{
			using (new GUILayoutBeginVerticalScope(EditorStyles.helpBox))
			{
				using (new GUILayoutBeginScrollViewScope(ref scroll_position))
				{
					for (int i = 0; i < tracks.Length; i++)
					{
						TimelinableTrackBase track = tracks[i];
						using (new GUILayoutToggleAreaScope(track.toggleTween,
						  string.Format("track[{0}]:{1}", i, track.name), () =>
						  {
							  using (new GUIEnabledScope(track.IsCanAddItemInfo()))
							  {
								  if (GUILayout.Button("addItemInfo", GUILayout.Width(80)))
								  {
									  var to_add_ItemInfo_type = this.GetFieldInfo("_tracks").FieldType.GetElementType()
							.GetFieldInfo("_itemInfoes").FieldType.GetElementType();
									  var to_add_ItemInfo = to_add_ItemInfo_type.CreateInstance<TimelinableItemInfoBase>();
									  to_add_ItemInfo.time = track.curTime;
									  track.AddItemInfo(to_add_ItemInfo);
								  }
							  }

							  if (GUILayout.Button("+", GUILayout.Width(32)))
							  {
								  var _clone_track = tracks.GetType().GetElementType().CreateInstance<TimelinableTrackBase>();
								  _clone_track.CopyFrom(track);
								  AddTrack(_clone_track);
							  }

							  if (tracks.Length != 1 || i != 0)
								  if (GUILayout.Button("-", GUILayout.Width(32)))
								  {
									  RemoveTrack(track);
								  }
						  }))
						{
							track.DrawGUISetting();
						}
						Array.Sort(track.itemInfoes);
						track.Retime(play_time);
					}
				}
			}
		}
	}
}
#endif