#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public static class SerializedObjectExtension
	{
		public static T TargetObject<T>(this SerializedObject self) where T : Object
		{
			return self.targetObject as T;
		}

		public static T[] TargetObjects<T>(this SerializedObject self) where T : Object
		{
			return self.targetObjects.ToArray<T>();
		}
	}
}
#endif