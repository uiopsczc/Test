#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class AnimationTimelinableTrack
	{
		public void SyncAnimationWindow()
		{
			for (var i = 0; i < playingItemInfoList.Count; i++)
			{
				var playingItemInfo = playingItemInfoList[i];
				(playingItemInfo as AnimationTimelinableItemInfo).SyncAnimationWindow(curTime);
			}
		}

		public override bool IsItemInfoCanAddToLibrary()
		{
			return false;
		}

		public override bool IsCanAddItemInfo()
		{
			return false;
		}

		public override void DrawGUISetting_Detail()
		{
			base.DrawGUISetting_Detail();
			var editorRuntimeAnimatorController = (RuntimeAnimatorController)EditorGUILayout.ObjectField(
			  "runtimeAnimatorController", runtimeAnimatorController, typeof(RuntimeAnimatorController), false);
			if (runtimeAnimatorController != editorRuntimeAnimatorController)
			{
				runtimeAnimatorController = editorRuntimeAnimatorController;
				UpdateRuntimeAnimatorController();
			}

			var editorAnimator = EditorGUILayout.ObjectField("animator", animator, typeof(Animator));
			if (animator != editorAnimator)
			{
				animator = editorAnimator as Animator;
				UpdateRuntimeAnimatorController();
			}
		}

		void UpdateRuntimeAnimatorController()
		{
			if (animator != null)
				animator.runtimeAnimatorController = runtimeAnimatorController;
		}



		public override void DrawGUISetting_ItemInfoLibrary()
		{
			if (animator == null)
				return;
			using (new GUILayoutToggleAreaScope(itemInfoLibraryToggleTween, "Library"))
			{
				if (animator.runtimeAnimatorController == null)
					EditorGUILayout.HelpBox("Library is empty\nruntimeAnimatorController==null", MessageType.Warning);
				else
				{
					AnimationClip[] animationClips = animator.runtimeAnimatorController.animationClips;
					if (animationClips.Length <= 0)
						EditorGUILayout.HelpBox("Library is empty\nanimationClips==null", MessageType.Warning);
					else
					{
						isItemInfoLibrarySorted = GUILayout.Toggle(isItemInfoLibrarySorted, "Sort", "button");
						if (isItemInfoLibrarySorted)
							Array.Sort(animationClips, (x, y) => x.name.AlphanumCompareTo(y.name));
						for (int i = 0; i < animationClips.Length; i++)
						{
							var animationClip = animationClips[i];
							if (GUILayout.Button(animationClip.name))
							{
								var newItemInfo = new AnimationTimelinableItemInfo();
								newItemInfo.time = curTime;
								newItemInfo.duration = animationClip.length;
								newItemInfo.animationClip = animationClip;
								newItemInfo.name = animationClip.name;
								AddItemInfo(newItemInfo);
							}
						}
					}
				}
			}
		}
	}
}
#endif