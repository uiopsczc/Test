using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class EffectComponent : GameComponent
  {
    public EffectEntity effectEntity => this.GetEntity<EffectEntity>();


    protected override void __Destroy()
    {
      base.__Destroy();
    }
  }
}


