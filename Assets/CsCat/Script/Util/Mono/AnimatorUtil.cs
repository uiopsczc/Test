using UnityEngine;

namespace CsCat
{
  public class AnimatorUtil
  {
    /// <summary>
    /// 获取Animator中指定name的AnimationClip
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AnimationClip GetAnimationClip(Animator animator, string name)
    {
      foreach (AnimationClip animationClip in animator.runtimeAnimatorController.animationClips)
      {
        if (animationClip.name == name)
          return animationClip;
      }

      return null;
    }


    public static string GetAnimatorStateLayerName(string state_name, string separator = ".")
    {
      if (state_name.IndexOf(separator) != -1)
        return state_name.Substring(0, state_name.IndexOf(separator));
      return null;
    }

    public static string GetAnimatorStateLastName(string state_name, string separator = ".")
    {
      if (state_name.IndexOf(separator) != -1)
        return state_name.Substring(state_name.LastIndexOf(separator) + separator.Length);
      return state_name;
    }

    public static bool IsName(AnimatorStateInfo animatorStateInfo, string prefix, params string[] suffixs)
    {
      if (animatorStateInfo.IsName(prefix))
        return true;
      foreach (string suffix in suffixs)
      {
        string name = prefix + suffix;
        if (animatorStateInfo.IsName(name))
        {
          return true;
        }
      }

      return false;
    }


  }
}