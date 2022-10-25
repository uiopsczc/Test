using System;

namespace CsCat
{
	//特点：
	//如果没有despawn的，则由currentNumber自动增加1，返回该值，否则取deSpawn中的
	public class IdPool : PoolCat<ulong>
	{
		private ulong currentNumber = 0L;

		public IdPool(string poolName = StringConst.String_Empty) : base(poolName)
		{
		}

		protected override ulong _Spawn()
		{
			currentNumber++;
			return currentNumber;
		}

		public PoolItem<ulong> Get()
		{
			return this.Spawn(null);
		}
	}
}