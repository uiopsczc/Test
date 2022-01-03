using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class TimelineRect
	{
		private float _widthSizePerSecond = 1f;
		private int _frameCountPerSecond = 10;

		private float _frameDuration => 1f / _frameCountPerSecond;

		public float widthPerSecond => TimelineRectConst.Width_Per_Second * _widthSizePerSecond;


		void DrawTimelineTrack()
		{
			Color trackBackgroundColor = AnimationMode.InAnimationMode()
				? new Color(211 / 255f, 127 / 255f, 127 / 255f, 0.5f)
				: new Color(173 / 255f, 205 / 255f, 211 / 255f, 0.5f);
			EditorGUI.DrawRect(new Rect(0, 0, this.duration * widthPerSecond, TimelineRectConst.Timeline_Track_Height),
				trackBackgroundColor);

			EditorGUI.DrawRect(new Rect(0, TimelineRectConst.Timeline_Track_Height, this.duration * widthPerSecond, 1),
				new Color(153 / 255f, 153 / 255f, 153 / 255f, 1));
			float minTime = _scrollPosition.x / widthPerSecond; //当前时间条的最小时间
			float maxTime = scrollRect.width / widthPerSecond + minTime; //当前时间条的最大时间
			GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
			labelStyle.normal.textColor = Color.black;
			Color color = Color.gray;

			for (int i = (int) minTime; i <= maxTime; i++)
			{
				//画秒的字符串
				string currentSecond = string.Format("{0}s", i);
				GUIContent secondContent = currentSecond.ToGUIContent();
				Vector2 secondContentCellSize = GUI.skin.label.CalcSize(secondContent);
				Rect secondContentRect = new Rect();
				secondContentRect.x = i * widthPerSecond;
				secondContentRect.y = TimelineRectConst.Timeline_Track_Height - secondContentCellSize.y - 3;
				secondContentRect.size = secondContentCellSize;
				GUI.Label(secondContentRect, secondContent, labelStyle);
				//画秒的刻度
				Rect secondSplitLineRect = new Rect();
				secondSplitLineRect.x = i * widthPerSecond;
				secondSplitLineRect.height = TimelineRectConst.Second_Split_Line_Height;
				secondSplitLineRect.y = TimelineRectConst.Timeline_Track_Height - secondSplitLineRect.height;
				secondSplitLineRect.width = 1;
				//画秒的子刻度
				for (int j = 0; j < _frameCountPerSecond; j++)
				{
					Rect secondSubSplitLineRect = new Rect();
					secondSubSplitLineRect.x = secondSplitLineRect.x + j * widthPerSecond / _frameCountPerSecond;
					secondSubSplitLineRect.width = 1;
					secondSubSplitLineRect.height = TimelineRectConst.Second_Sub_Split_Line_Height;
					secondSubSplitLineRect.y =
						TimelineRectConst.Timeline_Track_Height - secondSubSplitLineRect.height;
					Color secondSubSplitLineColor = color;
					secondSubSplitLineColor.a = 0.8f;
					EditorGUI.DrawRect(secondSubSplitLineRect, secondSubSplitLineColor);
				}

				EditorGUI.DrawRect(secondSplitLineRect, color);
			}
		}
	}
}