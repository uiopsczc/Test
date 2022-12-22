using UnityEngine;

namespace CsCat
{
	public partial class AnimationTimelinableSequence : TimelinableSequenceBase
	{
		[SerializeField] private AnimationTimelinableTrack[] _tracks = new AnimationTimelinableTrack[0];

		public override TimelinableTrackBase[] tracks
		{
			get => _tracks;
			set => _tracks = value as AnimationTimelinableTrack[];
		}
	}
}