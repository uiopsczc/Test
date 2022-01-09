using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public static partial class TimelinableEditorWindowUtil
	{
		public static void OnDrawTracksLeftSideCallback(TimelinableSequenceBase sequence, TimelineRect timelineRect)
		{
			if (sequence == null || sequence.tracks.IsNullOrEmpty())
				return;
			for (int i = 0; i < sequence.tracks.Length; i++)
			{
				TimelinableTrackBase track = sequence.tracks[i];
				var trackColor = i % 2 == 0 ? Color.gray.SetA(0.2f) : Color.gray.SetA(0.4f);
				var leftTrackRect = timelineRect.GetLeftTrackRect(i);
				using (new GUILayout.AreaScope(leftTrackRect))
				{
					EditorGUI.DrawRect(new Rect(0, 0, leftTrackRect.width, leftTrackRect.height), trackColor);
					EditorGUILayout.LabelField(track.name, GUIStyleConst.LabelBoldMiddleCenterStyle);
				}
			}
		}

		public static void OnDrawTracksRightSideCallback(TimelinableSequenceBase sequence, TimelineRect timelineRect)
		{
			if (sequence == null || sequence.tracks.IsNullOrEmpty())
				return;
			for (int i = 0; i < sequence.tracks.Length; i++)
			{
				TimelinableTrackBase track = sequence.tracks[i];
				var trackColor = i % 2 == 0 ? Color.gray.SetA(0.2f) : Color.gray.SetA(0.4f);
				var rightTrackRect = timelineRect.GetRightTrackRect(i);

				using (new GUILayout.AreaScope(rightTrackRect))
				{
					EditorGUI.DrawRect(new Rect(0, 0, rightTrackRect.width, rightTrackRect.height), trackColor);
					//Draw ItemInfo
					for (int j = 0; j < track.itemInfoes.Length; j++)
					{
						var itemInfo = track.itemInfoes[j];
						var itemInfoRect = timelineRect.GetRightTrackRect(itemInfo.time, itemInfo.duration, i);
						itemInfo.rect = itemInfoRect;

						itemInfoRect.y = 0;
						var style = new GUIStyle(GUIStyleConst.LabelBoldMiddleLeftStyle.SetName(GUI.skin.box));
						if (itemInfo.isSelected || itemInfo.IsTimeInside(track.curTime))
							GUIUtil.Box(itemInfoRect,
								string.Format("{0}[{1}]<color=red>{2}</color>", itemInfo.name, j,
									itemInfo.isSelected ? "*" : ""),
								style, Color.blue.SetA(0.5f));
						else
							GUIUtil.Box(itemInfoRect, string.Format("{0}[{1}]", itemInfo.name, j), style,
								Color.gray.SetA(0.5f));
					}
				}
			}
		}

		public static bool IsMouseDownOfSelectedItem(Vector2 mousePosition, TimelinableSequenceBase sequence)
		{
			if (sequence == null || sequence.tracks.IsNullOrEmpty())
				return false;
			foreach (var track in sequence.tracks)
			{
				foreach (var itemInfo in track.itemInfoes)
				{
					if (itemInfo.rect.Contains(mousePosition) && itemInfo.isSelected)
						return true;
				}
			}

			return false;
		}

		public static bool TryToSelectUnselectedItemCallback(Vector2 mousePosition, TimelinableSequenceBase sequence)
		{
			if (sequence == null || sequence.tracks.IsNullOrEmpty())
				return false;
			TimelinableItemInfoBase selectNewUnselectedItemInfo = null;
			foreach (var track in sequence.tracks)
			{
				foreach (var itemInfo in track.itemInfoes)
				{
					if (itemInfo.rect.Contains(mousePosition) && !itemInfo.isSelected)
					{
						selectNewUnselectedItemInfo = itemInfo;
						selectNewUnselectedItemInfo.isSelected = true;
						break;
					}
				}

				if (selectNewUnselectedItemInfo != null)
					break;
			}

			if (!Event.current.control)
			{
				foreach (var track in sequence.tracks)
				{
					foreach (var itemInfo in track.itemInfoes)
					{
						if (selectNewUnselectedItemInfo != itemInfo)
							itemInfo.isSelected = false;
					}
				}
			}


			if (selectNewUnselectedItemInfo != null)
				return true;
			return false;
		}


		public static void UpdateSelectedItemsCallback(Rect selectingRect, TimelinableSequenceBase sequence)
		{
			if (sequence == null || sequence.tracks.IsNullOrEmpty())
				return;
			foreach (var track in sequence.tracks)
			{
				foreach (var itemInfo in track.itemInfoes)
				{
					if (!Event.current.control || !itemInfo.isSelected)
						itemInfo.isSelected = itemInfo.rect.Overlaps(selectingRect);
				}
			}
		}

		public static void OnDraggingSelectedCallback(float deltaDuration, TimelinableSequenceBase sequence,
			float playTime)
		{
			if (sequence == null || sequence.tracks.IsNullOrEmpty())
				return;
			foreach (var track in sequence.tracks)
			{
				bool isTrackHasSelected = false;
				foreach (var itemInfo in track.itemInfoes)
				{
					if (itemInfo.isSelected)
					{
						itemInfo.time += deltaDuration;
						isTrackHasSelected = true;
					}
				}

				if (isTrackHasSelected)
				{
					Array.Sort(track.itemInfoes);
					track.Retime(playTime);
				}
			}
		}

		public static bool IsHasSelectedItem(TimelinableSequenceBase sequence)
		{
			if (sequence == null || sequence.tracks.IsNullOrEmpty())
				return false;
			foreach (var track in sequence.tracks)
			{
				foreach (var itemInfo in track.itemInfoes)
				{
					if (itemInfo.isSelected)
						return true;
				}
			}

			return false;
		}

		public static void OnPlayTimeChangeCallback(TimelinableSequenceBase sequence, float playTime)
		{
			if (sequence == null || sequence.tracks.IsNullOrEmpty())
				return;
			sequence.Retime(playTime);
		}

		public static void OnMouseRightButtonClickCallback(Rect positionRect, TimelinableTrackBase[] tracks)
		{
			GUIContent[] menuItems = new[] {new GUIContent("Past"), new GUIContent("Delete")};
			EditorUtility.DisplayCustomMenu(positionRect, menuItems, -1, OnMenuItem, tracks);
		}

		private static void OnMenuItem(object userData, string[] options, int selected)
		{
			TimelinableTrackBase[] tracks = userData as TimelinableTrackBase[];
			switch (selected)
			{
				case 0:
					OnDoEditorCommandCallback(EditorCommand.Paste, tracks);
					break;
				case 1:
					OnDoEditorCommandCallback(EditorCommand.Delete, tracks);
					break;
				default:
					break;
			}
		}


		public static void OnDoEditorCommandCallback(EditorCommand editorCommand, TimelinableTrackBase[] tracks)
		{
			switch (editorCommand)
			{
				case EditorCommand.Copy:
					break;
				case EditorCommand.Paste:
					DoEditorOperation_Paste(tracks);
					break;
				case EditorCommand.Delete:
					DoEditorOperation_Delete(tracks);
					break;
				default:
					break;
			}
		}

		private static void DoEditorOperation_Paste(TimelinableTrackBase[] tracks)
		{
			if (tracks.IsNullOrEmpty())
				return;
			using (var d = new DelayEditHandlerScope(null))
			{
				foreach (var track in tracks)
				{
					foreach (var itemInfo in track.itemInfoes)
					{
						if (itemInfo.isSelected)
						{
							var _itemInfo = itemInfo; //形成闭包
							var _track = track;
							d.ToCallback(() =>
							{
								var addItemInfo = _itemInfo.GetType().CreateInstance<TimelinableItemInfoBase>();
								addItemInfo.CopyFrom(_itemInfo);
								addItemInfo.time = _track.curTime;
								_track.AddItemInfo(addItemInfo);
							});
						}
					}
				}
			}
		}


		private static void DoEditorOperation_Delete(TimelinableTrackBase[] tracks)
		{
			if (tracks.IsNullOrEmpty())
				return;
			using (var d = new DelayEditHandlerScope(null))
			{
				foreach (var track in tracks)
				{
					foreach (var itemInfo in track.itemInfoes)
					{
						if (itemInfo.isSelected)
						{
							var _itemInfo = itemInfo; //形成闭包
							var _track = track; //形成闭包
							d.ToCallback(() => { _track.RemoveItemInfo(_itemInfo); });
						}
					}
				}
			}
		}
	}
}