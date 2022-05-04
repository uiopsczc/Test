using System;

namespace CsCat
{
	public interface IPoolCat
	{
		void Destroy();
		IPoolObject Spawn();
		IPoolObject GetPoolObjectAtIndex(int index);
		void DeSpawnAll();
		void DeSpawn(IPoolObject poolObject);
	}
}