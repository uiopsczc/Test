using System;

namespace CsCat
{
	//特点：
	//如果没有despawn的，则由current_number自动增加1，返回该值，否则取despawn中的
	public class IdPool : PoolCat
	{
		private ulong currentNumber = 0L;

		public IdPool(string poolName = StringConst.String_Empty) : base(poolName, typeof(ulong))
		{
		}

		protected override object _Spawn()
		{
			currentNumber++;
			return currentNumber;
		}

		public ulong Get()
		{
			return this.Spawn<ulong>();
		}

		public void Despawn(ulong n)
		{
			base.Despawn(n);
		}

		public void Despawn(string n)
		{
			if (ulong.TryParse(n, out var id))
				Despawn(id);
		}
	}
}