using System;
using System.Collections;

namespace CsCat
{
	public class AbstractPhase<T>
	{
		protected T value;

		//持续时间
		public int durationTick;


		//lerp_tick的范围在[0, 这个this的duration_tick]之间
		public T Tween(AbstractPhase<T> toPhase, int lerpTick, Func<T, T, float, T> lerpCallback)
		{
			T fromValue = value;
			if (toPhase == null)
				return fromValue;
			T toValue = toPhase.value;
			return lerpCallback(fromValue, toValue, (float)lerpTick / durationTick);
		}

		public virtual T Lerp(AbstractPhase<T> toPhase, int lerpTick)
		{
			return default(T);
		}

		public virtual void DoSave(Hashtable dict)
		{
			dict["durationTick"] = durationTick;
		}

		public virtual void DoRestore(Hashtable dict)
		{
			durationTick = dict["durationTick"].ToIntOrToDefault();
		}
	}
}