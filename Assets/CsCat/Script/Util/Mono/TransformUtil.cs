

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public class TransformUtil
  {
    /// <summary>
    /// GetName    赋值物体的时候，名字可能出现去掉（），空格等，去掉这些冗余得到的名字
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static string GetName(Transform transform)
    {
      string prefab_name = transform.name;
      int del_index = -1;
      if ((del_index = prefab_name.IndexOf('(')) >= 0)
      {
        prefab_name = prefab_name.Remove(del_index);
      }

      if ((del_index = prefab_name.IndexOf(' ')) >= 0)
      {
        prefab_name = prefab_name.Remove(del_index);
      }

      return prefab_name;
    }

    #region Find children

    /// <summary>
    /// 找到一个符合条件的TransformA后，不会再在该TransformA中继续查找，而是找TransformA的下一个兄弟节点
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Component[] FindComponentsInChildren(Transform transform, Type type, string name,
      bool is_recursive = true, bool is_start_with = true)
    {
      List<Component> list = new List<Component>();
      for (int i = 0; i < transform.childCount; i++)
      {
        Transform child = transform.GetChild(i);
        if (is_start_with)
        {
          if (child.name.StartsWith(name))
          {
            list.Add(child.GetComponent(type));
          }
        }
        else
        {
          if (child.name.Equals(name))
          {
            list.Add(child.GetComponent(type));
          }
        }

        if (is_recursive)
        {
          Component[] ts = FindComponentsInChildren(child, type, name, is_recursive, is_start_with);
          if (ts != null && ts.Length > 0)
          {
            list.AddRange(ts);
          }
        }
      }

      return list.Count == 0 ? null : list.ToArray();
    }

    public static T[] FindComponentsInChildren<T>(Transform transform, string name, bool is_recursive = true,
      bool is_start_with = true) where T : Component
    {
      Component[] components = FindComponentsInChildren(transform, typeof(T), name, is_recursive, is_start_with);
      return components == null ? null : components.ToArray<T>();
    }

    public static Component FindComponentInChildren(Transform transform, Type type, string name,
      bool is_recursive = true, bool is_start_with = true)
    {
      if (name.IndexOf('/') != -1)
      {
        return transform.Find(name).GetComponent(type);
      }

      for (int i = 0; i < transform.childCount; i++)
      {
        Transform child = transform.GetChild(i);
        if (is_start_with)
        {
          if (child.name.StartsWith(name))
          {
            return child.GetComponent(type);
          }
        }
        else
        {
          if (child.name.Equals(name))
          {
            return child.GetComponent(type);
          }
        }

        if (is_recursive)
        {
          Component t = FindComponentInChildren(child, type, name, is_recursive, is_start_with);
          if (t != null)
            return t;
        }
      }

      return null;
    }

    public static T FindComponentInChildren<T>(Transform transform, string name, bool is_recursive = true,
      bool is_start_with = true) where T : Component
    {
      return FindComponentInChildren(transform, typeof(T), name, is_recursive, is_start_with) as T;
    }

    public static Component FindComponentWithTagInChildren(Transform transform, Type type, string tag_name,
      bool is_recursive = true, bool is_start_with = true)
    {

      for (int i = 0; i < transform.childCount; i++)
      {
        Transform child = transform.GetChild(i);
        if (is_start_with)
        {
          if (child.tag.StartsWith(tag_name))
          {
            return child.GetComponent(type);
          }
        }
        else
        {
          if (child.tag.Equals(tag_name))
          {
            return child.GetComponent(type);
          }
        }

        if (is_recursive)
        {
          Component t = FindComponentWithTagInChildren(child, type, tag_name, is_recursive, is_start_with);
          if (t != null)
            return t;
        }
      }

      return null;
    }

    public static T FindComponentWithTagInChildren<T>(Transform transform, string tag_name, bool is_recursive = true,
      bool is_start_with = true) where T : Component
    {
      return FindComponentWithTagInChildren(transform, typeof(T), tag_name, is_recursive, is_start_with) as T;
    }

    public static Component[] FindComponentsWithTagInChildren(Transform transform, Type type, string tag_name,
      bool is_recursive = true, bool is_start_with = true)
    {
      List<Component> list = new List<Component>();
      for (int i = 0; i < transform.childCount; i++)
      {
        Transform child = transform.GetChild(i);
        if (is_start_with)
        {
          if (child.tag.StartsWith(tag_name))
          {
            list.Add(child.GetComponent(type));
          }
        }
        else
        {
          if (child.tag.Equals(tag_name))
          {
            list.Add(child.GetComponent(type));
          }
        }

        if (is_recursive)
        {
          Component[] ts = FindComponentsWithTagInChildren(child, type, tag_name, is_recursive, is_start_with);
          if (ts != null && ts.Length > 0)
          {
            list.AddRange(ts);
          }
        }
      }

      return list.Count == 0 ? null : list.ToArray();
    }

    public static T[] FindComponentsWithTagInChildren<T>(Transform transform, string tag_name, bool is_recursive = true,
      bool is_start_with = true) where T : Component
    {
      Component[] components =
        FindComponentsWithTagInChildren(transform, typeof(T), tag_name, is_recursive, is_start_with);
      return components == null ? null : components.ToArray<T>();
    }

    #endregion

    #region Find parent

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Component[] FindComponentsInParent(Transform transform, Type type, string name,
      bool is_start_with = true)
    {
      List<Component> list = new List<Component>();
      Transform current = transform;
      while (current != null)
      {
        Component t = current.GetComponent(type);
        if (t != null)
        {
          if (is_start_with)
          {
            if (current.name.StartsWith(name))
              list.Add(t);
          }
          else
          {
            if (current.name.Equals(name))
              list.Add(t);
          }
        }

        current = current.parent;
      }

      return list.Count == 0 ? null : list.ToArray();
    }

    public static T[] FindComponentsInParent<T>(Transform transform, string name, bool is_start_with = true)
      where T : Component
    {
      Component[] components = FindComponentsInParent(transform, typeof(T), name, is_start_with);
      return components == null ? null : components.ToArray<T>();
    }

    public static Component FindComponentInParent(Transform transform, Type type, string name,
      bool is_start_with = true)
    {
      Transform current = transform;
      while (current != null)
      {
        Component t = current.GetComponent(type);
        if (t != null)
        {
          if (is_start_with)
          {
            if (current.name.StartsWith(name))
              return t;
          }
          else
          {
            if (current.name.Equals(name))
              return t;
          }
        }

        current = current.parent;
      }

      return null;
    }

    public static T FindComponentInParent<T>(Transform transform, string name, bool is_start_with = true)
      where T : Component
    {
      return FindComponentInParent(transform, typeof(T), name, is_start_with) as T;
    }

    #endregion

    /// <summary>
    /// 获取直接子孩子节点
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static Transform[] GetChildren(Transform root)
    {
      int count = root.childCount;
      Transform[] tfs = new Transform[count];
      for (int i = 0; i < count; i++)
      {
        Transform tf = root.GetChild(i);
        tfs[i] = tf;
      }

      return tfs;
    }

    /// <summary>
    /// 销毁子节点
    /// </summary>
    /// <param name="root"></param>
    public static void DestroyChildren(Transform root)
    {
      for (int i = root.childCount - 1; i >= 0; i--)
      {
        root.GetChild(i).Destroy();
      }
    }

    /// <summary>
    /// Find子Object，包括Disable的Object也会遍历获取
    /// </summary>
    public static Transform FindChild(Transform parent, string child_name)
    {
      Transform[] transform = parent.GetComponentsInChildren<Transform>(true);
      for (int i = 0; i < transform.Length; i++)
      {
        if (transform[i].name.Equals(child_name))
        {
          return transform[i];
        }
      }

      return null;
    }


    /// <summary>
    /// 从根物体到当前物体的全路径, 以/分隔
    /// </summary>
    public static string GetFullPath(Transform transform, Transform root_transform = null, string separator = "/")
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(transform.name);

      Transform iterator = transform.parent;
      while (iterator != root_transform || iterator != null)
      {
        sb.Insert(0, separator);
        sb.Insert(0, iterator.name);
        iterator = iterator.parent;
      }

      return sb.ToString();
    }

    /// <summary>
    /// 递归设置layer
    /// </summary>
    public static void SetLayerRecursive(Transform transform, int layer)
    {
      if (transform == null)
        return;
      transform.gameObject.layer = layer;
      foreach (Transform child in transform)
      {
        SetLayerRecursive(child, layer);
      }
    }

    /// <summary>
    /// 重置
    /// </summary>
    /// <param name="transform"></param>
    public static void Reset(Transform transform,
      TransformMode transformMode =
        TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
    {
      if (transformMode.Contains(TransformMode.localPosition))
        transform.localPosition = Vector3.zero;
      if (transformMode.Contains(TransformMode.localRotation))
        transform.localRotation = Quaternion.identity;
      if (transformMode.Contains(TransformMode.localScale))
        transform.localScale = Vector3.one;
      if (transformMode.Contains(TransformMode.position))
        transform.position = Vector3.zero;
      if (transformMode.Contains(TransformMode.rotation))
        transform.rotation = Quaternion.identity;
    }

    public static void ResetToParent(Transform transform, GameObject parent,
      TransformMode transformMode =
        TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
    {
      transform.ResetToParent(parent.transform, transformMode);
    }

    public static void ResetToParent(Transform transform, Transform parent,
      TransformMode transformMode =
        TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
    {
      transform.SetParent(parent);
      Reset(transform, transformMode);
    }

    #region SetPositon,LocalPosition,Euler,LocalEuler,Rotation, LocalRotation,LocalScale,LossyScale

    #region position

    public static void SetPositionX(Transform transform, float value)
    {
      transform.position = new Vector3(value, transform.position.y, transform.position.z);
    }

    public static void SetPositionY(Transform transform, float value)
    {
      transform.position = new Vector3(transform.position.x, value, transform.position.z);
    }

    public static void SetPositionZ(Transform transform, float value)
    {
      transform.localPosition = new Vector3(transform.position.x, transform.position.y, value);
    }

    public static void SetLocalPositionX(Transform transform, float value)
    {
      transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
    }

    public static void SetLocalPositionY(Transform transform, float value)
    {
      transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
    }

    public static void SetLocalPositionZ(Transform transform, float value)
    {
      transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, value);
    }

    #endregion

    #region eulerAngles

    public static void SetEulerAnglesX(Transform transform, float value)
    {
      transform.eulerAngles = new Vector3(value, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public static void SetEulerAnglesY(Transform transform, float value)
    {
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, value, transform.eulerAngles.z);
    }

    public static void SetEulerAnglesZ(Transform transform, float value)
    {
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, value);
    }

    public static void SetLocalEulerAnglesX(Transform transform, float value)
    {
      transform.localEulerAngles = new Vector3(value, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public static void SetLocalEulerAnglesY(Transform transform, float value)
    {
      transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, value, transform.localEulerAngles.z);
    }

    public static void SetLocalEulerAnglesZ(Transform transform, float value)
    {
      transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, value);
    }

    #endregion

    #region Rotation
    public static void SetRotationX(Transform transform, float value)
    {
      transform.rotation = new Quaternion(value, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }
    public static void SetRotationY(Transform transform, float value)
    {
      transform.rotation = new Quaternion(transform.rotation.x, value, transform.rotation.z, transform.rotation.w);
    }
    public static void SetRotationZ(Transform transform, float value)
    {
      transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, value, transform.rotation.w);
    }
    public static void SetRotationW(Transform transform, float value)
    {
      transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, value);
    }

    public static void SetLocalRotationX(Transform transform, float value)
    {
      transform.localRotation = new Quaternion(value, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
    }
    public static void SetLocalRotationY(Transform transform, float value)
    {
      transform.localRotation = new Quaternion(transform.localRotation.x, value, transform.localRotation.z, transform.localRotation.w);
    }
    public static void SetLocalRotationZ(Transform transform, float value)
    {
      transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, value, transform.localRotation.w);
    }
    public static void SetLocalRotationW(Transform transform, float value)
    {
      transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, value);
    }
    #endregion

    #region scale

    public static void SetLocalScaleX(Transform transform, float value)
    {
      transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
    }

    public static void SetLocalScaleY(Transform transform, float value)
    {
      transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);
    }

    public static void SetLocalScaleZ(Transform transform, float value)
    {
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);
    }

    public static Vector3 GetLossyScaleOfPrarent(Transform transform)
    {
      Vector3 result = Vector3.one;
      Transform current = transform.parent;
      while (current != null)
      {
        result = result.Multiply(current.localScale);
        current = current.parent;
      }

      return result;
    }

    public static void SetLossyScaleX(Transform transform, float value)
    {
      var lossyScale = GetLossyScaleOfPrarent(transform);
      transform.localScale =
        new Vector3(
          Math.Abs(lossyScale.x) <= float.Epsilon
            ? 0
            : value / lossyScale.x, transform.localScale.y, transform.localScale.z);
    }

    public static void SetLossyScaleY(Transform transform, float value)
    {
      var lossyScale = GetLossyScaleOfPrarent(transform);
      transform.localScale = new Vector3(transform.localScale.x,
        Math.Abs(lossyScale.y) <= float.Epsilon
          ? 0
          : value / lossyScale.y, transform.localScale.z);
    }

    public static void SetLossyScaleZ(Transform transform, float value)
    {
      var lossyScale = GetLossyScaleOfPrarent(transform);
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
        Math.Abs(lossyScale.z) <= float.Epsilon
          ? 0
          : value / lossyScale.z);
    }
    #endregion

    #endregion

    #region AddPositon,LocalPosition,Euler,LocalEuler,Rotation,LocalRotation,LocalScale,LossyScale

    #region position

    public static void AddPositionX(Transform transform, float value)
    {
      transform.position = new Vector3(transform.position.x+value, transform.position.y, transform.position.z);
    }

    public static void AddPositionY(Transform transform, float value)
    {
      transform.position = new Vector3(transform.position.x, transform.position.y + value, transform.position.z);
    }

    public static void AddPositionZ(Transform transform, float value)
    {
      transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z+value );
    }

    public static void AddLocalPositionX(Transform transform, float value)
    {
      transform.localPosition = new Vector3(transform.localPosition.x+value, transform.localPosition.y, transform.localPosition.z);
    }

    public static void AddLocalPositionY(Transform transform, float value)
    {
      transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + value, transform.localPosition.z);
    }

    public static void AddLocalPositionZ(Transform transform, float value)
    {
      transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + value);
    }

    #endregion

    #region eulerAngles

    public static void AddEulerAnglesX(Transform transform, float value)
    {
      transform.eulerAngles = new Vector3(transform.eulerAngles.x+value, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public static void AddEulerAnglesY(Transform transform, float value)
    {
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y+value, transform.eulerAngles.z);
    }

    public static void AddEulerAnglesZ(Transform transform, float value)
    {
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z+value);
    }

    public static void AddLocalEulerAnglesX(Transform transform, float value)
    {
      transform.localEulerAngles = new Vector3(value, transform.localEulerAngles.y, transform.localEulerAngles.x+ transform.localEulerAngles.z);
    }

    public static void AddLocalEulerAnglesY(Transform transform, float value)
    {
      transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + value, transform.localEulerAngles.z);
    }

    public static void AddLocalEulerAnglesZ(Transform transform, float value)
    {
      transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + value);
    }

    #endregion

    #region Rotation
    public static void AddRotationX(Transform transform, float value)
    {
      transform.rotation =  new Quaternion(transform.rotation.x+value, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }
    public static void AddRotationY(Transform transform, float value)
    {
      transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + value, transform.rotation.z, transform.rotation.w);
    }
    public static void AddRotationZ(Transform transform, float value)
    {
      transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + value, transform.rotation.w);
    }
    public static void AddRotationW(Transform transform, float value)
    {
      transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w+value);
    }

    public static void AddLocalRotationX(Transform transform, float value)
    {
      transform.localRotation = new Quaternion(transform.localRotation.x + value, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
    }
    public static void AddLocalRotationY(Transform transform, float value)
    {
      transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y + value, transform.localRotation.z, transform.localRotation.w);
    }
    public static void AddLocalRotationZ(Transform transform, float value)
    {
      transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + value, transform.localRotation.w);
    }
    public static void AddLocalRotationW(Transform transform, float value)
    {
      transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w+value);
    }
    #endregion

    #region scale

    public static void AddLocalScaleX(Transform transform, float value)
    {
      transform.localScale = new Vector3(transform.localScale.x+value, transform.localScale.y, transform.localScale.z);
    }

    public static void AddLocalScaleY(Transform transform, float value)
    {
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + value, transform.localScale.z);
    }

    public static void AddLocalScaleZ(Transform transform, float value)
    {
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + value);
    }

    public static void AddLossyScaleX(Transform transform, float value)
    {
      var lossyScale = GetLossyScaleOfPrarent(transform);
      transform.localScale =
        new Vector3(
          Math.Abs(lossyScale.x) <= float.Epsilon
            ? (0+value)
            : 1+(value / lossyScale.x), transform.localScale.y, transform.localScale.z);
    }

    public static void AddLossyScaleY(Transform transform, float value)
    {
      var lossyScale = GetLossyScaleOfPrarent(transform);
      transform.localScale = new Vector3(transform.localScale.x,
        Math.Abs(lossyScale.y) <= float.Epsilon
          ? (0+value)
          : 1+(value / lossyScale.y), transform.localScale.z);
    }

    public static void AddLossyScaleZ(Transform transform, float value)
    {
      var lossyScale = GetLossyScaleOfPrarent(transform);
      transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
        Math.Abs(lossyScale.z) <= float.Epsilon
          ? (0+value)
          : 1+(value / lossyScale.z));
    }
    #endregion

    #endregion

    public static void SetIsGray(Transform transform,bool is_gray, bool is_recursive = true)
    {
      __SetIsGray(transform, is_gray);
      if (is_recursive)
      {
        for (int i = 0; i < transform.childCount; i++)
          SetIsGray(transform.GetChild(i), is_gray, is_recursive);
      }
    }

    static void __SetIsGray(Transform transform, bool is_gray)
    {
      transform.GetComponent<Image>()?.SetIsGray(is_gray);
      transform.GetComponent<Text>()?.SetIsGray(is_gray);
    }

    public static void SetAlpha( Transform self, float alpha, bool is_recursive = true)
    {
      if (!is_recursive)
        __SetAlpha(self,alpha);
      else
        self.DoActionRecursive(transform => SetAlpha(transform, alpha));

    }

    static void __SetAlpha(Transform transform, float alpha)
    {
      transform.GetComponent<Image>()?.SetAlpha(alpha);
      transform.GetComponent<Text>()?.SetAlpha(alpha);
    }

    public static void SetColor(Transform self, Color color, bool is_not_use_color_alpha = false, bool is_recursive = true)
    {
      if (!is_recursive)
        __SetColor(self, color, is_not_use_color_alpha);
      else
        self.DoActionRecursive(transform => __SetColor(transform, color, is_not_use_color_alpha));
    }

    static void __SetColor(Transform transform, Color color, bool is_not_use_color_alpha = false)
    {
      transform.GetComponent<Image>()?.SetColor(color, is_not_use_color_alpha);
      transform.GetComponent<Text>()?.SetColor(color, is_not_use_color_alpha);
    }
  }
}