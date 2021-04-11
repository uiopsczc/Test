using UnityEngine;

namespace CsCat
{
  public class PauseUtil
  {
    /// <summary>
    /// 设置暂停
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="cause"></param>
    public static void SetPuase(Animator animator, object cause)
    {
      new PorpertyToRestore(cause, animator, "speed").AddToList();
      animator.speed = 0;
    }

    /// <summary>
    /// 设置暂停
    /// 暂停animator和particleSystem(包括其孩子节点)
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="cause"></param>
    public static void SetPuase(GameObject gameObject, object cause)
    {
      if (gameObject.activeInHierarchy == false)
        return;

      foreach (Animator animator in gameObject.GetComponentsInChildren<Animator>())
      {
        if (animator.enabled == true)
          animator.SetPuase(cause);
      }

      foreach (ParticleSystem ps in gameObject.GetComponentsInChildren<ParticleSystem>())
      {
        ps.SetPuase(cause);
      }
    }

    /// <summary>
    /// 设置暂停
    /// </summary>
    /// <param name="particleSystem"></param>
    /// <param name="cause"></param>
    public static void SetPuase(ParticleSystem particleSystem, object cause)
    {
      new MemberToRestoreProxy(cause, particleSystem, "Play", true);
      particleSystem.Pause();
    }
  }
}