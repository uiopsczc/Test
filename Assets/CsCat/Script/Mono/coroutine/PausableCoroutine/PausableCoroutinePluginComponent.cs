using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class PausableCoroutinePluginComponent : AbstractComponent
  {
    public PausableCoroutinePlugin pausableCoroutinePlugin;
    public void Init(PausableCoroutinePlugin pausableCoroutinePlugin)
    {
      base.Init();
      this.pausableCoroutinePlugin = pausableCoroutinePlugin;
    }

    public string StartCoroutine(IEnumerator ie, string key = null)
    {
      return pausableCoroutinePlugin.StartCoroutine(ie, key);
    }

    /// <summary>
    /// 此处的key与StartCoroutine的key保持一致
    /// </summary>
    /// <param name="key"></param>
    public void StopCoroutine(string key)
    {
      pausableCoroutinePlugin.StopCoroutine(key);
    }

    public void StopAllCoroutines()
    {
      pausableCoroutinePlugin.StopAllCoroutines();
    }

    protected override void __SetIsPaused(bool is_paused)
    {
      base.__SetIsPaused(is_paused);
      pausableCoroutinePlugin.SetIsPaused(is_paused);
    }

    protected override void __Reset()
    {
      base.__Reset();
      StopAllCoroutines();
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      StopAllCoroutines();
    }
  }
}