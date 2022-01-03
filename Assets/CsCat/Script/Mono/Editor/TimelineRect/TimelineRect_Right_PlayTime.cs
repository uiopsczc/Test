using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class TimelineRect
	{
		private Rect _playTimeLineRect;

		private float _playTime;

		public float playTime
		{
			get => _playTime;
			set
			{
				float prePlayTime = _playTime;
				_playTime = value < 0 ? 0 : value;
				if (prePlayTime != _playTime)
					OnPlayTimeChange();
			}
		}


		void DrawPlayTimeLine()
		{
			_playTimeLineRect.x = playTime * widthPerSecond;
			_playTimeLineRect.y = 0;
			_playTimeLineRect.width = 1;
			_playTimeLineRect.height = totalRect.height;
			EditorGUI.DrawRect(_playTimeLineRect, Color.red);
		}

		void OnPlayTimeChange()
		{
			onPlayTimeChangeCallback?.Invoke(playTime);
			if (!EditorApplication.isPlaying && AnimationMode.InAnimationMode())
			{
				AnimationMode.BeginSampling();
				onAnimatingCallback?.Invoke(playTime);
				AnimationMode.EndSampling();
				SceneView.RepaintAll();
			}
		}
	}
}