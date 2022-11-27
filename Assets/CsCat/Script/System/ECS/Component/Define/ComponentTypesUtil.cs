using System;
using System.Collections.Generic;

namespace CsCat
{
	public static class ComponentTypesUtil
	{
		public static void AddGameComponentTypes(this Entity entity, TimerManager timerManager)
		{
			if (!entity.IsRawHasComponentStrictly<CoroutineDictComponent>())
				entity.AddComponent<CoroutineDictComponent>(new CoroutineDict(Main.instance));
			if (!entity.IsRawHasComponentStrictly<DOTweenDictComponent>())
				entity.AddComponent<DOTweenDictComponent>(new DOTweenDict());
			if (!entity.IsRawHasComponentStrictly<PausableCoroutineDictComponent>())
				entity.AddComponent<PausableCoroutineDictComponent>(new PausableCoroutineDict(Main.instance));
			if (!entity.IsRawHasComponentStrictly<TimerDictComponent>())
				entity.AddComponent<TimerDictComponent>(new TimerDict(timerManager));
		}
	}
}