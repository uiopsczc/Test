using UnityEngine;

namespace CsCat
{
  public class InvokeMethodMonoTest : MonoBehaviour
  {
    void Start()
    {
      this.transform.GetComponent<InvokeMethodMono>().Invoke();
    }
  }
}