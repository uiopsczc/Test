using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class RedDotManager
  {
    public class ListenInnerDict
    {
      public EventListenerInfo listener;

      public Dictionary<GameObject, RedDotManager.RedDotInfo> red_dot_info_dict =
        new Dictionary<GameObject, RedDotManager.RedDotInfo>();
    }
  }
}