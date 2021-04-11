using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
  public partial class GameEntity
  {
    

    public string StartPausableCoroutine(IEnumerator ie, string key = null)
    {
      return this.pausableCoroutinePluginComponent.StartCoroutine(ie, key);
    }

    /// <summary>
    /// 此处的key与StartCoroutine的key保持一致
    /// </summary>
    /// <param name="key"></param>
    public void StopPausableCoroutine(string key)
    {
      this.pausableCoroutinePluginComponent.StopCoroutine(key);
    }

    public void StopAllPausableCoroutines()
    {
      this.pausableCoroutinePluginComponent.StopAllCoroutines();
    }
  }
}