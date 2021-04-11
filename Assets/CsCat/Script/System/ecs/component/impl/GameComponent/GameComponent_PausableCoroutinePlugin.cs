using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
  public partial class GameComponent
  {
    protected PausableCoroutinePlugin pausableCoroutinePlugin
    {
      get { return cache.GetOrAddDefault(() => { return new PausableCoroutinePlugin(Main.instance); }); }
    }
    

    public string StartPausableCoroutine(IEnumerator ie, string key = null)
    {
      return pausableCoroutinePlugin.StartCoroutine(ie, key);
    }

    /// <summary>
    /// 此处的key与StartCoroutine的key保持一致
    /// </summary>
    /// <param name="key"></param>
    public void StopPausableCoroutine(string key)
    {
      pausableCoroutinePlugin.StopCoroutine(key);
    }

    public void StopAllPausableCoroutines()
    {
      pausableCoroutinePlugin.StopAllCoroutines();
    }

    public void SetIsPaused_PausableCoroutines(bool is_paused)
    {
      pausableCoroutinePlugin.SetIsPaused(is_paused);
    }
    

  }
}