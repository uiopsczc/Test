using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
  public partial class GameComponent
  {
    protected DOTweenPlugin dotweenPlugin = new DOTweenPlugin();

    public Sequence AddDOTweenSequence(string key)
    {
      return dotweenPlugin.AddDOTweenSequence(key);
    }

    public Tween AddDOTween(string key, Tween tween)
    {
      return dotweenPlugin.AddDOTween(key, tween);
    }

    public void RemoveDOTween(string key)
    {
      dotweenPlugin.RemoveDOTween(key);
    }

    public void RemoveDOTween(Tween tween)
    {
      dotweenPlugin.RemoveDOTween(tween);
    }

    public void RemoveAllDOTweens()
    {
      dotweenPlugin.RemoveAllDOTweens();
    }

    public void SetIsPaused_DOTweens(bool is_paused)
    {
      dotweenPlugin.SetIsPaused(is_paused);
    }
    
  }
}