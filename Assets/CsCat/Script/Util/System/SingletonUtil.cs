using UnityEngine;

namespace CsCat
{
    public class SingletonUtil
    {
        /// <summary>
        /// Mono类的单例调用这里
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T GetInstnaceMono<T>(T instance) where T : MonoBehaviour, ISingleton
        {
            if (instance != null) return instance;
            //检查场景有效的物体中是否有名为(Singleton)xxx【xxx为T的类名】
            string targetName = string.Format(SingletonConst.StringSingleFormat, typeof(T).GetLastName());
            GameObject instanceGameObject = GameObject.Find(targetName);
            if (instanceGameObject != null)
            {
                instance = instanceGameObject.GetComponent<T>();
                instance.SingleInit();
                return instance;
            }

            if (GameObject.Find(SingletonConst.StringSingletonMaster))
            {
                //检测失效物体中是否有名为(Singleton)xxx【xxx为T的类名】
                foreach (GameObject inActiveGameObject in SingletonFactory.instance.GetMono<SingletonMaster>()
                    .inActiveGameObjects)
                {
                    if (!inActiveGameObject.name.Equals(targetName)) continue;
                    instance = inActiveGameObject.GetComponent<T>();
                    instance.SingleInit();
                    return instance;
                }
            }

            //如果都没有，新建一个
            instanceGameObject = new GameObject();
            instanceGameObject.name = targetName;
            instance = instanceGameObject.AddComponent<T>();
            instance.SingleInit();
            return instance;
        }

        /// <summary>
        /// 非Mono类的单例调用这里
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T GetInstnace<T>(T instance) where T : ISingleton, new()
        {
            if (instance != null) return instance;
            instance = new T();
            instance.SingleInit();

            return instance;
        }
    }
}