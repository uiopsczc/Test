

using System;
using UnityEngine;
using UnityEngine.UI;
//using XLua;

namespace CsCat
{
  public static class GameObjectExtension
  {
    public static bool IsSceneGameObject(this GameObject self)
    {
      return self.scene.IsValid();
    }

    /// <summary>
    /// 有T返回T，没T添加T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject self) where T : Component
    {
      return GameObjectUtil.GetOrAddComponent<T>(self);
    }

    public static Component GetOrAddComponent(this GameObject self, Type type)
    {
      return GameObjectUtil.GetOrAddComponent(self, type);
    }

    /// <summary>
    /// 使某个类型的组件enable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="enable"></param>
    public static void EnableComponents<T>(this GameObject self, bool enable) where T : MonoBehaviour
    {
      GameObjectUtil.EnableComponents<T>(self, enable);
    }

    public static void EnableComponents(this GameObject self, Type type, bool enable)
    {
      GameObjectUtil.EnableComponents(self, type, enable);
    }

    /// <summary>
    /// 销毁子孩子节点
    /// </summary>
    /// <param name="self"></param>
    public static void DestroyChildren(this GameObject self)
    {
      GameObjectUtil.DestroyChildren(self);
    }



    /// <summary>
    /// 只有包含全部的Components才会返回True
    /// </summary>
    /// <param name="self"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    public static bool IsHasComponents(this GameObject self, params Type[] types)
    {
      return GameObjectUtil.IsHasComponents(self, types);
    }


    public static bool IsHasComponent(this GameObject self, Type type)
    {
      return self.GetComponent(type) != null;
    }

    public static bool IsHasComponent<T>(this GameObject self) where T:Component
    {
      return IsHasComponent(self, typeof(T));
    }

    /// <summary>
    /// 获取该gameObject下的组件，不包括剔除的组件类型
    /// </summary>
    /// <param name="self"></param>
    /// <param name="exclude_component_types">剔除的组件类型</param>
    /// <returns></returns>
    public static Component[] GetComponentsExclude(this GameObject self, params Type[] exclude_component_types)
    {
      return GameObjectUtil.GetComponentsExclude(self, exclude_component_types);
    }

    /// <summary>
    /// 获取该gameObject下的组件，不包括剔除的组件类型
    /// </summary>
    /// <param name="self"></param>
    /// <param name="exclude_component_types">剔除的组件类型</param>
    /// <param name="exclude_split"></param>
    /// <returns></returns>
    public static Component[] GetComponentsExclude(this GameObject self, string exclude_component_types,
      string exclude_split = "|")
    {
      return GameObjectUtil.GetComponentsExclude(self, exclude_component_types, exclude_split);
    }

    public static GameObject GetSocketGameObject(this GameObject self, string socket_name = null)
    {
      return self.transform.GetSocketTransform(socket_name).gameObject;
    }


    /// <summary>
    /// 设置暂停
    /// 暂停animator和particleSystem(包括其孩子节点)
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="cause"></param>
    public static void SetPuase(this GameObject self, object cause)
    {
      PauseUtil.SetPuase(self, cause);
    }

    public static RectTransform RectTransform(this GameObject self)
    {
      return self.GetComponent<RectTransform>();
    }

    public static void Despawn(this GameObject self)
    {
      if (self == null)
        return;
      if (self.IsCacheContainsKey(PoolCatConst.Pool_Name))
      {
        PoolCat pool = self.GetCache<PoolCat>(PoolCatConst.Pool_Name);
        pool.Despawn(self);
      }
      //      else if (self.IsCacheContainsKey(PoolCatConst.Lua_Pool_Name))//Lua端调用
      //      {
      //        LuaTable pool = self.GetCache<LuaTable>(PoolCatConst.Lua_Pool_Name);
      //        pool.Get("Despawn", out LuaFunction despawnFunction);
      //        despawnFunction.Action(pool, self);
      //        despawnFunction.Dispose();
      //      }
    }

    public static void SetCache(this GameObject self, string key, object obj)
    {
      CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
      cache.Set(obj, key);
    }

    public static void SetCache(this GameObject self, string key, string sub_key, object obj)
    {
      CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
      cache.Set(obj, key, sub_key);
    }

    public static T GetCache<T>(this GameObject self, string key = null)
    {
      CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
      return cache.Get<T>(key);
    }


