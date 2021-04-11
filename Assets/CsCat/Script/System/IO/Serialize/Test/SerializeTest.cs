using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class SerializeTest : MonoBehaviour
  {
    public void Start()
    {
      //TestSave();
      TestLoad();
    }

    void TestSave()
    {
      List<int> listA = new List<int> {101, 102, 103};
      List<int> listB = new List<int> {205, 206, 207};
      SerializeTestData.instance.serializeTestSubData.nameDictList["a"] = listA;
      SerializeTestData.instance.serializeTestSubData.nameDictList["b"] = listB;
      SerializeTestData.instance.Save();
    }

    void TestLoad()
    {
      Debug.LogError(SerializeTestData.instance.serializeTestSubData.nameDictList.ToString2());
    }


  }
}




