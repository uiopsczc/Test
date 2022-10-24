using System;

namespace CsCat
{
	public static partial class PoolCatManagerUtil
	{
		public static T SpawnScope<T>(PoolCatManager poolCatManager, Action<T> onSpawnCallback = null) where T : PoolScope
		{
			return poolCatManager.Spawn(null, null, onSpawnCallback).GetValue();
		}
	}
}