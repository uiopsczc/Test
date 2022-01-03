using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class TimelineRect
	{
		private bool _isPlaying;
		private float _preRealtimeSinceStartupOfPlayTime;
		private float _playSpeed = 1f;

		public void DrawLeftTop()
		{
			using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
			{
				using (var check1 = new EditorGUIBeginChangeCheckScope())
				{
					GUILayout.Toggle(_isPlaying, EditorIconGUIContent.GetSystem(EditorIconGUIContentType.PlayButton),
						EditorStyles.toolbarButton); //Play
					if (check1.IsChanged)
						SwitchPlay();
				}


				using (var check3 = new EditorGUIBeginChangeCheckScope())
				{
					GUILayout.Toggle(AnimationMode.InAnimationMode(),
						EditorIconGUIContent.GetSystem(EditorIconGUIContentType.Animation_Record),
						EditorStyles.toolbarButton); //Animate
					if (check3.IsChanged)
					{
						if (AnimationMode.InAnimationMode())
							AnimationMode.StopAnimationMode();
						else
							AnimationMode.StartAnimationMode();
					}
				}

				using (new EditorGUIUtilityLabelWidthScope(30))
				{
					_frameCountPerSecond =
						EditorGUILayout.IntField("FPS", _frameCountPerSecond, GUILayout.Width(30 + 25));
					if (_frameCountPerSecond <= 1)
						_frameCountPerSecond = 1;
				}

				using (new EditorGUIUtilityLabelWidthScope(70))
				{
					_playSpeed = EditorGUILayout.FloatField("play_speed", _playSpeed, GUILayout.Width(70 + 25));
					if (_playSpeed <= 0)
						_playSpeed = 1;
				}

				GUILayout.FlexibleSpace();
			}
		}


		void SwitchPlay()
		{
			if (!_isPlaying)
				Play();
			else
				Pause();
		}


		void Play()
		{
			_isPlaying = true;
			_preRealtimeSinceStartupOfPlayTime = Time.realtimeSinceStartup;
			onPlayCallback?.Invoke();
		}

		void Pause()
		{
			_isPlaying = false;
			onPauseCallback?.Invoke();
		}
	}
}