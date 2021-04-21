using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class TransformFinder0 : TransformFinderBase
  {
    public string path;

    public override Transform Find(params object[] args)
    {
      var root_transform = args[0] as Transform;
      if (root_transform == null)
      {
        if (path.IsNullOrEmpty())
          return null;
        var gameObject = GameObject.Find(path);
        return gameObject == null ? null : gameObject.transform;
      }
      else
      {
        if (path.IsNullOrEmpty())
          return root_transform;
        var transform = root_transform.FindComponentInChildren<Transform>(path);
        return transform;
      }
    }

    public override void CopyTo(object dest)
    {
      var _dest = dest as TransformFinder0;
      _dest.path = path;
      base.CopyTo(dest);
    }

    public override void CopyFrom(object source)
    {
      var _source = source as TransformFinder0;
      path = _source.path;
      base.CopyFrom(_source);
    }
  }
}




