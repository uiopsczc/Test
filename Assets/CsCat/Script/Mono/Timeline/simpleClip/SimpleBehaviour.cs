using System;
using UnityEngine.Playables;

namespace CsCat
{
	[Serializable]
	public class SimpleBehaviour : PlayableBehaviour
	{
		//Example of a variable that is going to be unique per-clip
		public string message;

		//ProcessFrame is like "the Update of Timeline"
		public override void ProcessFrame(Playable playable, FrameData info, object arg)
		{
			//Insert logic per frame in here
			var inputPlayable = (ScriptPlayable<SimpleBehaviour>)playable.GetInput(0);

			LogCat.Log(message);
		}
	}
}