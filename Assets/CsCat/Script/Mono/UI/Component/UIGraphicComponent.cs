using System;
using UnityEngine;

namespace CsCat
{
  public partial class UIGraphicComponent: GraphicComponent
  {
    public override void SetGameObject(GameObject gameObject, bool? isNotDestroyGameObject)
    {
      base.SetGameObject(gameObject, isNotDestroyGameObject);
      ((UIObject)this.entity).Open();
    }
  }
}