#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class AnimationTimelinableItemInfo
	{
		public void SyncAnimationWindow(float playTime)
		{
			float elaspedDuration = (playTime - time) * speed;
			int newCurrentFrame = (int) (elaspedDuration * animationClip.frameRate);
			var animationWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.AnimationWindow");
			var animationWindows =
				animationWindowType.InvokeMethod<object>("GetAllAnimationWindows"); //List<AnimationWindow>
			int count = animationWindows.GetPropertyValue<int>("Count");
			if (count > 0)
			{
				EditorWindow animationWindow = EditorWindow.GetWindow(animationWindowType); //AnimationWindow
				var animEditor = animationWindow.GetFieldValue("m_AnimEditor"); //AnimEditor
				var animationWindowState = animEditor.GetPropertyValue("state"); //AnimationWindowState
				var currentFrame = animationWindowState.GetPropertyValue("currentFrame");
				//          LogCat.log("before set currentFrame: " , currentFrame);
				animationWindowState.SetPropertyValue("currentFrame", newCurrentFrame);
				currentFrame = animationWindowState.GetPropertyValue("currentFrame");
				//          LogCat.log("after set currentFrame: " , currentFrame);
				var selection = animEditor.GetPropertyValue("selection"); //AnimationWindowSelectionItem
				var animationClip = (AnimationClip) selection.GetPropertyValue("animationClip");
				if (animationClip != this.animationClip)
				{
					selection.SetPropertyValue("animationClip", animationClip);
					selection.SetPropertyValue("frameRate", animationClip.frameRate);
				}

				animationWindow.Repaint();
			}
		}

		public override void DrawGUISettingDetail()
		{
			base.DrawGUISettingDetail();
			crossFadeDuration = EditorGUILayout
				.FloatField("crossFadeDuration", crossFadeDuration)
				.Minimum(0.1f);

			using (new EditorGUIDisabledGroupScope(true))
				EditorGUILayout.ObjectField("animationClip", animationClip, typeof(AnimationClip));
		}
	}
}
#endif