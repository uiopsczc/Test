using UnityEngine;

namespace CsCat
{
  public class SingletonUtil
  {
    /// <summary>
    /// Mono类的单例调用这里
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_instance"></param>
    /// <returns></returns>
    public static T GetInstnaceMono<T>(T _instance) where T : MonoBehaviour, ISingleton
    {

      if (_instance == null)
      {
        //检查场景有效的物体中是否有名为(Singleton)xxx【xxx为T的类名】
        string targetName = string.Format("(Singleton){0}", typeof(T).GetLastName().ToString());
        GameObject instance_gameObject = GameObject.Find(targetName);
        if (instance_gameObject != null)
        {
          _instance = instance_gameObject.GetComponent<T>();
          _instance.InvokeMethod(SingletonConst.SingleInit_Method_Name);
          return _instance;
        }

        if (GameObject.Find("(Singleton)SingletonMaster"))
        {
          //检测失效物体中是否有名为(Singleton)xxx【xxx为T的类名】
          foreach (GameObject inActive_gameObject in SingletonFactory.instance.GetMono<SingletonMaster>()
            .inActive_gameObjects)
          {
            if (inActive_gameObject.name.Equals(targetName))
            {
              _instance = inActive_gameObject.GetComponent<T>();
              _instance.InvokeMethod(SingletonConst.SingleInit_Method_Name);
              return _instance;
            }

          }
        }

        //如果都没有，新建一个
        instance_gameObject = new GameObject();
        instance_gameObject.name = targetName;
        _instance = instance_gameObject.AddComponent<T>();
        _instance.InvokeMethod(SingletonConst.SingleInit_Method_Name);
        return _instance;

      }

      return _instance;
    }

    /// <summary>
    /// 非Mono类的单例调用这里
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_instance"></param>
    /// <returns></returns>
    public static T GetInstnace<T>(T _instance) where T : ISingleton, new()
    {
      if (_instance == null)
      {
        _instance = new T();
        _instance.InvokeMethod(SingletonConst.SingleInit_Method_Name);
      }

      return _instance;
    }
  }

}

