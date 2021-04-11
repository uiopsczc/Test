
using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public class TestScriptableObjectBB<T1,T2>
  {
    public T1 street;
    public T2 age;

    public TestScriptableObjectBB(T1 t1,T2 t2)
    {
      this.street = t1;
      this.age = t2;
    }

    public override string ToString()
    {
      return this.street.ToString()+ this.age.ToString();
    }
  }
}



