
using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public class TestScriptableObjectBBStringInt : TestScriptableObjectBB<string, int>
  {
    public TestScriptableObjectBBStringInt(string t1, int t2) : base(t1, t2)
    {
    }
  }
}



