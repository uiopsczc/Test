using UnityEngine;
using UnityEngine.Timeline;

namespace CsCat
{
  public static class TimelineAssetExtension
  {

    public static T GetTrackAsset<T>(this TimelineAsset self, string track_name) where T : TrackAsset
    {
      return (T)self.GetTrackAsset(track_name);
    }

    public static TrackAsset GetTrackAsset(this TimelineAsset self, string track_name)
    {
      foreach (var output_track in self.GetOutputTracks())
      {
        if (output_track.name.Equals(track_name))
          return output_track;
        TrackAsset suceess_found_trackAsset = GetSubTrackAsset(output_track, track_name);
        if (suceess_found_trackAsset != null)
          return suceess_found_trackAsset;
      }

      return null;
    }

    static TrackAsset GetSubTrackAsset(TrackAsset self, string track_name)
    {
      foreach (var sub_trackAsset in self.GetChildTracks())
      {
        if (sub_trackAsset.name.Equals(track_name))
          return sub_trackAsset;
      }

      return null;
    }



#if UNITY_EDITOR
    public static TimelineAsset CloneTrackAsset(this TimelineAsset self, string track_name)
    {
      string path = self.GetAssetPath();

      TimelineAsset clone = ScriptableObject.CreateInstance<TimelineAsset>();


      foreach (var output_track in self.GetOutputTracks())
      {
        if (output_track.name.Equals(track_name))
        {
          //                TrackAsset cloneTrackAsset= clone.CreateTrack(outputTrack.GetType(), null, outputTrack.name);
          //                string animClipName = "animClip";
          //                cloneTrackAsset.SetPropertyValue(animClipName, outputTrack.GetPropertyValue<AnimationClip>(animClipName));
          //
          //                foreach (var clip in outputTrack.GetClips())
          //                {
          //                    TimelineClip cloneClip= cloneTrackAsset.CreateDefaultClip();
          //                    foreach (var fieldInfo in clone.GetType().GetFields())
          //                    {
          //                        cloneClip.SetFieldValue(fieldInfo.Name, clip.GetFieldValue(fieldInfo.Name));
          //                        cloneClip.parentTrack = cloneTrackAsset;
          //                    }
          //                }
          TrackAsset clone_trackAsset = clone.CreateTrack(output_track.GetType(), null, output_track.name);
          foreach (var info in output_track.GetType().GetProperties())
          {
            if (info.GetSetMethod(true) != null || info.GetSetMethod() != null)
              clone_trackAsset.SetPropertyValue(info.Name, output_track.GetPropertyValue(info.Name));
          }

          string anim_clip_name = "animClip";
          clone_trackAsset.SetPropertyValue(anim_clip_name,
            output_track.GetPropertyValue<AnimationClip>(anim_clip_name));




        }
      }

      return clone;
    }
#endif

  }

}