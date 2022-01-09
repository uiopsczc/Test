using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CsCat
{
	[TrackColor(0.7366781f, 0.3261246f, 0.8529412f)]
	[TrackClipType(typeof(TransformTweenClip))]
	[TrackBindingType(typeof(Transform))]
	public class TransformTweenTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			var playable =
			  ScriptPlayable<TransformTweenMixerBehaviour>.Create(graph, inputCount);

			var template = playable.GetBehaviour();

			foreach (var clip in m_Clips)
			{
				var playable_asset = clip.asset as TransformTweenClip;
				playable_asset.clip = clip;
			}

			if (template != null)
			{
				var director = go.GetComponent<PlayableDirector>();
				var transform = director.GetGenericBinding(this) as Transform;

				template.director = director;
				template.clip_list = m_Clips;
				template.position_default = transform.localPosition;
				template.rotation_default = transform.localEulerAngles;
				template.scale_default = transform.localScale;
			}

			return playable;
		}
	}
}