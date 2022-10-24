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
		public static T GetOrAddComponent<T>(this GameObject self) where T : UnityEngine.Component
		{
			return GameObjectUtil.GetOrAddComponent<T>(self);
		}

		public static UnityEngine.Component GetOrAddComponent(this GameObject self, Type type)
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

		public static bool IsHasComponent<T>(this GameObject self) where T : UnityEngine.Component
		{
			return IsHasComponent(self, typeof(T));
		}

		/// <summary>
		/// 获取该gameObject下的组件，不包括剔除的组件类型
		/// </summary>
		/// <param name="self"></param>
		/// <param name="excludeComponentTypes">剔除的组件类型</param>
		/// <returns></returns>
		public static UnityEngine.Component[] GetComponentsExclude(this GameObject self, params Type[] excludeComponentTypes)
		{
			return GameObjectUtil.GetComponentsExclude(self, excludeComponentTypes);
		}

		/// <summary>
		/// 获取该gameObject下的组件，不包括剔除的组件类型
		/// </summary>
		/// <param name="self"></param>
		/// <param name="excludeComponentTypes">剔除的组件类型</param>
		/// <param name="excludeSplit"></param>
		/// <returns></returns>
		public static UnityEngine.Component[] GetComponentsExclude(this GameObject self, string excludeComponentTypes,
			string excludeSplit = StringConst.String_Vertical)
		{
			return GameObjectUtil.GetComponentsExclude(self, excludeComponentTypes, excludeSplit);
		}

		public static GameObject GetSocketGameObject(this GameObject self, string socketName = null)
		{
			return self.transform.GetSocketTransform(socketName).gameObject;
		}


		/// <summary>
		/// 设置暂停
		/// 暂停animator和particleSystem(包括其孩子节点)
		/// </summary>
		/// <param name="gameObject"></param>
		/// <param name="cause"></param>
		public static void SetPuase(this GameObject self, object cause)
		{
			PauseUtil.SetPause(self, cause);
		}

		public static RectTransform RectTransform(this GameObject self)
		{
			return self.GetComponent<RectTransform>();
		}

		public static void Despawn(this GameObject self)
		{
			if (self == null)
				return;
			if (self.IsCacheContainsKey(PoolCatConst.Pool_Object))
			{
				IPoolObject poolObject = self.GetCache<IPoolObject>(PoolCatConst.Pool_Object);
				poolObject.Despawn();
			}
		}

		public static void SetCache(this GameObject self, string key, object obj)
		{
			CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
			cache.Set(obj, key);
		}

		public static void SetCache(this GameObject self, string key, string subKey, object obj)
		{
			CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
			cache.Set(obj, key, subKey);
		}

		public static T GetCache<T>(this GameObject self, string key = null)
		{
			CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
			return cache.Get<T>(key);
		}


		public static T GetCache<T>(this GameObject self, string key, string subKey)
		{
			CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
			return cache.Get<T>(key, subKey);
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

		public static T GetOrAddCache<T>(this GameObject self, string key, string subKey, Func<T> defaultFunc)
		{
			CacheMonoBehaviour cache = self.GetOrAddComponent<CacheMonoBehaviour>();
			return cache.GetOrAdd(key, subKey, defaultFunc);
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

		public static UnityEngine.Component NewChildWithComponent(this GameObject self, Type componentType, string path = null)
		{
			return self == null ? null : self.transform.NewChildWithComponent(componentType, path);
		}

		public static T NewChildWithComponent<T>(this GameObject self, string path = null) where T : UnityEngine.Component
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
			int fontSize = 20, Color? color = null, TextAnchor? alignment = null, Font font = null)
		{
			return self.transform.NewChildWithText(path, content, fontSize, color, alignment, null);
		}

		public static void SetIsGray(this GameObject self, bool isGray, bool isRecursive = true)
		{
			self.transform.SetIsGray(isGray, isRecursive);
		}

		public static void DoActionRecursive(this GameObject self, Action<GameObject> doAction)
		{
			self.transform.DoActionRecursive(transform => doAction(transform.gameObject));
		}

		public static void SetAlpha(this GameObject self, float alpha, bool isRecursive = true)
		{
			self.transform.SetAlpha(alpha, isRecursive);
		}

		public static void SetColor(this GameObject self, Color color, bool isNotUseColorAlpha = false,
			bool isRecursive = true)
		{
			self.transform.SetColor(color, isNotUseColorAlpha, isRecursive);
		}


		public static (bool, string) GetRelativePath(this GameObject self, GameObject parentGameObject = null)
		{
			return TransformUtil.GetRelativePath(self.transform,
				parentGameObject == null ? null : parentGameObject.transform);
		}

		public static float GetParticleSystemDuration(this GameObject self, bool isRecursive = true)
		{
			if (!isRecursive)
			{
				var particleSystem = self.GetComponent<ParticleSystem>();
				if (particleSystem == null)
					return 0;
				return particleSystem.GetDuration(false);
			}

			float maxDuration = 0;
			foreach (var particleSystem in self.GetComponentsInChildren<ParticleSystem>())
			{
				var duration = particleSystem.GetDuration(false);
				if (duration == -1)
					return duration;
				if (maxDuration < duration)
					maxDuration = duration;
			}

			return maxDuration;
		}

		#region 反射

		#region FiledValue

		public static T GetFieldValue<T>(this GameObject self, string fieldName, T defaultValue,
			params Type[] excludeComponentTypes)
		{
			return GameObjectUtil.GetFieldValue<T>(self, fieldName, defaultValue, excludeComponentTypes);
		}

		public static void SetFieldValue(this GameObject self, string fieldName, object value,
			params Type[] excludeComponentTypes)
		{
			GameObjectUtil.SetFieldValue(self, fieldName, value, excludeComponentTypes);
		}

		#endregion

		#region ProperyValue

		public static T GetPropertyValue<T>(this GameObject self, string propertyName, T defaultValue,
			object[] index = null,
			params Type[] excludeComponentTypes)
		{
			return GameObjectUtil.GetPropertyValue(self, propertyName, defaultValue, index, excludeComponentTypes);
		}

		public static void SetPropertyValue(this GameObject self, string propertyName, object value,
			object[] index = null,
			params Type[] excludeComponentTypes)
		{
			GameObjectUtil.SetPropertyValue(self, propertyName, value, index, excludeComponentTypes);
		}

		#endregion

		#region Invoke

		/// <summary>
		/// 调用callMethod的方法
		/// </summary>
		/// <param name="c"></param>
		/// <param name="methodName"></param>
		/// <param name="excludeComponents"></param>
		/// <param name="args"></param>
		public static void Invoke(this GameObject self, string methodName, string excludeComponentTypes = null,
			params object[] args)
		{
			GameObjectUtil.Invoke(self, methodName, excludeComponentTypes, args);
		}

		#endregion

		#endregion
	}
}