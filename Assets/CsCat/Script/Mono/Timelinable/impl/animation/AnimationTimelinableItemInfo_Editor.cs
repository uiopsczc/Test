#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class AnimationTimelinableItemInfo
  {
    public void SyncAnimationWindow(float play_time)
    {
      float elasped_duration = (play_time - time) * speed;
      int new_current_frame = (int)(elasped_duration * animationClip.frameRate);
      var AnimationWindow_type = typeof(EditorWindow).Assembly.GetType("UnityEditor.AnimationWindow");
      var animationWindows =
        AnimationWindow_type.InvokeMethod<object>("GetAllAnimationWindows"); //List<AnimationWindow>
      int count = animationWindows.GetPropertyValue<int>("Count");
      if (count > 0)
      {
        EditorWindow animationWindow = EditorWindow.GetWindow(AnimationWindow_type); //AnimationWindow
        var animEditor = animationWindow.GetFieldValue("m_AnimEditor"); //AnimEditor
        var animationWindowState = animEditor.GetPropertyValue("state"); //AnimationWindowState
        var currentFrame = animationWindowState.GetPropertyValue("currentFrame");
        //          LogCat.log("before set currentFrame: " , currentFrame);
        animationWindowState.SetPropertyValue("currentFrame", new_current_frame);
        currentFrame = animationWindowState.GetPropertyValue("currentFrame");
        //          LogCat.log("after set currentFrame: " , currentFrame);
        var selection = animEditor.GetPropertyValue("selection"); //AnimationWindowSelectionItem
        var animationClip = (AnimationClip)selection.GetPropertyValue("animationClip");
        if (animationClip != this.animationClip)
        {
          selection.SetPropertyValue("animationClip", animationClip);
          selection.SetPropertyValue("frameRate", animationClip.frameRate);
        }

        animationWindow.Repaint();
      }
    }

    public override void DrawGUISetting_Detail()
    {
      base.DrawGUISetting_Detail();
      cross_fade_duration = EditorGUILayout
        .FloatField("cross_fade_duration", cross_fade_duration)
        .Minimum(0.1f);

      using (new EditorGUIDisabledGroupScope(true))
        EditorGUILayout.ObjectField("animationClip", animationClip, typeof(AnimationClip));
    }
  }
}
#endif