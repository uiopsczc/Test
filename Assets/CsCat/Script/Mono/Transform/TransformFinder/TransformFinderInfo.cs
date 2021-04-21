using System;
using UnityEngine;

namespace CsCat
{
  public class TransformFinderInfo
  {
    public Type transformFinder_type;
    public string name;
    private Func<object> create_callback;

    public TransformFinderInfo(Type transformFinder_type)
    {
      this.name = transformFinder_type.GetLastName();
      this.transformFinder_type = transformFinder_type;
      this.create_callback = () => Activator.CreateInstance(transformFinder_type);
    }

    public TransformFinderBase CreateInstance()
    {
      return Activator.CreateInstance(transformFinder_type) as TransformFinderBase;
    }

    public T CreateInstance<T>() where T : TransformFinderBase
    {
      return CreateInstance() as T;
    }


  }
}




