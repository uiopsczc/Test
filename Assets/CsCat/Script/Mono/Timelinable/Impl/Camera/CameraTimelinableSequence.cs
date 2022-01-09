using UnityEngine;

namespace CsCat
{
	public class CameraTimelinableSequence : TimelinableSequenceBase
	{
		public RuntimeAnimatorController runtimeAnimatorController;

		[SerializeField] private CameraTimelinableTrack[] _tracks = new CameraTimelinableTrack[0];

		public override TimelinableTrackBase[] tracks
		{
			get { return _tracks; }
			set { _tracks = value as CameraTimelinableTrack[]; }
		}
	}
}



