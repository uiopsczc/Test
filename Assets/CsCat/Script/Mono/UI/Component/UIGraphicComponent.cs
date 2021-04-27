using System;
using UnityEngine;

namespace CsCat
{
  public partial class UIGraphicComponent: GraphicComponent
  {
    public override void SetGameObject(GameObject gameObject, bool? is_not_destroy_gameObject)
    {
      base.SetGameObject(gameObject, is_not_destroy_gameObject);
      ((UIObject)this.entity).Open();
    }
  }
}