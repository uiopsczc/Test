using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiGraphicButton : Button
{
  private Graphic[] _graphics;
  protected Dictionary<Graphic, Color> graphic_orgin_color_dict = new Dictionary<Graphic, Color>();


  protected Graphic[] graphics
  {
    get
    {
      if (_graphics != null) return _graphics;
      _graphics = targetGraphic.transform.parent.GetComponentsInChildren<Graphic>();
      foreach (var graphic in graphics)
        graphic_orgin_color_dict[graphic] = graphic.color;
      return _graphics;
    }
  }

  protected override void DoStateTransition(SelectionState state, bool instant)
  {
    Color? color = null;
    switch (state)
    {
      case Selectable.SelectionState.Normal:
        color = this.colors.normalColor;
        break;
      case Selectable.SelectionState.Highlighted:
        color = this.colors.highlightedColor;
        break;
      case Selectable.SelectionState.Pressed:
        color = this.colors.pressedColor;
        break;
      case Selectable.SelectionState.Disabled:
        color = this.colors.disabledColor;
        break;
      case Selectable.SelectionState.Selected:
        color = this.colors.selectedColor;
        break;
    }
    if (base.gameObject.activeInHierarchy)
    {
      switch (this.transition)
      {
        case Selectable.Transition.ColorTint:
          ColorTween(color, this.colors.colorMultiplier, instant);
          break;
        default:
          throw new NotSupportedException();
      }
    }
  }

  private void ColorTween(Color? targetColor, float colorMultiplier, bool instant)
  {
    if (this.targetGraphic == null)
      return;

    Color final_color;
    foreach (Graphic graphic in this.graphics)
    {
      if (targetColor == null)
        final_color = graphic_orgin_color_dict[graphic];
      else
        final_color = targetColor.Value * colorMultiplier;
      graphic.CrossFadeColor(final_color, (!instant) ? this.colors.fadeDuration : 0f, true, true);
    }
  }


}
