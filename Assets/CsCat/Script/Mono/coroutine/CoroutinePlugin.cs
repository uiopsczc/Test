using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class CoroutinePlugin
  {
    public MonoBehaviour mono;
    private IdPool idPool = new IdPool();
    private Dictionary<string, IEnumerator> dict = new Dictionary<string, IEnumerator>();

    public CoroutinePlugin(MonoBehaviour mono)
    {
      this.mono = mono;
    }


    public string StartCoroutine(IEnumerator ie, string key = null)
    {
      key = key ?? idPool.Get().ToString();
      this.dict[key] = ie;
      mono.StopAndStartCacheIEnumerator(key.ToGuid(this), ie);
      return key;
    }

    /// <summary>
    /// 此处的key与StartCoroutine的key保持一致
    /// </summary>
    /// <param name="key"></param>
    public void StopCoroutine(string key)
    {
      if (!this.dict.ContainsKey(key))
        return;
      this.dict.Remove(key);
      idPool.Despawn(key);
      mono.StopCacheIEnumerator(key.ToGuid(this));

    }

    public void StopAllCoroutines()
    {
      foreach (var key in dict.Keys)
      {
        mono.StopCacheIEnumerator(key.ToGuid(this));
      }

      dict.Clear();
      idPool.DespawnAll();
    }



  }
}