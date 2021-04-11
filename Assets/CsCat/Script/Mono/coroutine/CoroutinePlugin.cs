using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class CoroutinePlugin
  {
    private MonoBehaviour mono;
    private IdPool idPool = new IdPool();
    private Dictionary<string, IEnumerator> coroutine_dict = new Dictionary<string, IEnumerator>();

    public CoroutinePlugin(MonoBehaviour mono)
    {
      this.mono = mono;
    }


    public string StartCoroutine(IEnumerator ie, string key = null)
    {
      key = key?? idPool.Get().ToString();
      this.coroutine_dict[key] = ie;
      mono.StopAndStartCacheIEnumerator(key.ToGuid(this), ie);
      return key;
    }

    /// <summary>
    /// 此处的key与StartCoroutine的key保持一致
    /// </summary>
    /// <param name="key"></param>
    public void StopCoroutine(string key)
    {
      if (!this.coroutine_dict.ContainsKey(key))
        return;
      this.coroutine_dict.Remove(key);
      idPool.Despawn(key);
      mono.StopCacheIEnumerator(key.ToGuid(this));

    }

    public void StopAllCoroutines()
    {
      foreach (var key in coroutine_dict.Keys)
      {
        mono.StopCacheIEnumerator(key.ToGuid(this));
      }

      coroutine_dict.Clear();
      idPool.DespawnAll();
    }



  }
}