using UnityEngine;
using UnityEngine.Timeline;

namespace CsCat
{
	public static class TimelineAssetExtension
	{
		public static T GetTrackAsset<T>(this TimelineAsset self, string trackName) where T : TrackAsset
		{
			return (T)self.GetTrackAsset(trackName);
		}

		public static TrackAsset GetTrackAsset(this TimelineAsset self, string trackName)
		{
			foreach (var outputTrack in self.GetOutputTracks())
			{
				if (outputTrack.name.Equals(trackName))
					return outputTrack;
				TrackAsset successFoundTrackAsset = GetSubTrackAsset(outputTrack, trackName);
				if (successFoundTrackAsset != null)
					return successFoundTrackAsset;
			}

			return null;
		}

		static TrackAsset GetSubTrackAsset(TrackAsset self, string trackName)
		{
			foreach (var subTrackAsset in self.GetChildTracks())
			{
				if (subTrackAsset.name.Equals(trackName))
					return subTrackAsset;
			}

			return null;
		}


#if UNITY_EDITOR
		public static TimelineAsset CloneTrackAsset(this TimelineAsset self, string trackName)
		{
			string path = self.GetAssetPath();

			TimelineAsset clone = ScriptableObject.CreateInstance<TimelineAsset>();


			foreach (var outputTrack in self.GetOutputTracks())
			{
				if (outputTrack.name.Equals(trackName))
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
					TrackAsset cloneTrackAsset = clone.CreateTrack(outputTrack.GetType(), null, outputTrack.name);
					for (var i = 0; i < outputTrack.GetType().GetProperties().Length; i++)
					{
						var info = outputTrack.GetType().GetProperties()[i];
						if (info.GetSetMethod(true) != null || info.GetSetMethod() != null)
							cloneTrackAsset.SetPropertyValue(info.Name, outputTrack.GetPropertyValue(info.Name));
					}

					string animClipName = StringConst.String_animClip;
					cloneTrackAsset.SetPropertyValue(animClipName,
						outputTrack.GetPropertyValue<AnimationClip>(animClipName));
				}
			}

			return clone;
		}
#endif
	}
}