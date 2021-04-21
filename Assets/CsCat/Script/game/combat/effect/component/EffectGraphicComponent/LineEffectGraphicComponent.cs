using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  //弹道
  public class LineEffectGraphicComponent : EffectGraphicComponent
  {
    private List<XLineRenderer> xlineRenderer_list = new List<XLineRenderer>();

    public override void OnAllAssetsLoadDone()
    {
      base.OnAllAssetsLoadDone();
      foreach (var xlineRenderer in transform.GetComponentsInChildren<XLineRenderer>())
        xlineRenderer_list.Add(xlineRenderer);
    }

    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      transform.position = this.effectEntity.transformComponent.position;
      transform.eulerAngles = this.effectEntity.transformComponent.eulerAngles;
      foreach (var line in xlineRenderer_list)
      {
        line.target.position = effectEntity.GetComponent<LineEffectComponent>().target_position;
        line.target.eulerAngles = this.effectEntity.transformComponent.eulerAngles;
      }
    }


  }
}


