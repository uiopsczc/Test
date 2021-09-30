
using System.Collections.Generic;
using DG.Tweening;
namespace CsCat
{
  public class DOTweenPlugin
  {
    Dictionary<string,Tween> dict = new Dictionary<string, Tween>();
    private IdPool idPool = new IdPool();

    public Sequence AddDOTweenSequence(string key)
    {
      CleanNotActiveDOTweens();
      if (key != null && dict.ContainsKey(key))
        RemoveDOTween(key);
      key = key ?? idPool.Get().ToString();
      var sequence = DOTween.Sequence();
	    dict[key] = sequence;
      return sequence;
    }

    public Tween AddDOTween(string key, Tween tween)
    {
      CleanNotActiveDOTweens();
      if (key != null && dict.ContainsKey(key))
        RemoveDOTween(key);
      key = key ?? idPool.Get().ToString();
	    dict[key] = tween;
      return tween;
    }

    public void RemoveDOTween(string key)
    {
      CleanNotActiveDOTweens();
      if (dict.ContainsKey(key))
      {
        Tween tween = dict[key];
        if (tween.IsActive())
	        dict[key].Kill();
        idPool.Despawn(key);
	      dict.Remove(key);
      }
    }

    public void RemoveDOTween(Tween tween)
    {
      string key = null;
      foreach (var dictKey in dict.Keys)
      {
        if (dict[dictKey] == tween)
        {
          key = dictKey;
          break;
        }
      }
      if (key != null)
        RemoveDOTween(key);
    }

    private void CleanNotActiveDOTweens()
    {
	    dict.RemoveByFunc((_key, _tween) =>
      {
        if (!_tween.IsActive())
        {
          idPool.Despawn(_key);
          return true;
        }
        return false;
      });
    }

    public void SetIsPaused(bool is_paused)
    {
      foreach (var tween in dict.Values)
      {
        if (!tween.IsActive())
          continue;
        if (is_paused)
          tween.Pause();
        else
          tween.Play();
      }
    }

    public void RemoveAllDOTweens()
    {
      foreach (var tween in dict.Values)
      {
        if (tween.IsActive())
          tween.Kill();
      }

	    dict.Clear();
      idPool.DespawnAll();
    }

    public void Destroy()
    {
      this.RemoveAllDOTweens();
    }
  }
}
