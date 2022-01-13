#if UNITY_EDITOR
using System.Reflection;
using UnityEditorInternal;

namespace CsCat
{
	public class SortingLayerUtil
	{
		public static string GetNameById(int sortingLayerId)
		{
			int index = GetUniqueIDs().IndexOf(sortingLayerId);
			if (index >= 0)
			{
				return GetNames()[index];
			}

			return "";
		}

		public static int GetIdByName(string sortingLayerName)
		{
			int index = GetNames().IndexOf(sortingLayerName);
			if (index >= 0)
			{
				return GetUniqueIDs()[index];
			}

			return -1;
		}

		// Get the sorting layer names
		public static string[] GetNames()
		{
			System.Type internalEditorUtilityType = typeof(InternalEditorUtility);
			PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetPropertyInfo(
				"sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
			return (string[]) sortingLayersProperty.GetValue(null, new object[0]);
		}

		// Get the sorting layer UniqueIds
		public static int[] GetUniqueIDs()
		{
			System.Type internalEditorUtilityType = typeof(InternalEditorUtility);
			PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetPropertyInfo(
				"sortingLayerUniqueIDs", BindingFlags.Static | BindingFlags.NonPublic);
			return (int[]) sortingLayersProperty.GetValue(null, new object[0]);
		}
	}
}
#endif