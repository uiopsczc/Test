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
		public static UnityEngine.Component GetOrAddComponent(GameObject gameObject, Type type)
		{
			if (gameObject == null) return null;
			var component = gameObject.GetComponent(type);
			if (component == null)
				component = gameObject.AddComponent(type);
			return component;
		}

		public static T GetOrAddComponent<T>(GameObject gameObject) where T : UnityEngine.Component
		{
			return GetOrAddComponent(gameObject, typeof(T)) as T;
		}

		/// <summary>
		///   使某个类型的组件enable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="gameObject"></param>
		/// <param name="isEnable"></param>
		public static void EnableComponents(GameObject gameObject, Type type, bool isEnable)
		{
			var components = gameObject.GetComponents(type);
			if (components == null) return;
			var num = components.Length;
			for (var i = 0; i < num; i++) ((MonoBehaviour)components[i]).enabled = isEnable;
		}

		public static void EnableComponents<T>(GameObject gameObject, bool isEnable) where T : MonoBehaviour
		{
			EnableComponents(gameObject, typeof(T), isEnable);
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
		/// <param name="excludeComponentTypes">剔除的组件类型</param>
		/// <returns></returns>
		public static UnityEngine.Component[] GetComponentsExclude(GameObject gameObject, params Type[] excludeComponentTypes)
		{
			var result = new List<UnityEngine.Component>();
			foreach (var component in gameObject.GetComponents<UnityEngine.Component>())
			{
				if (excludeComponentTypes.Length > 0) //如果剔除的类型个数不为0
				{
					var isContinueThisRound = false; //是否结束这个round
					foreach (var excludeComponentType in excludeComponentTypes)
						if (component.GetType().IsSubTypeOf(excludeComponentType)) //如果是组件类型是其中的剔除的类型或其子类
						{
							isContinueThisRound = true;
							break;
						}

					if (isContinueThisRound)
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
		/// <param name="excludeComponentTypes">剔除的组件类型</param>
		/// <param name="excludeSeparator"></param>
		/// <returns></returns>
		public static UnityEngine.Component[] GetComponentsExclude(GameObject gameObject, string excludeComponentTypes,
			string excludeSeparator = StringConst.String_Vertical)
		{
			var excludeTypeList = new List<Type>();
			if (string.IsNullOrEmpty(excludeComponentTypes))
				return gameObject.GetComponentsExclude(excludeTypeList.ToArray());
			var excludeComponentTypeList = excludeComponentTypes.ToList<string>(excludeSeparator);
			foreach (var excludeComponentType in excludeComponentTypeList)
				//只查当前的assembly和UnityEngine两个Assembly
				if (TypeUtil.GetType(excludeComponentType) != null)
					excludeTypeList.Add(TypeUtil.GetType(excludeComponentType));
				else if (TypeUtil.GetType(excludeComponentType, "UnityEngine") != null)
					excludeTypeList.Add(TypeUtil.GetType(excludeComponentType, "UnityEngine"));

			return gameObject.GetComponentsExclude(excludeTypeList.ToArray());
		}

		public static GameObject GetOrNewGameObject(string path, GameObject parentGameObject)
		{
			if (parentGameObject == null)
			{
				var result = GameObject.Find(path);
				if (result != null)
					return result;
			}
			else
			{
				var result = parentGameObject.transform.Find(path);
				if (result != null)
					return result.gameObject;
			}

			string name = path.GetPreString(StringConst.String_Slash);
			var gameObject = new GameObject(name);
			if (parentGameObject != null)
				gameObject.transform.ResetToParent(parentGameObject.transform);
			return !name.Equals(path)
				? GetOrNewGameObject(path.GetPostString(StringConst.String_Slash), gameObject)
				: gameObject;
		}


		#region GameObject 反射

		#region FiledValue

		public static T GetFieldValue<T>(GameObject gameObject, string fieldInfoString, T defaultValue,
			params Type[] excludeComponentTypes)
		{
			foreach (var component in gameObject.GetComponentsExclude(excludeComponentTypes))
			{
				var fieldInfo = component.GetType().GetFieldInfo(fieldInfoString);
				if (fieldInfo != null)
					return (T)fieldInfo.GetValue(fieldInfoString);
			}

			return defaultValue;
		}

		public static void SetFieldValue(GameObject gameObject, string fieldInfoString, object value,
			params Type[] excludeComponentTypes)
		{
			foreach (var component in gameObject.GetComponentsExclude(excludeComponentTypes))
			{
				var fieldInfo = component.GetType().GetFieldInfo(fieldInfoString);
				if (fieldInfo != null)
					fieldInfo.SetValue(fieldInfoString, value);
			}
		}

		#endregion

		#region ProperyValue

		public static T GetPropertyValue<T>(GameObject gameObject, string propertyInfoString, T defaultValue,
			object[] index = null, params Type[] excludeComponentTypes)
		{
			foreach (var component in gameObject.GetComponentsExclude(excludeComponentTypes))
			{
				var propertyInfo = component.GetType().GetPropertyInfo(propertyInfoString);
				if (propertyInfo != null)
					return (T)propertyInfo.GetValue(propertyInfoString, index);
			}

			return defaultValue;
		}

		public static void SetPropertyValue(GameObject gameObject, string fieldInfoString, object value,
			object[] index = null, params Type[] excludeComponentTypes)
		{
			foreach (var component in gameObject.GetComponentsExclude(excludeComponentTypes))
			{
				var propertyInfo = component.GetType().GetPropertyInfo(fieldInfoString);
				if (propertyInfo != null)
					propertyInfo.SetValue(fieldInfoString, value, index);
			}
		}

		#endregion

		#region Invoke

		/// <summary>
		///   调用callMethod的方法
		/// </summary>
		/// <param name="gameObject"></param>
		/// <param name="invokeMethodName"></param>
		/// <param name="excludeComponentTypes"></param>
		/// <param name="parameters"></param>
		public static void Invoke(GameObject gameObject, string invokeMethodName, string excludeComponentTypes = null,
			params object[] parameters)
		{
			foreach (var component in gameObject.GetComponentsExclude(excludeComponentTypes))
				ReflectionUtil.Invoke(component, invokeMethodName, true, parameters);
		}

		#endregion

		#endregion
	}
}