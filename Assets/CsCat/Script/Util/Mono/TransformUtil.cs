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
			string prefabName = transform.name;
			int removeIndex = -1;
			if ((removeIndex = prefabName.IndexOf(StringConst.String_LeftRoundBrackets)) >= 0)
				prefabName = prefabName.Remove(removeIndex);

			if ((removeIndex = prefabName.IndexOf(StringConst.String_Space)) >= 0)
				prefabName = prefabName.Remove(removeIndex);

			return prefabName;
		}

		#region Find children

		/// <summary>
		/// 找到一个符合条件的TransformA后，不会再在该TransformA中继续查找，而是找TransformA的下一个兄弟节点
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static UnityEngine.Component[] FindComponentsInChildren(Transform transform, Type type, string name,
			bool isRecursive = true, bool isStartWith = true)
		{
			List<UnityEngine.Component> list = new List<UnityEngine.Component>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (isStartWith)
				{
					if (child.name.StartsWith(name))
						list.Add(child.GetComponent(type));
				}
				else if (child.name.Equals(name))
					list.Add(child.GetComponent(type));

				if (!isRecursive) continue;
				UnityEngine.Component[] components = FindComponentsInChildren(child, type, name, isRecursive, isStartWith);
				if (components == null || components.Length <= 0) continue;
				list.AddRange(components);
			}

			return list.Count == 0 ? null : list.ToArray();
		}

		public static T[] FindComponentsInChildren<T>(Transform transform, string name, bool isRecursive = true,
			bool isStartWith = true) where T : UnityEngine.Component
		{
			UnityEngine.Component[] components = FindComponentsInChildren(transform, typeof(T), name, isRecursive, isStartWith);
			return components?.ToArray<T>();
		}

		public static UnityEngine.Component FindComponentInChildren(Transform transform, Type type, string name,
			bool isRecursive = true, bool isStartWith = true)
		{
			if (name.IndexOf(CharConst.Char_Slash) != -1)
				return transform.Find(name).GetComponent(type);

			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (isStartWith)
				{
					if (child.name.StartsWith(name))
						return child.GetComponent(type);
				}
				else if (child.name.Equals(name))
					return child.GetComponent(type);

				if (isRecursive)
				{
					UnityEngine.Component t = FindComponentInChildren(child, type, name, isRecursive, isStartWith);
					if (t != null)
						return t;
				}
			}

			return null;
		}

		public static T FindComponentInChildren<T>(Transform transform, string name, bool isRecursive = true,
			bool isStartWith = true) where T : UnityEngine.Component
		{
			return FindComponentInChildren(transform, typeof(T), name, isRecursive, isStartWith) as T;
		}

		public static UnityEngine.Component FindComponentWithTagInChildren(Transform transform, Type type, string tagName,
			bool isRecursive = true, bool isStartWith = true)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (isStartWith)
				{
					if (child.tag.StartsWith(tagName))
						return child.GetComponent(type);
				}
				else if (child.tag.Equals(tagName))
					return child.GetComponent(type);

				if (!isRecursive) continue;
				UnityEngine.Component component = FindComponentWithTagInChildren(child, type, tagName, true, isStartWith);
				if (component != null)
					return component;
			}

			return null;
		}

		public static T FindComponentWithTagInChildren<T>(Transform transform, string tagName,
			bool isRecursive = true,
			bool isStartWith = true) where T : UnityEngine.Component
		{
			return FindComponentWithTagInChildren(transform, typeof(T), tagName, isRecursive, isStartWith) as T;
		}

		public static UnityEngine.Component[] FindComponentsWithTagInChildren(Transform transform, Type type, string tagName,
			bool isRecursive = true, bool isStartWith = true)
		{
			List<UnityEngine.Component> list = new List<UnityEngine.Component>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (isStartWith)
				{
					if (child.tag.StartsWith(tagName))
						list.Add(child.GetComponent(type));
				}
				else if (child.tag.Equals(tagName))
					list.Add(child.GetComponent(type));

				if (!isRecursive) continue;
				UnityEngine.Component[] components =
					FindComponentsWithTagInChildren(child, type, tagName, isRecursive, isStartWith);
				if (components == null || components.Length <= 0) continue;
				list.AddRange(components);
			}

			return list.Count == 0 ? null : list.ToArray();
		}

		public static T[] FindComponentsWithTagInChildren<T>(Transform transform, string tagName,
			bool isRecursive = true,
			bool isStartWith = true) where T : UnityEngine.Component
		{
			UnityEngine.Component[] components =
				FindComponentsWithTagInChildren(transform, typeof(T), tagName, isRecursive, isStartWith);
			return components?.ToArray<T>();
		}

		#endregion

		#region Find parent

		/// <summary>
		/// 
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static UnityEngine.Component[] FindComponentsInParent(Transform transform, Type type, string name,
			bool isStartWith = true)
		{
			List<UnityEngine.Component> list = new List<UnityEngine.Component>();
			Transform current = transform;
			while (current != null)
			{
				UnityEngine.Component component = current.GetComponent(type);
				if (component != null)
				{
					if (isStartWith)
					{
						if (current.name.StartsWith(name))
							list.Add(component);
					}
					else if (current.name.Equals(name))
						list.Add(component);
				}

				current = current.parent;
			}

			return list.Count == 0 ? null : list.ToArray();
		}

		public static T[] FindComponentsInParent<T>(Transform transform, string name, bool isStartWith = true)
			where T : UnityEngine.Component
		{
			UnityEngine.Component[] components = FindComponentsInParent(transform, typeof(T), name, isStartWith);
			return components?.ToArray<T>();
		}

		public static UnityEngine.Component FindComponentInParent(Transform transform, Type type, string name,
			bool isStartWith = true)
		{
			Transform current = transform;
			while (current != null)
			{
				UnityEngine.Component component = current.GetComponent(type);
				if (component != null)
				{
					if (isStartWith)
					{
						if (current.name.StartsWith(name))
							return component;
					}
					else if (current.name.Equals(name))
						return component;
				}

				current = current.parent;
			}

			return null;
		}

		public static T FindComponentInParent<T>(Transform transform, string name, bool isStartWith = true)
			where T : UnityEngine.Component
		{
			return FindComponentInParent(transform, typeof(T), name, isStartWith) as T;
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
			Transform[] transforms = new Transform[count];
			for (int i = 0; i < count; i++)
			{
				Transform transform = root.GetChild(i);
				transforms[i] = transform;
			}

			return transforms;
		}

		/// <summary>
		/// 销毁子节点
		/// </summary>
		/// <param name="root"></param>
		public static void DestroyChildren(Transform root)
		{
			for (int i = root.childCount - 1; i >= 0; i--)
				root.GetChild(i).Destroy();
		}

		/// <summary>
		/// Find子Object，包括Disable的Object也会遍历获取
		/// </summary>
		public static Transform FindChild(Transform parent, string childName)
		{
			Transform[] transforms = parent.GetComponentsInChildren<Transform>(true);
			foreach (Transform transform in transforms)
				if (transform.name.Equals(childName))
					return transform;

			return null;
		}


		/// <summary>
		/// 从根物体到当前物体的全路径, 以/分隔
		/// </summary>
		public static string GetFullPath(Transform transform, Transform rootTransform = null,
			string separator = StringConst.String_Slash)
		{
			using (var scope = new StringBuilderScope())
			{
				scope.stringBuilder.Append(transform.name);
				Transform iterator = transform.parent;
				while (iterator != rootTransform || iterator != null)
				{
					scope.stringBuilder.Insert(0, separator);
					scope.stringBuilder.Insert(0, iterator.name);
					iterator = iterator.parent;
				}

				return scope.stringBuilder.ToString();
			}
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

		public static (bool, string) GetRelativePath(Transform transform, Transform parentTransform = null)
		{
			using (var scope = new StringBuilderScope())
			{
				scope.stringBuilder.Append(transform.name);
				if (transform == parentTransform)
					return (true, scope.stringBuilder.ToString());
				Transform parentNode = transform.parent;
				while (!(parentNode == null || parentNode == parentTransform))
				{
					scope.stringBuilder.Insert(0, parentNode.name + StringConst.String_Slash);
					parentNode = parentNode.parent;
				}

				bool isFound = parentTransform == parentNode;
				if (isFound && parentNode != null)
					scope.stringBuilder.Insert(0, parentNode.name + StringConst.String_Slash);
				return (isFound, scope.stringBuilder.ToString());
			}
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
			transform.rotation =
				new Quaternion(value, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		}

		public static void SetRotationY(Transform transform, float value)
		{
			transform.rotation =
				new Quaternion(transform.rotation.x, value, transform.rotation.z, transform.rotation.w);
		}

		public static void SetRotationZ(Transform transform, float value)
		{
			transform.rotation =
				new Quaternion(transform.rotation.x, transform.rotation.y, value, transform.rotation.w);
		}

		public static void SetRotationW(Transform transform, float value)
		{
			transform.rotation =
				new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, value);
		}

		public static void SetLocalRotationX(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(value, transform.localRotation.y, transform.localRotation.z,
				transform.localRotation.w);
		}

		public static void SetLocalRotationY(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, value, transform.localRotation.z,
				transform.localRotation.w);
		}

		public static void SetLocalRotationZ(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, value,
				transform.localRotation.w);
		}

		public static void SetLocalRotationW(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y,
				transform.localRotation.z, value);
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

		public static Vector3 GetLossyScaleOfParent(Transform transform)
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
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale =
				new Vector3(
					Math.Abs(lossyScale.x) <= float.Epsilon
						? 0
						: value / lossyScale.x, transform.localScale.y, transform.localScale.z);
		}

		public static void SetLossyScaleY(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale = new Vector3(transform.localScale.x,
				Math.Abs(lossyScale.y) <= float.Epsilon
					? 0
					: value / lossyScale.y, transform.localScale.z);
		}

		public static void SetLossyScaleZ(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
				Math.Abs(lossyScale.z) <= float.Epsilon
					? 0
					: value / lossyScale.z);
		}

		public static void SetLossyScale(Transform transform, Vector3 value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			var valueX = Math.Abs(lossyScale.x) <= float.Epsilon ? 0 : value.x / lossyScale.x;
			var valueY = Math.Abs(lossyScale.y) <= float.Epsilon ? 0 : value.y / lossyScale.y;
			var valueZ = Math.Abs(lossyScale.z) <= float.Epsilon ? 0 : value.z / lossyScale.z;
			transform.localScale = new Vector3(valueX, valueY, valueZ);
		}

		#endregion

		#endregion

		#region AddPositon,LocalPosition,Euler,LocalEuler,Rotation,LocalRotation,LocalScale,LossyScale

		#region position

		public static void AddPositionX(Transform transform, float value)
		{
			transform.position = new Vector3(transform.position.x + value, transform.position.y, transform.position.z);
		}

		public static void AddPositionY(Transform transform, float value)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + value, transform.position.z);
		}

		public static void AddPositionZ(Transform transform, float value)
		{
			transform.localPosition =
				new Vector3(transform.position.x, transform.position.y, transform.position.z + value);
		}

		public static void AddLocalPositionX(Transform transform, float value)
		{
			transform.localPosition = new Vector3(transform.localPosition.x + value, transform.localPosition.y,
				transform.localPosition.z);
		}

		public static void AddLocalPositionY(Transform transform, float value)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + value,
				transform.localPosition.z);
		}

		public static void AddLocalPositionZ(Transform transform, float value)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
				transform.localPosition.z + value);
		}

		#endregion

		#region eulerAngles

		public static void AddEulerAnglesX(Transform transform, float value)
		{
			transform.eulerAngles =
				new Vector3(transform.eulerAngles.x + value, transform.eulerAngles.y, transform.eulerAngles.z);
		}

		public static void AddEulerAnglesY(Transform transform, float value)
		{
			transform.eulerAngles =
				new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + value, transform.eulerAngles.z);
		}

		public static void AddEulerAnglesZ(Transform transform, float value)
		{
			transform.eulerAngles =
				new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + value);
		}

		public static void AddLocalEulerAnglesX(Transform transform, float value)
		{
			transform.localEulerAngles = new Vector3(value, transform.localEulerAngles.y,
				transform.localEulerAngles.x + transform.localEulerAngles.z);
		}

		public static void AddLocalEulerAnglesY(Transform transform, float value)
		{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + value,
				transform.localEulerAngles.z);
		}

		public static void AddLocalEulerAnglesZ(Transform transform, float value)
		{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y,
				transform.localEulerAngles.z + value);
		}

		#endregion

		#region Rotation

		public static void AddRotationX(Transform transform, float value)
		{
			transform.rotation = new Quaternion(transform.rotation.x + value, transform.rotation.y,
				transform.rotation.z,
				transform.rotation.w);
		}

		public static void AddRotationY(Transform transform, float value)
		{
			transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + value,
				transform.rotation.z,
				transform.rotation.w);
		}

		public static void AddRotationZ(Transform transform, float value)
		{
			transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y,
				transform.rotation.z + value,
				transform.rotation.w);
		}

		public static void AddRotationW(Transform transform, float value)
		{
			transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z,
				transform.rotation.w + value);
		}

		public static void AddLocalRotationX(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x + value, transform.localRotation.y,
				transform.localRotation.z, transform.localRotation.w);
		}

		public static void AddLocalRotationY(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y + value,
				transform.localRotation.z, transform.localRotation.w);
		}

		public static void AddLocalRotationZ(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y,
				transform.localRotation.z + value, transform.localRotation.w);
		}

		public static void AddLocalRotationW(Transform transform, float value)
		{
			transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y,
				transform.localRotation.z, transform.localRotation.w + value);
		}

		#endregion

		#region scale

		public static void AddLocalScaleX(Transform transform, float value)
		{
			transform.localScale =
				new Vector3(transform.localScale.x + value, transform.localScale.y, transform.localScale.z);
		}

		public static void AddLocalScaleY(Transform transform, float value)
		{
			transform.localScale =
				new Vector3(transform.localScale.x, transform.localScale.y + value, transform.localScale.z);
		}

		public static void AddLocalScaleZ(Transform transform, float value)
		{
			transform.localScale =
				new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + value);
		}

		public static void AddLossyScaleX(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale =
				new Vector3(
					Math.Abs(lossyScale.x) <= float.Epsilon
						? (0 + value)
						: 1 + (value / lossyScale.x), transform.localScale.y, transform.localScale.z);
		}

		public static void AddLossyScaleY(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale = new Vector3(transform.localScale.x,
				Math.Abs(lossyScale.y) <= float.Epsilon
					? (0 + value)
					: 1 + (value / lossyScale.y), transform.localScale.z);
		}

		public static void AddLossyScaleZ(Transform transform, float value)
		{
			var lossyScale = GetLossyScaleOfParent(transform);
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
				Math.Abs(lossyScale.z) <= float.Epsilon
					? (0 + value)
					: 1 + (value / lossyScale.z));
		}

		#endregion

		#endregion

		public static void SetIsGray(Transform transform, bool isGray, bool isRecursive = true)
		{
			_SetIsGray(transform, isGray);
			if (!isRecursive) return;
			for (int i = 0; i < transform.childCount; i++)
				SetIsGray(transform.GetChild(i), isGray, isRecursive);
		}

		static void _SetIsGray(Transform transform, bool isGray)
		{
			transform.GetComponent<Image>()?.SetIsGray(isGray);
			transform.GetComponent<Text>()?.SetIsGray(isGray);
		}

		public static void SetAlpha(Transform self, float alpha, bool isRecursive = true)
		{
			if (!isRecursive)
				_SetAlpha(self, alpha);
			else
				self.DoActionRecursive(transform => SetAlpha(transform, alpha));
		}

		static void _SetAlpha(Transform transform, float alpha)
		{
			transform.GetComponent<Image>()?.SetAlpha(alpha);
			transform.GetComponent<Text>()?.SetAlpha(alpha);
		}

		public static void SetColor(Transform self, Color color, bool isNotUseColorAlpha = false,
			bool isRecursive = true)
		{
			if (!isRecursive)
				_SetColor(self, color, isNotUseColorAlpha);
			else
				self.DoActionRecursive(transform => _SetColor(transform, color, isNotUseColorAlpha));
		}

		static void _SetColor(Transform transform, Color color, bool isNotUseColorAlpha = false)
		{
			transform.GetComponent<Image>()?.SetColor(color, isNotUseColorAlpha);
			transform.GetComponent<Text>()?.SetColor(color, isNotUseColorAlpha);
		}
	}
}