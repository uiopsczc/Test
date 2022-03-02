using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class AbstractPhaseTrack<T>
	{
		public bool isLoop;
		//持续时间
		public int durationTick;

		public List<AbstractPhase<T>> phaseList = new List<AbstractPhase<T>>();

		public T GetSnapshot(int targetTick)
		{
			if (targetTick < 0)
				throw new ArgumentException(string.Format("Tick should be zero or positive number. tick:{0}", targetTick));
			if (this.isLoop && targetTick >= this.durationTick)
				targetTick = this.durationTick != 0 ? targetTick % this.durationTick : 0;
			int n = 0;
			int lerpTick = 0;
			AbstractPhase<T> fromPhase = null;
			AbstractPhase<T> toPhase = null;
			for (int i = 0; i < this.phaseList.Count; i++)
			{
				AbstractPhase<T> phase = this.phaseList[i];
				n += phase.durationTick;
				if (n > targetTick)
				{
					fromPhase = phase;
					toPhase = i + 1 < this.phaseList.Count ? this.phaseList[i + 1] : null;
					lerpTick = targetTick - (n - phase.durationTick);
					break;
				}
			}
			if (fromPhase == null)
			{
				fromPhase = this.phaseList[this.phaseList.Count - 1];
				toPhase = null;
				lerpTick = 0;
			}
			T result = fromPhase.Lerp(toPhase, lerpTick);
			return result;
		}

		public virtual void DoSave(Hashtable dict)
		{
			durationTick = 0;

			var phaseArrayList = phaseList.DoSaveList((phase, subDict) =>
			{
				phase.DoSave(subDict);
				durationTick += phase.durationTick;
			});

			dict["durationTick"] = durationTick;
			dict["isLoop"] = isLoop;
			dict["phaseArrayList"] = phaseArrayList;
		}

		public virtual void DoRestore(Hashtable dict)
		{
			phaseList.DoRestoreList(dict["phaseArrayList"] as ArrayList, (subDict) =>
			{
				AbstractPhase<T> phase = new AbstractPhase<T>();
				phase.DoRestore(subDict);
				return phase;
			});
			durationTick = dict["durationTick"].ToIntOrToDefault();
			isLoop = dict["isLoop"].ToBoolOrToDefault();

		}
	}
}