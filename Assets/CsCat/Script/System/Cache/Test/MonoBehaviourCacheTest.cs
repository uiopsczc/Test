using UnityEngine;

namespace CsCat
{
  public class CacheTest : MonoBehaviour
  {
    private void Start()
    {
      LogCat.LogError(monoBehaviourCache.transform);
    }

    #region property

    private MonoBehaviourCache _monoBehaviourCache;

    public MonoBehaviourCache monoBehaviourCache
    {
      get
      {
        if (_monoBehaviourCache == null) _monoBehaviourCache = new MonoBehaviourCache(this);
        return _monoBehaviourCache;
      }
    }

    #endregion
  }
}