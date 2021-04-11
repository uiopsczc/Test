using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class PausableCoroutinePlugin
  {
    private MonoBehaviour mono;
    private IdPool idPool = new IdPool();
    private Dictionary<string, PausableCoroutine> coroutine_dict = new Dictionary<string, PausableCoroutine>();

    public PausableCoroutinePlugin(MonoBehaviour mono)
    {
      this.mono = mono;
    }

    public string StartCoroutine(IEnumerator ie, string key = null)
    {
      CleanFinishedCoroutines();
      key = key ?? idPool.Get().ToString();
      var coroutine = mono.StopAndStartCachePausableCoroutine(key.ToGuid(this), ie);
      this.coroutine_dict[key] = coroutine;
      return key;
    }

    /// <summary>
    /// 此处的key与StartCoroutine的key保持一致
    /// </summary>
    /// <param name="key"></param>
    public void StopCoroutine(string key)
    {
      CleanFinishedCoroutines();
      if (!this.coroutine_dict.ContainsKey(key))
        return;
      this.coroutine_dict.Remove(key);
      idPool.Despawn(key);
      mono.StopCachePausableCoroutine(key.ToGuid(this));

    }

    public void StopAllCoroutines()
    {
      foreach (var key in coroutine_dict.Keys)
      {
        mono.StopCachePausableCoroutine(key.ToGuid(this));
      }

      coroutine_dict.Clear();
      idPool.DespawnAll();
    }

    public void SetIsPaused(bool is_paused)
    {
      CleanFinishedCoroutines();
      foreach (var key in coroutine_dict.Keys)
      {
        this.coroutine_dict[key].SetIsPaused(is_paused);
      }
    }

    void CleanFinishedCoroutines()
    {
      coroutine_dict.RemoveByFunc<string, PausableCoroutine>((key, coroutine) =>
      {
        if (coroutine.is_finished)
        {
          idPool.Despawn(key);
          return true;
        }
        return false;
      });
    }

  }
}