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
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject gameObject, int inputCount)
		{
			var playable =
				ScriptPlayable<TransformTweenMixerBehaviour>.Create(graph, inputCount);

			var template = playable.GetBehaviour();

			for (var i = 0; i < m_Clips.Count; i++)
			{
				var clip = m_Clips[i];
				var playableAsset = clip.asset as TransformTweenClip;
				playableAsset.clip = clip;
			}

			if (template != null)
			{
				var director = gameObject.GetComponent<PlayableDirector>();
				var transform = director.GetGenericBinding(this) as Transform;

				template.director = director;
				template.clipList = m_Clips;
				template.defaultPosition = transform.localPosition;
				template.defaultRotation = transform.localEulerAngles;
				template.defaultScale = transform.localScale;
			}

			return playable;
		}
	}
}