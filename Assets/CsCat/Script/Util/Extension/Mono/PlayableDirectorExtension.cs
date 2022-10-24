using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CsCat
{
	public static class PlayableDirectorExtension
	{
#if UNITY_EDITOR
		public static List<Object> CloneTrack(this PlayableDirector self, PlayableDirector dest, string trackName)
		{
			List<Object> toRemoveList = new List<Object>();
			TimelineAsset sourceTimelineAsset = (TimelineAsset)self.playableAsset;
			TimelineAsset cloneTimelineAsset = sourceTimelineAsset.CloneTrackAsset(trackName);
			dest.playableAsset = cloneTimelineAsset;
			toRemoveList.Add(cloneTimelineAsset);
			Object sourceBinding = self.GetGenericBinding(sourceTimelineAsset.GetTrackAsset(trackName));
			if (sourceBinding != null)
			{
				Object clone_binding;
				if (sourceBinding is UnityEngine.Component c)
				{
					GameObject cloneBindingGameObject = Object.Instantiate(c.gameObject);
					cloneBindingGameObject.transform.CopyFrom(c.transform);
					clone_binding = cloneBindingGameObject.GetComponent(c.GetType());
					toRemoveList.Add(cloneBindingGameObject);
				}
				else
				{
					clone_binding = Object.Instantiate(sourceBinding);
					toRemoveList.Add(clone_binding);
				}

				clone_binding.name = StringUtilCat.RoundBrackets(StringConst.String_clone) + sourceBinding.name;
				dest.SetGenericBinding(cloneTimelineAsset.GetTrackAsset(trackName), clone_binding);
			}

			return toRemoveList;
		}
#endif

		public static T GetTrackAsset<T>(this PlayableDirector self, string trackName) where T : TrackAsset
		{
			TimelineAsset timelineAsset = (TimelineAsset)self.playableAsset;
			return (T)timelineAsset.GetTrackAsset(trackName);
		}

		public static TrackAsset GetTrackAsset(this PlayableDirector self, string trackName)
		{
			TimelineAsset timelineAsset = (TimelineAsset)self.playableAsset;
			return timelineAsset.GetTrackAsset(trackName);
		}
	}
}