    public static T GetCache<T>(this GameObject self, string key, string sub_key)
    {
      CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
      return cache.Get<T>(key, sub_key);
    }

    public static T GetOrAddCache<T>(this GameObject self, string key, Func<T> defaultFunc)
    {
      CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
      return cache.GetOrAdd(key, defaultFunc);
    }

    public static object GetOrAddCache(this GameObject self, string key, Func<object> defaultFunc)
    {
      return GetOrAddCache<object>(self, key, defaultFunc);
    }

    public static T GetOrAddCache<T>(this GameObject self, string key, string sub_key, Func<T> defaultFunc)
    {
      CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
      return cache.GetOrAdd(key, sub_key, defaultFunc);
    }

    public static bool IsCacheContainsKey(this GameObject self, string key)
    {
      CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
      return cache.dict.ContainsKey(key);
    }

    public static GameObject NewChildGameObject(this GameObject self, string path = null)
    {
      if (self == null)
        return null;
      return self.transform.NewChildGameObject(path);
    }

    public static Component NewChildWithComponent(this GameObject self, Type component_type, string path = null)
    {
      if (self == null)
        return null;
      return self.transform.NewChildWithComponent(component_type, path);
    }

    public static T NewChildWithComponent<T>(this GameObject self, string path = null) where T : Component
    {
      if (self == null)
        return null;
      return self.transform.NewChildWithComponent<T>(path);
    }

    public static RectTransform NewChildWithRectTransform(this GameObject self, string path = null)
    {
      return self.transform.NewChildWithComponent<RectTransform>(path);
    }

    public static Image NewChildWithImage(this GameObject self, string path = null)
    {
      return self.transform.NewChildWithImage(path);
    }

    public static Text NewChildWithText(this GameObject self, string path = null, string content = null,
      int font_size = 20, Color? color = null, TextAnchor? alignment = null, Font font = null)
    {
      return self.transform.NewChildWithText(path, content, font_size, color, alignment, null);
    }

    public static void SetIsGray(this GameObject self, bool is_gray, bool is_recursive = true)
    {
      self.transform.SetIsGray(is_gray, is_recursive);
    }

    public static void DoActionRecursive(this GameObject self, Action<GameObject> do_action)
    {
      self.transform.DoActionRecursive(transform => do_action(transform.gameObject));
    }

    public static void SetAlpha(this GameObject self, float alpha, bool is_recursive = true)
    {
      self.transform.SetAlpha(alpha, is_recursive);
    }

    public static void SetColor(this GameObject self, Color color, bool is_not_use_color_alpha = false, bool is_recursive = true)
    {
      self.transform.SetColor(color, is_not_use_color_alpha, is_recursive);
    }


    public static (bool, string) GetRelativePath(this GameObject self, GameObject parent_gameObject = null)
    {
      return TransformUtil.GetRelativePath(self.transform, parent_gameObject == null ? null : parent_gameObject.transform);
    }
    

    #region 反射

    #region FiledValue

    public static T GetFieldValue<T>(this GameObject self, string field_name, T defalutValue,
      params Type[] exclude_component_types)
    {
      return GameObjectUtil.GetFieldValue<T>(self, field_name, defalutValue, exclude_component_types);
    }

    public static void SetFieldValue(this GameObject self, string field_name, object value,
      params Type[] exclude_component_types)
    {
      GameObjectUtil.SetFieldValue(self, field_name, value, exclude_component_types);
    }

    #endregion

    #region ProperyValue

    public static T GetProperyValue<T>(this GameObject self, string property_name, T defalutValue, object[] index = null,
      params Type[] exclude_component_types)
    {
      return GameObjectUtil.GetProperyValue(self, property_name, defalutValue, index, exclude_component_types);

    }

    public static void SetProperyValue(this GameObject self, string property_name, object value, object[] index = null,
      params Type[] exclude_component_types)
    {
      GameObjectUtil.SetProperyValue(self, property_name, value, index, exclude_component_types);
    }

    #endregion

    #region Invoke

    /// <summary>
    /// 调用callMethod的方法
    /// </summary>
    /// <param name="c"></param>
    /// <param name="method_name"></param>
    /// <param name="excludeComponents"></param>
    /// <param name="args"></param>
    public static void Invoke(this GameObject self, string method_name, string exclude_component_types = null,
      params object[] args)
    {
      GameObjectUtil.Invoke(self, method_name, exclude_component_types, args);
    }

    #endregion

    #endregion
  }
}