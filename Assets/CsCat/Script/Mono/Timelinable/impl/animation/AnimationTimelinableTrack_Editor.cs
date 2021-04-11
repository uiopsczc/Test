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
      foreach (var playing_itemInfo in playing_itemInfo_list)
        (playing_itemInfo as AnimationTimelinableItemInfo).SyncAnimationWindow(cur_time);
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
      var _runtimeAnimatorController = (RuntimeAnimatorController)EditorGUILayout.ObjectField(
        "runtimeAnimatorController", runtimeAnimatorController, typeof(RuntimeAnimatorController), false);
      if (runtimeAnimatorController != _runtimeAnimatorController)
      {
        runtimeAnimatorController = _runtimeAnimatorController;
        UpdateRuntimeAnimatorController();
      }

      var _animator = EditorGUILayout.ObjectField("animator", animator, typeof(Animator));
      if (animator != _animator)
      {
        animator = _animator as Animator;
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
      using (new GUILayoutToggleAreaScope(itemInfoLibrary_toggleTween, "Library"))
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
            is_itemInfoLibrary_sorted = GUILayout.Toggle(is_itemInfoLibrary_sorted, "Sort", "button");
            if (is_itemInfoLibrary_sorted)
              Array.Sort(animationClips, (x, y) => x.name.AlphanumCompareTo(y.name));
            for (int i = 0; i < animationClips.Length; i++)
            {
              var animationClip = animationClips[i];
              if (GUILayout.Button(animationClip.name))
              {
                var new_itemInfo = new AnimationTimelinableItemInfo();
                new_itemInfo.time = cur_time;
                new_itemInfo.duration = animationClip.length;
                new_itemInfo.animationClip = animationClip;
                new_itemInfo.name = animationClip.name;
                AddItemInfo(new_itemInfo);
              }
            }
          }
        }
      }
    }
  }
}
#endif