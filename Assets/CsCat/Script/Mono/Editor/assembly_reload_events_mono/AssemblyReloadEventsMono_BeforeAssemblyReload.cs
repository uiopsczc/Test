using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class AssemblyReloadEventsMono
  {
    static void BeforeAssemblyReload()
    {
      PausableCoroutineManager.instance.gameObject.Destroy();
    }



  }



}



