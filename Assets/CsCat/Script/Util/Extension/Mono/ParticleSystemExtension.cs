using UnityEngine;

namespace CsCat
{
  public static class ParticleSystemExtensions
  {
    /// <summary>
    /// 设置暂停
    /// </summary>
    /// <param name="self"></param>
    /// <param name="cause"></param>
    public static void SetPuase(this ParticleSystem self, object cause)
    {
      PauseUtil.SetPuase(self, cause);
    }
  }
}