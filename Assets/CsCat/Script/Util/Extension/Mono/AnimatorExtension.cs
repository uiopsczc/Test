using UnityEngine;

namespace CsCat
{
    public static class AnimatorExtension
    {
        /// <summary>
        /// 获取Animator中指定name的AnimationClip
        /// </summary>
        /// <param name="self"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AnimationClip GetAnimationClip(this Animator self, string name)
        {
            return AnimatorUtil.GetAnimationClip(self, name);
        }

//    public static EventName GetEventName(this Animator self)
//    {
//      return typeof(StateMachineBehaviourEvents).Name.ToEventName(self);
//    }

        public static T GetBehaviour<T>(this Animator self, string name) where T : StateMachineBehaviour
        {
            foreach (var behaviour in self.GetBehaviours<T>())
            {
                if (name.Equals(behaviour.GetFieldValue<string>(StringConst.String_name)))
                    return behaviour;
            }

            return null;
        }


        /// <summary>
        /// 设置暂停
        /// </summary>
        /// <param name="self"></param>
        /// <param name="cause"></param>
        public static void SetPause(this Animator self, object cause)
        {
            PauseUtil.SetPause(self, cause);
        }

        public static float SetTriggerExt(this Animator self, string triggerName)
        {
            self.SetTrigger(triggerName);
            self.Update(0);
            return self.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }
    }
}