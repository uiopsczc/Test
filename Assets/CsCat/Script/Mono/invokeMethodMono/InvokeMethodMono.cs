using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class InvokeMethodMono : MonoBehaviour
  {
    private MonoBehaviourCache _monoBehaviourCache;
    public bool is_found_child; //当前transform中找不到的时候，【true：查找子节点】 【false：查找父节点】
    public string target_gameObject_name; //目标节点的名称，不写则没限制
    [HideInInspector] public string target_methodArgs_json_string = "";
    [HideInInspector] public string target_methodInfo_name;
    [HideInInspector] public string target_type_name;


    public MonoBehaviourCache monoBehaviourCache => _monoBehaviourCache ?? (_monoBehaviourCache = new MonoBehaviourCache(this));
    public Component target_component
    {
      get
      {
        return monoBehaviourCache.GetOrAddDefault(" targetComponent",
          () =>
          {
            var type = GetType().Assembly.GetType(target_type_name);
            var result = transform.GetComponent(type);
            if (result != null &&
                (target_gameObject_name.IsNullOrWhiteSpace() || target_gameObject_name.Equals(gameObject.name)))
              return result;
            if (target_gameObject_name.IsNullOrWhiteSpace())
              return is_found_child ? transform.GetComponentInChildren(type) : transform.GetComponentInParent(type);

            return is_found_child ? transform.FindComponentInChildren<Transform>(name, true, false).GetComponent(type) : transform.FindComponentInParent<Transform>(name, false).GetComponent(type);
          });
      }
    }

    public void Invoke()
    {
      var args = (List<object>)JsonSerializer.Deserialize(target_methodArgs_json_string);
      args = args ?? new List<object>();
      target_component.InvokeMethod(target_methodInfo_name, false, args.ToArray());
    }
  }
}