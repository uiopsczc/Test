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

        public static void SetParentFactor(this Transform self, Transform mutiplyTransform)
        {
            Transform originSelfParentTransform = self.parent;
            self.SetParent(mutiplyTransform, false);
            self.SetParent(originSelfParentTransform);
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
        public static T[] FindComponentsInChildren<T>(this Transform self, string name, bool isRecursive = true,
            bool isStartWith = true) where T : Component
        {
            return TransformUtil.FindComponentsInChildren<T>(self, name, isRecursive, isStartWith);
        }

        public static Component[] FindComponentsInChildren(this Transform self, Type type, string name,
            bool isRecursive = true, bool isStartWith = true)
        {
            return TransformUtil.FindComponentsInChildren(self, type, name, isRecursive, isStartWith);
        }


        public static T FindComponentInChildren<T>(this Transform self, string name, bool isRecursive = true,
            bool isStartWith = true) where T : Component
        {
            return TransformUtil.FindComponentInChildren<T>(self, name, isRecursive, isStartWith);
        }

        public static Component FindComponentInChildren(this Transform self, Type type, string name,
            bool isRecursive = true, bool isStartWith = true)
        {
            return TransformUtil.FindComponentInChildren(self, type, name, isRecursive, isStartWith);
        }

        public static T FindComponentWithTagInChildren<T>(this Transform self, string tagName, bool isRecursive = true,
            bool isStartWith = true) where T : Component
        {
            return TransformUtil.FindComponentWithTagInChildren<T>(self, tagName, isRecursive, isStartWith);
        }

        public static Component FindComponentWithTagInChildren(this Transform self, Type type, string tagName,
            bool isRecursive = true, bool isStartWith = true)
        {
            return TransformUtil.FindComponentWithTagInChildren(self, type, tagName, isRecursive, isStartWith);
        }

        public static T[] FindComponentsWithTagInChildren<T>(this Transform self, string tagName,
            bool isRecursive = true,
            bool isStartWith = true) where T : Component
        {
            return TransformUtil.FindComponentsWithTagInChildren<T>(self, tagName, isRecursive, isStartWith);
        }

        public static Component[] FindComponentsWithTagInChildren(this Transform self, Type type, string tagName,
            bool isRecursive = true, bool isStartWith = true)
        {
            return TransformUtil.FindComponentsWithTagInChildren(self, type, tagName, isRecursive, isStartWith);
        }

        #endregion

        #region Find parent

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T[] FindComponentsInParent<T>(this Transform self, string name, bool isStartWith = true)
            where T : Component
        {
            return TransformUtil.FindComponentsInParent<T>(self, name, isStartWith);
        }

        public static Component[] FindComponentsInParent(this Transform self, Type type, string name,
            bool isStartWith = true)
        {
            return TransformUtil.FindComponentsInParent(self, type, name, isStartWith);
        }


        public static T FindComponentInParent<T>(this Transform self, string name, bool isStartWith = true)
            where T : Component
        {
            return TransformUtil.FindComponentInParent<T>(self, name, isStartWith);
        }

        public static Component FindComponentInParent(this Transform self, Type type, string name,
            bool isStartWith = true)
        {
            return TransformUtil.FindComponentInParent(self, type, name, isStartWith);
        }

        #endregion

        #region Find Peer

        public static Transform GetPeer(this Transform self, string peerName)
        {
            if (self.parent == null)
            {
                GameObject gameObject = GameObject.Find(peerName);
                if (gameObject != null && gameObject.transform.parent == null)
                    return gameObject.transform;
            }
            else
            {
                return self.parent.Find(peerName);
            }

            return null;
        }

        public static T GetPeer<T>(this Transform self, string peerName)
        {
            return self.GetPeer(peerName).GetComponent<T>();
        }

        #endregion


        /// <summary>
        /// 设置目标物体下所有子物体的显隐状态
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isActive"></param>
        public static void SetChildrenActive(this Transform self, bool isActive)
        {
            for (int i = 0; i < self.childCount; ++i)
                self.GetChild(i).gameObject.SetActive(isActive);
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
        public static Transform FindChildRecursive(this Transform self, string childName)
        {
            return TransformUtil.FindChild(self, childName);
        }


        /// <summary>
        /// 从根物体到当前物体的全路径, 以/分隔
        /// </summary>
        public static string GetFullPath(this Transform self, Transform root_transform = null, string separator = StringConst.String_Slash)
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

        public static void ResetToParent(this Transform self, GameObject parentGameObject,
            TransformMode transformMode =
                TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
        {
            TransformUtil.ResetToParent(self, parentGameObject, transformMode);
        }

        public static void ResetToParent(this Transform self, Transform parentTransform,
            TransformMode transformMode =
                TransformMode.localPosition | TransformMode.localRotation | TransformMode.localScale)
        {
            TransformUtil.ResetToParent(self, parentTransform, transformMode);
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

        public static Vector3 GetLossyScaleOfParent(this Transform transform)
        {
            return TransformUtil.GetLossyScaleOfParent(transform);
        }

        public static void SetLossyScale(this Transform transform, Vector3 value)
        {
            Vector3 parentLossyScale = transform.GetLossyScaleOfParent();
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

        public static Transform GetSocketTransform(this Transform self, string socketName = null)
        {
            socketName = socketName ?? StringConst.String_Empty;
            Transform socketTransform = self.gameObject.GetOrAddCache(StringConst.String_socket, socketName, () =>
            {
                if (socketName.IsNullOrWhiteSpace())
                    return self;
                Transform result = self.FindChildRecursive(socketName);
                result = result ?? self;
                return result;
            });
            return socketTransform;
        }

        public static TransformPosition ToTransformPosition(this Transform self)
        {
            return new TransformPosition(self);
        }

        public static void SetIsGray(this Transform self, bool isGray, bool isRecursive = true)
        {
            TransformUtil.SetIsGray(self, isGray, isRecursive);
        }

        public static void DoActionRecursive(this Transform self, Action<Transform> doAction)
        {
            doAction(self);
            foreach (var child in self.Children())
                doAction(child);
        }

        public static void SetAlpha(this Transform self, float alpha, bool isRecursive = true)
        {
            TransformUtil.SetAlpha(self, alpha, isRecursive);
        }

        public static void SetColor(this Transform self, Color color, bool isNotUseColorAlpha = false,
            bool isRecursive = true)
        {
            TransformUtil.SetColor(self, color, isNotUseColorAlpha, isRecursive);
        }


        public static (bool, string) GetRelativePath(this Transform self, Transform parentTransform = null)
        {
            return TransformUtil.GetRelativePath(self, parentTransform);
        }

        public static float GetParticleSystemDuration(this Transform self, bool isRecursive = true)
        {
            return self.gameObject.GetParticleSystemDuration(isRecursive);
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
            Vector3 endValue,
            float jumpPower,
            int jumpNum,
            float duration,
            AxisConstraint aix,
            bool snapping = false)
        {
            if (jumpNum < 1)
                jumpNum = 1;
            float startPosOfAix = 0.0f;
            float offsetAix = -1f;
            bool isOffsetAixSetted = false;
            Sequence sequence = DOTween.Sequence();

            Vector3 jumpVector3 = new Vector3(aix == AxisConstraint.X ? jumpPower : 0,
                aix == AxisConstraint.Y ? jumpPower : 0, aix == AxisConstraint.Z ? jumpPower : 0);
            Tween yTween = DOTween
                .To(() => target.position, x => target.position = x, jumpVector3, duration / (jumpNum * 2))
                .SetOptions(aix, snapping).SetEase(Ease.OutQuad).SetRelative().SetLoops(jumpNum * 2, LoopType.Yoyo)
                .OnStart(
                    () =>
                    {
                        switch (aix)
                        {
                            case AxisConstraint.X:
                                startPosOfAix = target.position.x;
                                break;
                            case AxisConstraint.Y:
                                startPosOfAix = target.position.y;
                                break;
                            case AxisConstraint.Z:
                                startPosOfAix = target.position.z;
                                break;
                        }
                    });


            switch (aix)
            {
                case AxisConstraint.X:
                    sequence.Append(DOTween
                        .To(() => target.position, x => target.position = x, new Vector3(0, endValue.y, 0.0f), duration)
                        .SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.Linear));
                    sequence.Join(DOTween
                        .To(() => target.position, x => target.position = x, new Vector3(0.0f, 0.0f, endValue.z),
                            duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear));
                    break;
                case AxisConstraint.Y:
                    sequence.Append(DOTween
                        .To(() => target.position, x => target.position = x, new Vector3(endValue.x, 0.0f, 0.0f), duration)
                        .SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear));
                    sequence.Join(DOTween
                        .To(() => target.position, x => target.position = x, new Vector3(0.0f, 0.0f, endValue.z),
                            duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear));
                    break;
                case AxisConstraint.Z:
                    sequence.Append(DOTween
                        .To(() => target.position, x => target.position = x, new Vector3(endValue.x, 0.0f, 0.0f), duration)
                        .SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear));
                    sequence.Join(DOTween
                        .To(() => target.position, x => target.position = x, new Vector3(0.0f, endValue.y, 0),
                            duration).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.Linear));
                    break;
            }


            sequence.Join(yTween).SetTarget(target).SetEase(DOTween.defaultEaseType);
            yTween.OnUpdate(() =>
            {
                if (!isOffsetAixSetted)
                {
                    isOffsetAixSetted = true;
                    switch (aix)
                    {
                        case AxisConstraint.X:
                            offsetAix = sequence.isRelative ? endValue.x : endValue.x - startPosOfAix;
                            break;
                        case AxisConstraint.Y:
                            offsetAix = sequence.isRelative ? endValue.y : endValue.y - startPosOfAix;
                            break;
                        case AxisConstraint.Z:
                            offsetAix = sequence.isRelative ? endValue.z : endValue.z - startPosOfAix;
                            break;
                    }
                }

                float y = DOVirtual.EasedValue(0.0f, offsetAix, yTween.ElapsedPercentage(), Ease.OutQuad);
                Vector3 position = target.position + new Vector3(aix == AxisConstraint.X ? y : 0,
                                       aix == AxisConstraint.Y ? y : 0, aix == AxisConstraint.Z ? y : 0);

                target.position = position;
            });
            return sequence;
        }

        #endregion
    }
}