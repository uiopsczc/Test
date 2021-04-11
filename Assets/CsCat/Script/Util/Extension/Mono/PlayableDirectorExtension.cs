using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CsCat
{
  public static class PlayableDirectorExtension
  {
#if UNITY_EDITOR
    public static List<Object> CloneTrack(this PlayableDirector self, PlayableDirector dest, string track_name)
    {
      List<Object> to_remove_list = new List<Object>();
      TimelineAsset source_timelineAsset = (TimelineAsset) self.playableAsset;
      TimelineAsset clone_timelineAsset = source_timelineAsset.CloneTrackAsset(track_name);
      dest.playableAsset = clone_timelineAsset;
      to_remove_list.Add(clone_timelineAsset);
      Object source_binding = self.GetGenericBinding(source_timelineAsset.GetTrackAsset(track_name));
      if (source_binding != null)
      {
        Object clone_binding;
        if (source_binding is Component)
        {
          Component c = source_binding as Component;
          GameObject clone_binding_gameObject = Object.Instantiate(c.gameObject) as GameObject;
          clone_binding_gameObject.transform.CopyFrom(c.transform);
          clone_binding = clone_binding_gameObject.GetComponent(source_binding.GetType());
          to_remove_list.Add(clone_binding_gameObject);
        }
        else
        {
          clone_binding = Object.Instantiate(source_binding);
          to_remove_list.Add(clone_binding);
        }

        clone_binding.name = "(clone)" + source_binding.name;
        dest.SetGenericBinding(clone_timelineAsset.GetTrackAsset(track_name), clone_binding);

      }

      return to_remove_list;
    }
#endif

    public static T GetTrackAsset<T>(this PlayableDirector self, string track_name) where T : TrackAsset
    {
      TimelineAsset timelineAsset = (TimelineAsset) self.playableAsset;
      return (T) timelineAsset.GetTrackAsset(track_name);
    }

    public static TrackAsset GetTrackAsset(this PlayableDirector self, string track_name)
    {
      TimelineAsset timelineAsset = (TimelineAsset) self.playableAsset;
      return timelineAsset.GetTrackAsset(track_name);
    }


  }

}