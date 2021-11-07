using System;
using System.Collections.Generic;

namespace CsCat
{
    public partial class AbstractEntity
    {
        public virtual bool IsCanUpdate()
        {
            return isEnabled && !this.is_paused && !IsDestroyed();
        }
    }
}