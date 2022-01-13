#if UNITY_EDITOR
using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class TimelinableSequenceBase
	{
		public Vector2 scrollPosition;


		public virtual void DrawGUISetting_Detail()
		{
		}

		public virtual void Save()
		{
			for (var i = 0; i < tracks.Length; i++)
			{
				var track = tracks[i];
				Array.Sort(track.itemInfoes);
				if (track.itemInfoLibrary != null)
					EditorUtility.SetDirty(track.itemInfoLibrary);
			}

			EditorUtility.SetDirty(this);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}


		public virtual void DrawGUISetting_Tracks(float playTime)
		{
			using (new GUILayoutBeginVerticalScope(EditorStyles.helpBox))
			{
				using (new GUILayoutBeginScrollViewScope(ref scrollPosition))
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
										var toAddItemInfoType = this.GetFieldInfo("_tracks").FieldType
											.GetElementType()
											.GetFieldInfo("_itemInfoes").FieldType.GetElementType();
										var toAddItemInfo =
											toAddItemInfoType.CreateInstance<TimelinableItemInfoBase>();
										toAddItemInfo.time = track.curTime;
										track.AddItemInfo(toAddItemInfo);
									}
								}

								if (GUILayout.Button("+", GUILayout.Width(32)))
								{
									var cloneTrack = tracks.GetType().GetElementType()
										.CreateInstance<TimelinableTrackBase>();
									cloneTrack.CopyFrom(track);
									AddTrack(cloneTrack);
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
						track.Retime(playTime);
					}
				}
			}
		}
	}
}
#endif