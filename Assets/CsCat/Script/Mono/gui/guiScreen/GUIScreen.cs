using System;
using UnityEngine;

namespace CsCat
{
  class GUIScreen : MonoBehaviour
  {
    public static Action action;

    void OnGUI()
    {
      action?.Invoke();
      LogCat.Flush_GUI();
    }

    void Awake()
    {
      DontDestroyOnLoad(this.gameObject);
    }
  }
}
