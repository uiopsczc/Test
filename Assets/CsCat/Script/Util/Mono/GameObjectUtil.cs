using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class GameObjectUtil
  {
    /// <summary>
    ///   有T返回T，没T添加T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static Component GetOrAddComponent(GameObject gameObject, Type type)
    {
      if (gameObject == null) return null;
      var component = gameObject.GetComponent(type);
      if (component == null)
        component = gameObject.AddComponent(type);
      return component;
    }

    public static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
    {
      return GetOrAddComponent(gameObject, typeof(T)) as T;
    }

    /// <summary>
    ///   使某个类型的组件enable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <param name="is_enable"></param>
    public static void EnableComponents(GameObject gameObject, Type type, bool is_enable)
    {
      var components = gameObject.GetComponents(type);
      if (components == null) return;
      var num = components.Length;
      for (var i = 0; i < num; i++) ((MonoBehaviour)components[i]).enabled = is_enable;
    }

    public static void EnableComponents<T>(GameObject gameObject, bool is_enable) where T : MonoBehaviour
    {
      EnableComponents(gameObject, typeof(T), is_enable);
    }

    /// <summary>
    ///   销毁子孩子节点
    /// </summary>
    /// <param name="gameObject"></param>
    public static void DestroyChildren(GameObject gameObject)
    {
      if (gameObject == null) return;
      var transform = gameObject.transform;
      if (transform == null) return;
      var num = transform.childCount;
      while (--num >= 0)
        transform.GetChild(num).gameObject.Destroy();
    }


    /// <summary>
    ///   只有包含全部的Components才会返回True
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    public static bool IsHasComponents(GameObject gameObject, params Type[] types)
    {
      foreach (var type in types)
        if (!gameObject.GetComponent(type))
          return false;
      return true;
    }

    /// <summary>
    ///   获取该gameObject下的组件，不包括剔除的组件类型
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="exclude_component_types">剔除的组件类型</param>
    /// <returns></returns>
    public static Component[] GetComponentsExclude(GameObject gameObject, params Type[] exclude_component_types)
    {
      var result = new List<Component>();
      foreach (var component in gameObject.GetComponents<Component>())
      {
        if (exclude_component_types.Length > 0) //如果剔除的类型个数不为0
        {
          var is_continue_this_round = false; //是否结束这个round
          foreach (var exclude_component_type in exclude_component_types)
            if (component.GetType().IsSubTypeOf(exclude_component_type)) //如果是组件类型是其中的剔除的类型或其子类
            {
              is_continue_this_round = true;
              break;
            }

          if (is_continue_this_round)
            continue;
        }

        result.Add(component);
      }

      return result.ToArray();
    }

    /// <summary>
    ///   获取该gameObject下的组件，不包括剔除的组件类型
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="exclude_component_types">剔除的组件类型</param>
    /// <param name="exclude_separator"></param>
    /// <returns></returns>
    public static Component[] GetComponentsExclude(GameObject gameObject, string exclude_component_types,
      string exclude_separator = "|")
    {
      var exclude_type_list = new List<Type>();
      if (!string.IsNullOrEmpty(exclude_component_types))
      {
        var exclude_component_type_list = exclude_component_types.ToList<string>(exclude_separator);
        foreach (var exclude_component_type in exclude_component_type_list)
          //只查当前的assembly和UnityEngine两个Assembly
          if (TypeUtil.GetType(exclude_component_type) != null)
            exclude_type_list.Add(TypeUtil.GetType(exclude_component_type));
          else if (TypeUtil.GetType(exclude_component_type, "UnityEngine") != null)
            exclude_type_list.Add(TypeUtil.GetType(exclude_component_type, "UnityEngine"));
      }

      return gameObject.GetComponentsExclude(exclude_type_list.ToArray());
    }

    public static GameObject GetOrNewGameObject(string path, GameObject parent_gameObject)
    {

      if (parent_gameObject == null)
      {
        var result = GameObject.Find(path);
        if (result != null)
          return result;
      }
      else
      {
        var result = parent_gameObject.transform.Find(path);
        if (result != null)
          return result.gameObject;
      }
      string name = path.GetPreString("/");
      GameObject gameObject = new GameObject(name);
      gameObject.name = name;
      if (parent_gameObject != null)
        gameObject.transform.ResetToParent(parent_gameObject.transform);
      if (!name.Equals(path))
        return GetOrNewGameObject(path.GetPostString("/"), gameObject);
      else
        return gameObject;
    }


    #region GameObject 反射

    #region FiledValue

    public static T GetFieldValue<T>(GameObject gameObject, string fieldInfo_string, T defalut_value,
      params Type[] exclude_component_types)
    {
      foreach (var component in gameObject.GetComponentsExclude(exclude_component_types))
      {
        var fieldInfo = component.GetType().GetFieldInfo(fieldInfo_string);
        if (fieldInfo != null)
          return (T)fieldInfo.GetValue(fieldInfo_string);
      }

      return defalut_value;
    }

    public static void SetFieldValue(GameObject gameObject, string fieldInfo_string, object value,
      params Type[] exclude_component_types)
    {
      foreach (var component in gameObject.GetComponentsExclude(exclude_component_types))
      {
        var fieldInfo = component.GetType().GetFieldInfo(fieldInfo_string);
        if (fieldInfo != null)
          fieldInfo.SetValue(fieldInfo_string, value);
      }
    }

    #endregion

    #region ProperyValue

    public static T GetProperyValue<T>(GameObject gameObject, string propertyInfo_string, T defalut_value,
      object[] index = null, params Type[] exclude_component_types)
    {
      foreach (var component in gameObject.GetComponentsExclude(exclude_component_types))
      {
        var propertyInfo = component.GetType().GetPropertyInfo(propertyInfo_string);
        if (propertyInfo != null)
          return (T)propertyInfo.GetValue(propertyInfo_string, index);
      }

      return defalut_value;
    }

    public static void SetProperyValue(GameObject gameObject, string fieldInfo_string, object value,
      object[] index = null, params Type[] exclude_component_types)
    {
      foreach (var component in gameObject.GetComponentsExclude(exclude_component_types))
      {
        var propertyInfo = component.GetType().GetPropertyInfo(fieldInfo_string);
        if (propertyInfo != null)
          propertyInfo.SetValue(fieldInfo_string, value, index);
      }
    }

    #endregion

    #region Invoke

    /// <summary>
    ///   调用callMethod的方法
    /// </summary>
    /// <param name="c"></param>
    /// <param name="call_method"></param>
    /// <param name="excludeComponents"></param>
    /// <param name="parameters"></param>
    public static void Invoke(GameObject gameObject, string call_method, string exclude_component_types = null,
      params object[] parameters)
    {
      foreach (var component in gameObject.GetComponentsExclude(exclude_component_types))
        ReflectionUtil.Invoke(component, call_method, true, parameters);
    }

    #endregion

    #endregion
  }
}