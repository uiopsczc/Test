using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
  public static partial class TransformExtension
  {
    public static Matrix4x4 LocalMatrix(this Transform self)
    {
      return Matrix4x4.TRS(self.localPosition, self.localRotation, self.localScale);
    }

    public static Matrix4x4 WorldMatrix(this Transform self)
    {
      return Matrix4x4.TRS(self.position, self.rotation, self.lossyScale);
    }

    public static void SetParentFactor(this Transform self, Transform mutiply_transform)
    {
      Transform orgin_self_parent_transform = self.parent;
      self.SetParent(mutiply_transform, false);
      self.SetParent(orgin_self_parent_transform);
    }


    /// <summary>
    /// GetName    赋值物体的时候，名字可能出现去掉（），空格等，去掉这些冗余得到的名字
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static string GetName(this Transform self)
    {
      return TransformUtil.GetName(self);
    }

    #region Find child

    /// <summary>
    /// 找到一个符合条件的TransformA后，不会再在该TransformA中继续查找，而是找TransformA的下一个兄弟节点
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T[] FindComponentsInChildren<T>(this Transform self, string name, bool is_recursive = true,
      bool is_start_with = true) where T : Component
    {
      return TransformUtil.FindComponentsInChildren<T>(self, name, is_recursive, is_start_with);
    }

    public static Component[] FindComponentsInChildren(this Transform self, Type type, string name,
      bool is_recursive = true, bool is_start_with = true)
    {
      return TransformUtil.FindComponentsInChildren(self, type, name, is_recursive, is_start_with);
    }


    public static T FindComponentInChildren<T>(this Transform self, string name, bool is_recursive = true,
      bool is_start_with = true) where T : Component
    {
      return TransformUtil.FindComponentInChildren<T>(self, name, is_recursive, is_start_with);
    }

    public static Component FindComponentInChildren(this Transform self, Type type, string name,
      bool is_recursive = true, bool is_start_with = true)
    {
      return TransformUtil.FindComponentInChildren(self, type, name, is_recursive, is_start_with);
    }

    public static T FindComponentWithTagInChildren<T>(this Transform self, string tagName, bool is_recursive = true,
      bool is_start_with = true) where T : Component
    {
      return TransformUtil.FindComponentWithTagInChildren<T>(self, tagName, is_recursive, is_start_with);
    }

    public static Component FindComponentWithTagInChildren(this Transform self, Type type, string tag_name,
      bool is_recursive = true, bool is_start_with = true)
    {
      return TransformUtil.FindComponentWithTagInChildren(self, type, tag_name, is_recursive, is_start_with);
    }

    public static T[] FindComponentsWithTagInChildren<T>(this Transform self, string tag_name, bool is_recursive = true,
      bool is_start_with = true) where T : Component
    {
      return TransformUtil.FindComponentsWithTagInChildren<T>(self, tag_name, is_recursive, is_start_with);
    }

    public static Component[] FindComponentsWithTagInChildren(this Transform self, Type type, string tag_name,
      bool is_recursive = true, bool is_start_with = true)
    {
      return TransformUtil.FindComponentsWithTagInChildren(self, type, tag_name, is_recursive, is_start_with);
    }

    #endregion

    #region Find parent

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T[] FindComponentsInParent<T>(this Transform self, string name, bool is_start_with = true)
      where T : Component
    {
      return TransformUtil.FindComponentsInParent<T>(self, name, is_start_with);
    }

    public static Component[] FindComponentsInParent(this Transform self, Type type, string name,
      bool is_start_with = true)
    {
      return TransformUtil.FindComponentsInParent(self, type, name, is_start_with);
    }


    public static T FindComponentInParent<T>(this Transform self, string name, bool is_start_with = true)
      where T : Component
    {
      return TransformUtil.FindComponentInParent<T>(self, name, is_start_with);
    }

    public static Component FindComponentInParent(this Transform self, Type type, string name,
      bool is_start_with = true)
    {
      return TransformUtil.FindComponentInParent(self, type, name, is_start_with);
    }

    #endregion

    #region Find Peer

    public static Transform GetPeer(this Transform self, string peer_name)
    {
      if (self.parent == null)
      {
        GameObject gameObject = GameObject.Find(peer_name);
        if (gameObject != null && gameObject.transform.parent == null)
          return gameObject.transform;
      }
      else
      {
        return self.parent.Find(peer_name);
      }

      return null;
    }

    public static T GetPeer<T>(this Transform self, string peer_name)
    {
      return self.GetPeer(peer_name).GetComponent<T>();
    }

    #endregion


    /// <summary>
    /// 设置目标物体下所有子物体的显隐状态
    /// </summary>
    /// <param name="self"></param>
    /// <param name="is_active"></param>
    public static void SetChildrenActive(this Transform self, bool is_active)
    {
      for (int i = 0; i < self.childCount; ++i)
        self.GetChild(i).gameObject.SetActive(is_active);
    }

    /// <summary>
    /// 获取直接子孩子节点
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static Transform[] Children(this Transform self)
    {
      return TransformUtil.GetChildren(self);
    }

    public static Transform GetLastChild(this Transform self)
    {
      return self.GetChild(self.childCount - 1);
    }

    public static Transform GetFirstChild(this Transform self)
    {
      return self.GetChild(0);
    }

    /// <summary>
    /// 销毁子节点
    /// </summary>
    /// <param name="root"></param>
    public static void DestroyChildren(this Transform self)
    {
      TransformUtil.DestroyChildren(self);
    }

    /// <summary>
    /// Find子Object，包括Disable的Object也会遍历获取
    /// </summary>
    public static Transform FindChildRecursive(this Transform self, string child_name)
    {
      return TransformUtil.FindChild(self, child_name);
    }


    /// <summary>
    /// 从根物体到当前物体的全路径, 以/分隔
    /// </summary>
    public static string GetFullPath(this Transform self, Transform root_transform = null, string separator = "/")
    {
      return TransformUtil.GetFullPath(self, root_transform, separator);
    }

    /// <summary>
    /// 递归设置layer
    /// </summary>
    public static void SetLayerRecursive(this Transform self, int layer)
    {
      TransformUtil.SetLayerRecursive(self, layer);
    }

    /// <summary>
    /// 重置
    /// </summary>
    /// <param name="transform"></param>
    public static void Reset(this Transform self,
      TransformMode transformMode =
        TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
    {
      TransformUtil.Reset(self, transformMode);
    }

    public static void ResetToParent(this Transform self, GameObject parent_gameObject,
      TransformMode transformMode =
        TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
    {
      TransformUtil.ResetToParent(self, parent_gameObject, transformMode);
    }

    public static void ResetToParent(this Transform self, Transform parent_transform,
      TransformMode transformMode =
        TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
    {
      TransformUtil.ResetToParent(self, parent_transform, transformMode);
    }

    #region SetPositon,LocalPosition,Euler,LocalEuler,Rotation,LocalRotation,LocalScale,LossyScale

    #region position

    public static void SetPositionX(this Transform self, float value)
    {
      TransformUtil.SetPositionX(self, value);
    }

    public static void SetPositionY(this Transform self, float value)
    {
      TransformUtil.SetPositionY(self, value);
    }

    public static void SetPositionZ(this Transform self, float value)
    {
      TransformUtil.SetPositionZ(self, value);
    }

    public static void SetLocalPositionX(this Transform self, float value)
    {
      TransformUtil.SetLocalPositionX(self, value);
    }

    public static void SetLocalPositionY(this Transform self, float value)
    {
      TransformUtil.SetLocalPositionY(self, value);
    }

    public static void SetLocalPositionZ(this Transform self, float value)
    {
      TransformUtil.SetLocalPositionZ(self, value);
    }

    #endregion

    #region eulerAngles

    public static void SetEulerAnglesX(this Transform self, float value)
    {
      TransformUtil.SetEulerAnglesX(self, value);
    }

    public static void SetEulerAnglesY(this Transform self, float value)
    {
      TransformUtil.SetEulerAnglesY(self, value);
    }

    public static void SetEulerAnglesZ(this Transform self, float value)
    {
      TransformUtil.SetEulerAnglesZ(self, value);
    }

    public static void SetLocalEulerAnglesX(this Transform self, float value)
    {
      TransformUtil.SetLocalEulerAnglesX(self, value);
    }

    public static void SetLocalEulerAnglesY(this Transform self, float value)
    {
      TransformUtil.SetLocalEulerAnglesY(self, value);
    }

    public static void SetLocalEulerAnglesZ(this Transform self, float value)
    {
      TransformUtil.SetLocalEulerAnglesZ(self, value);
    }

    #endregion

    #region rotation

    public static void SetRotationX(this Transform self, float value)
    {
      TransformUtil.SetRotationX(self, value);
    }

    public static void SetRotationY(this Transform self, float value)
    {
      TransformUtil.SetRotationY(self, value);
    }

    public static void SetRotationZ(this Transform self, float value)
    {
      TransformUtil.SetRotationZ(self, value);
    }

    public static void SetLocalRotationX(this Transform self, float value)
    {
      TransformUtil.SetLocalRotationX(self, value);
    }

    public static void SetLocalRotationY(this Transform self, float value)
    {
      TransformUtil.SetLocalRotationY(self, value);
    }

    public static void SetLocalRotationZ(this Transform self, float value)
    {
      TransformUtil.SetLocalRotationZ(self, value);
    }

    #endregion

    #region scale

    public static void SetLocalScaleX(this Transform self, float value)
    {
      TransformUtil.SetLocalScaleX(self, value);
    }

    public static void SetLocalScaleY(this Transform self, float value)
    {
      TransformUtil.SetLocalScaleY(self, value);
    }

    public static void SetLocalScaleZ(this Transform self, float value)
    {
      TransformUtil.SetLocalScaleZ(self, value);
    }

    public static void SetLossyScaleX(this Transform self, float value)
    {
      TransformUtil.SetLossyScaleX(self, value);
    }

    public static void SetLossyScaleY(this Transform self, float value)
    {
      TransformUtil.SetLossyScaleY(self, value);
    }

    public static void SetLossyScaleZ(this Transform self, float value)
    {
      TransformUtil.SetLossyScaleZ(self, value);
    }

    #endregion

    #endregion

    #region AddPositon,LocalPosition,Euler,LocalEuler,Rotation,LocalRotation,LocalScale,LossyScale

    #region position

    public static void AddPositionX(this Transform self, float value)
    {
      TransformUtil.AddPositionX(self, value);
    }

    public static void AddPositionY(this Transform self, float value)
    {
      TransformUtil.AddPositionY(self, value);
    }

    public static void AddPositionZ(this Transform self, float value)
    {
      TransformUtil.AddPositionZ(self, value);
    }

    public static void AddLocalPositionX(this Transform self, float value)
    {
      TransformUtil.AddLocalPositionX(self, value);
    }

    public static void AddLocalPositionY(this Transform self, float value)
    {
      TransformUtil.AddLocalPositionY(self, value);
    }

    public static void AddLocalPositionZ(this Transform self, float value)
    {
      TransformUtil.AddLocalPositionZ(self, value);
    }

    #endregion

    #region eulerAngles

    public static void AddEulerAnglesX(this Transform self, float value)
    {
      TransformUtil.AddEulerAnglesX(self, value);
    }

    public static void AddEulerAnglesY(this Transform self, float value)
    {
      TransformUtil.AddEulerAnglesY(self, value);
    }

    public static void AddEulerAnglesZ(this Transform self, float value)
    {
      TransformUtil.AddEulerAnglesZ(self, value);
    }

    public static void AddLocalEulerAnglesX(this Transform self, float value)
    {
      TransformUtil.AddLocalEulerAnglesX(self, value);
    }

    public static void AddLocalEulerAnglesY(this Transform self, float value)
    {
      TransformUtil.AddLocalEulerAnglesY(self, value);
    }

    public static void AddLocalEulerAnglesZ(this Transform self, float value)
    {
      TransformUtil.AddLocalEulerAnglesZ(self, value);
    }

    #endregion

    #region rotation

    public static void AddRotationX(this Transform self, float value)
    {
      TransformUtil.AddRotationX(self, value);
    }

    public static void AddRotationY(this Transform self, float value)
    {
      TransformUtil.AddRotationY(self, value);
    }

    public static void AddRotationZ(this Transform self, float value)
    {
      TransformUtil.AddRotationZ(self, value);
    }

    public static void AddLocalRotationX(this Transform self, float value)
    {
      TransformUtil.AddLocalRotationX(self, value);
    }

    public static void AddLocalRotationY(this Transform self, float value)
    {
      TransformUtil.AddLocalRotationY(self, value);
    }

    public static void AddLocalRotationZ(this Transform self, float value)
    {
      TransformUtil.AddLocalRotationZ(self, value);
    }

    #endregion

    #region scale

    public static void AddLocalScaleX(this Transform self, float value)
    {
      TransformUtil.AddLocalScaleX(self, value);
    }

    public static void AddLocalScaleY(this Transform self, float value)
    {
      TransformUtil.AddLocalScaleY(self, value);
    }

    public static void AddLocalScaleZ(this Transform self, float value)
    {
      TransformUtil.AddLocalScaleZ(self, value);
    }

    public static void AddLossyScaleX(this Transform self, float value)
    {
      TransformUtil.AddLossyScaleX(self, value);
    }

    public static void AddLossyScaleY(this Transform self, float value)
    {
      TransformUtil.AddLossyScaleY(self, value);
    }

    public static void AddLossyScaleZ(this Transform self, float value)
    {
      TransformUtil.AddLossyScaleZ(self, value);
    }

    #endregion

    #endregion

    public static Vector3 GetLossyScaleOfPrarent(this Transform transform)
    {
      return TransformUtil.GetLossyScaleOfPrarent(transform);
    }

    public static void SetLossyScale(this Transform transform, Vector3 value)
    {
      Vector3 parentLossyScale = transform.GetLossyScaleOfPrarent();
      transform.localScale = new Vector3(
        Math.Abs(parentLossyScale.x) <= float.Epsilon ? 0 : value.x / parentLossyScale.x,
        Math.Abs(parentLossyScale.y) <= float.Epsilon ? 0 : value.y / parentLossyScale.y,
        Math.Abs(parentLossyScale.z) < float.Epsilon ? 0 : value.z / parentLossyScale.z);
    }

    public static void CopyFrom(this Transform self, Transform from_transform,
      TransformMode transformMode =
        TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
    {
      from_transform.CopyTo(self, transformMode);
    }

    public static void CopyTo(this Transform self, Transform traget_transform,
      TransformMode transformMode =
        TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
    {
      if (transformMode.Contains(TransformMode.position))
        traget_transform.position = self.position;
      if (transformMode.Contains(TransformMode.localPosition))
        traget_transform.localPosition = self.localPosition;

      if (transformMode.Contains(TransformMode.rotation))
        traget_transform.rotation = self.rotation;
      if (transformMode.Contains(TransformMode.localRotation))
        traget_transform.localRotation = self.localRotation;

      if (transformMode.Contains(TransformMode.scale))
        traget_transform.SetLossyScale(self.lossyScale);
      if (transformMode.Contains(TransformMode.localScale))
        traget_transform.localScale = self.localScale;

      //有rect的，rect也一起copy
      if (self.GetComponent<RectTransform>() != null && traget_transform.GetComponent<RectTransform>() != null)
        traget_transform.GetComponent<RectTransform>().CopyFrom(self.GetComponent<RectTransform>());
    }

    public static Transform GetSocketTransform(this Transform self, string socket_name = null)
    {
      socket_name = socket_name ?? "";
      Transform soket_transform = self.gameObject.GetOrAddCache("socket", socket_name, () =>
      {
        if (socket_name.IsNullOrWhiteSpace())
          return self;
        Transform result = self.FindChildRecursive(socket_name);
        result = result ?? self;
        return result;
      });
      return soket_transform;
    }

    public static TransformPosition ToTransformPosition(this Transform self)
    {
      return new TransformPosition(self);
    }

    public static void SetIsGray(this Transform self, bool is_gray, bool is_recursive = true)
    {
      TransformUtil.SetIsGray(self, is_gray, is_recursive);
    }

    public static void DoActionRecursive(this Transform self, Action<Transform> do_action)
    {
      do_action(self);
      foreach (var child in self.Children())
        do_action(child);
    }

    public static void SetAlpha(this Transform self, float alpha, bool is_recursive = true)
    {
      TransformUtil.SetAlpha(self, alpha, is_recursive);
    }

    public static void SetColor(this Transform self, Color color, bool is_not_use_color_alpha = false, bool is_recursive = true)
    {
      TransformUtil.SetColor(self, color, is_not_use_color_alpha, is_recursive);
    }


    public static (bool, string) GetRelativePath(this Transform self, Transform parent_transform = null)
    {
      return TransformUtil.GetRelativePath(self, parent_transform);
    }

	  public static float GetParticleSystemDuration(this Transform self, bool is_recursive = true)
	  {
		  return self.gameObject.GetParticleSystemDuration(is_recursive);
	  }

		#region DOTween

		#region act

		//    public static Tween DOLocalMoveXOfAct(this Transform self, float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOLocalMoveX(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.Next(); });
		//    }
		//    public static Tween DOLocalMoveYOfAct(this Transform self, float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOLocalMoveY(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.Next(); });
		//    }
		//    public static Tween DOLocalMoveZOfAct(this Transform self,float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOLocalMoveZ(endValue,duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }
		//    public static Tween DOLocalMoveOfAct(this Transform self, Vector3 endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOLocalMove(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }
		//
		//    public static Tween DOMoveXOfAct(this Transform self, float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOMoveX(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }
		//    public static Tween DOMoveYOfAct(this Transform self, float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOMoveY(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }
		//    public static Tween DOMoveZOfAct(this Transform self, float endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOMoveZ(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }
		//    public static Tween DOMoveOfAct(this Transform self, Vector3 endValue, float duration, ActSequence parent)
		//    {
		//        return self.DOMove(endValue, duration).SetDOTweenId(parent).OnComplete(() => { parent.actCur.Exit(); });
		//    }

		#endregion

		public static Tween DOWait(this Transform self, float duration)
    {
      return self.DOBlendableLocalMoveBy(Vector3.zero, duration).SetDOTweenId();
    }

    public static Sequence DOJump(
      this Transform target,
      Vector3 end_value,
      float jump_power,
      int jump_num,
      float duration,
      AxisConstraint aix,
      bool snapping = false)
    {
      if (jump_num < 1)
        jump_num = 1;
      float start_pos_of_aix = 0.0f;
      float offset_aix = -1f;
      bool is_offset_aix_setted = false;
      Sequence sequence = DOTween.Sequence();

      Vector3 jump_vector3 = new Vector3(aix == AxisConstraint.X ? jump_power : 0,
        aix == AxisConstraint.Y ? jump_power : 0, aix == AxisConstraint.Z ? jump_power : 0);
      Tween y_tween = DOTween
        .To(() => target.position, x => target.position = x, jump_vector3, duration / (jump_num * 2))
        .SetOptions(aix, snapping).SetEase(Ease.OutQuad).SetRelative().SetLoops(jump_num * 2, LoopType.Yoyo).OnStart(
          () =>
          {
            if (aix == AxisConstraint.X)
              start_pos_of_aix = target.position.x;
            if (aix == AxisConstraint.Y)
              start_pos_of_aix = target.position.y;
            if (aix == AxisConstraint.Z)
              start_pos_of_aix = target.position.z;
          });


      if (aix == AxisConstraint.X)
      {
        sequence.Append(DOTween
          .To(() => target.position, x => target.position = x, new Vector3(0, end_value.y, 0.0f), duration)
          .SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.Linear));
        sequence.Join(DOTween
          .To(() => target.position, x => target.position = x, new Vector3(0.0f, 0.0f, end_value.z),
            duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear));
      }

      if (aix == AxisConstraint.Y)
      {
        sequence.Append(DOTween
          .To(() => target.position, x => target.position = x, new Vector3(end_value.x, 0.0f, 0.0f), duration)
          .SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear));
        sequence.Join(DOTween
          .To(() => target.position, x => target.position = x, new Vector3(0.0f, 0.0f, end_value.z),
            duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear));
      }


      if (aix == AxisConstraint.Z)
      {
        sequence.Append(DOTween
          .To(() => target.position, x => target.position = x, new Vector3(end_value.x, 0.0f, 0.0f), duration)
          .SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear));
        sequence.Join(DOTween
          .To(() => target.position, x => target.position = x, new Vector3(0.0f, end_value.y, 0),
            duration).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.Linear));
      }


      sequence.Join(y_tween).SetTarget(target).SetEase(DOTween.defaultEaseType);
      y_tween.OnUpdate(() =>
      {
        if (!is_offset_aix_setted)
        {
          is_offset_aix_setted = true;
          if (aix == AxisConstraint.X)
            offset_aix = sequence.isRelative ? end_value.x : end_value.x - start_pos_of_aix;
          if (aix == AxisConstraint.Y)
            offset_aix = sequence.isRelative ? end_value.y : end_value.y - start_pos_of_aix;
          if (aix == AxisConstraint.Z)
            offset_aix = sequence.isRelative ? end_value.z : end_value.z - start_pos_of_aix;
        }

        float y = DOVirtual.EasedValue(0.0f, offset_aix, y_tween.ElapsedPercentage(true), Ease.OutQuad);
        Vector3 position = target.position + new Vector3(aix == AxisConstraint.X ? y : 0,
                             aix == AxisConstraint.Y ? y : 0, aix == AxisConstraint.Z ? y : 0);

        target.position = position;
      });
      return sequence;
    }

    #endregion
  }
}