using System;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

namespace CsCat
{
    public static partial class PoolCatManagerUtil
    {
        public static T SpawnScope<T>(Action<T> on_spawn_callback = null) where T : PoolScope
        {
            return Spawn(null, on_spawn_callback);
        }
    }
}