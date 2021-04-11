#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
  /// <summary>
  /// AssetDatabase 需要包含后缀名称   path是相对于项目目录  如：Assets/xxxx
  /// </summary>
  public static class UnityEngineObjectExtension2
  {
    public static bool IsNull(this Object o)
    {
      return o == null;
    }

    public static void Destroy(this Object self)
    {
      UnityObjectUtil.Destroy(self);
    }
  }

}