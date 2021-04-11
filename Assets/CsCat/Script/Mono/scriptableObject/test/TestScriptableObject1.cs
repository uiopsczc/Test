
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class TestScriptableObject1:ScriptableObject
  {
    [NonSerialized]
    public List<TestScriptableObjectAA> indexes = new List<TestScriptableObjectAA>();
    public new string name;
  }
}



