using System;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

namespace CsCat
{
    public static partial class PoolCatManagerUtil
    {
        public static T SpawnScope<T>(Action<T> onSpawnCallback = null) where T : PoolScope
        {
            return Spawn(null, onSpawnCallback);
        }
    }
}