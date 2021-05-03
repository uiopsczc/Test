using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
  public class CustomPoolCat : PoolCat
  {
    private Func<object> spawn_func;
    public CustomPoolCat(string pool_name, Func<object> spawn_func) : base(pool_name, (Type)null)
    {
      this.spawn_func = spawn_func;
    }

    protected override object __Spawn()
    {
      return this.spawn_func();
    }

  }

}