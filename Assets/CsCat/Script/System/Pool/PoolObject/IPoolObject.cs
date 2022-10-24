using System;
using System.Collections.Generic;

namespace CsCat
{
	public interface IPoolObject
	{
		void Despawn();

		PoolObjectIndex GetPoolObjectIndex();
	}
}