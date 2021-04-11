using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class TransformFinderBase:ICopyable
  {
    public TransformFinderBase()
    {
    }


    public virtual Transform Find(params  object[] args)
    {
      return null;
    }

    public virtual void CopyTo(object dest)
    {
    }

    public virtual void CopyFrom(object source)
    {
    }
  }
}




