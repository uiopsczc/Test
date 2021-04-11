#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class HandlesUtil
  {
    public static HandlesColorScope Color(Color color_new)
    {
      return new HandlesColorScope(color_new);
    }
  }
}
#endif