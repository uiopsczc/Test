using System;
using System.Collections.Generic;

namespace CsCat
{
	public static class ComponentTypesUtil
	{
		public static void AddGameComponentTypes(this Entity entity, TimerManager timerManager)
		{
			if (!entity.RawHasComponentStrictly<CoroutineDictComponent>())
				entity.AddComponent<CoroutineDictComponent>(new CoroutineDict(Main.instance));
			if (!entity.RawHasComponentStrictly<DOTweenDictComponent>())
				entity.AddComponent<DOTweenDictComponent>(new DOTweenDict());
			if (!entity.RawHasComponentStrictly<PausableCoroutineDictComponent>())
				entity.AddComponent<PausableCoroutineDictComponent>(new PausableCoroutineDict(Main.instance));
			if (!entity.RawHasComponentStrictly<TimerDictComponent>())
				entity.AddComponent<TimerDictComponent>(new TimerDict(timerManager));
		}
	}
}