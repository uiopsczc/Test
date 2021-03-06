using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
  public partial class GameComponent
  {
    protected CoroutinePlugin coroutinePlugin
    {
      get { return cache.GetOrAddDefault(() => { return new CoroutinePlugin(Main.instance); }); }
    }


    public string StartCoroutine(IEnumerator ie, string key = null)
    {
      return coroutinePlugin.StartCoroutine(ie, key);
    }

    /// <summary>
    /// 此处的key与StartCoroutine的key保持一致
    /// </summary>
    /// <param name="key"></param>
    public void StopCoroutine(string key)
    {
      coroutinePlugin.StopCoroutine(key);
    }

    public void StopAllCoroutines()
    {
      coroutinePlugin.StopAllCoroutines();
    }

  }
}