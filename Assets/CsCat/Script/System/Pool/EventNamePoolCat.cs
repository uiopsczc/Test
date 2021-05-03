using System;
using System.Collections.Generic;

namespace CsCat
{
  public class EventNamePoolCat:PoolCat
  {
    public EventNamePoolCat():base(PoolNameConst.EventName, typeof(EventName))
    {
    }
    protected override object __Spawn()
    {
      return new EventName();
    }

    
  }
}