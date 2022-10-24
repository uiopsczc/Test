using System;

namespace CsCat
{
	public interface IPoolCat
	{
		void Destroy();
		void DespawnAll();

		void SetPoolManager(PoolCatManager poolManager);

		PoolCatManager GetPoolManager();
	}
}