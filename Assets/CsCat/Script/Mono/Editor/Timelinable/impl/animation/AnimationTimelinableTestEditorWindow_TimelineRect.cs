using UnityEditor;

namespace CsCat
{
  public partial class AnimationTimelinableTestEditorWindow
  {
    void OnAnimatingCallback(float play_time)
    {
      if (sequence == null || sequence.tracks.IsNullOrEmpty())
        return;
      foreach (AnimationTimelinableTrack track in sequence.tracks)
      {
        foreach (AnimationTimelinableItemInfo itemInfo in track.itemInfoes)
        {
          if (track.animator == null || track.runtimeAnimatorController == null)
            continue;

          if (itemInfo.IsTimeInside(play_time))
          {
            float elasped_duration = play_time - itemInfo.time;

            AnimationMode.SampleAnimationClip(track.animator.gameObject, itemInfo.animationClip,
              elasped_duration * itemInfo.speed);
          }
        }
      }
    }
  }
}