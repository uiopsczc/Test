using UnityEngine;

namespace CsCat
{
  public class InvokeMethodMonoTmp : MonoBehaviour
  {
    public void Hello(string s)
    {
      LogCat.LogError("hello " + s);
    }
  }
}