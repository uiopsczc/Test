using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    public virtual bool IsCanUpdate()
    {
      return is_enabled&&!this.is_paused && !IsDestroyed();
    }
  }
}