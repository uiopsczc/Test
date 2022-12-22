#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class AnimationTimelinableSequence : TimelinableSequenceBase
	{
		public void SyncAnimationWindow()
		{
			for (var index = 0; index < tracks.Length; index++)
			{
				var track = tracks[index];
				(track as AnimationTimelinableTrack).SyncAnimationWindow();
			}
		}
	}
}
#endif