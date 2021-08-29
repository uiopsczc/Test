

using System;

namespace CsCat
{
  //特点：
  //如果没有despawn的，则由current_number自动增加1，返回该值，否则取despawn中的
  public class IdPool : PoolCat
  {
    private ulong current_number = 0L;
    public IdPool(string pool_name = "") : base(pool_name, typeof(ulong))
    {
    }

    protected override object __Spawn()
    {
      current_number++;
      return current_number;
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
