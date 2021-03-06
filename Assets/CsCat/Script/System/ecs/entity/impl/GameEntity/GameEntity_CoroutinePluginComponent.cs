using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
  public partial class GameEntity
  {


    public string StartCoroutine(IEnumerator ie, string key = null)
    {
      return coroutinePluginComponent.StartCoroutine(ie, key);
    }

    /// <summary>
    /// 此处的key与StartCoroutine的key保持一致
    /// </summary>
    /// <param name="key"></param>
    public void StopCoroutine(string key)
    {
      coroutinePluginComponent.StopCoroutine(key);
    }

    public void StopAllCoroutines()
    {
      coroutinePluginComponent.StopAllCoroutines();
    }

  }
}