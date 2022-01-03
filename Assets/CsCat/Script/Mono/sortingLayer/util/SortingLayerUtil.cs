
#if UNITY_EDITOR
using System.Reflection;
using UnityEditorInternal;

namespace CsCat
{
	public class SortingLayerUtil
	{

		public static string GetNameById(int sorting_layer_id)
		{
			int index = GetUniqueIDs().IndexOf(sorting_layer_id);
			if (index >= 0)
			{
				return GetNames()[index];
			}

			return "";
		}

		public static int GetIdByName(string sorting_layer_name)
		{
			int index = GetNames().IndexOf(sorting_layer_name);
			if (index >= 0)
			{
				return GetUniqueIDs()[index];
			}

			return -1;
		}

		// Get the sorting layer names
		public static string[] GetNames()
		{
			System.Type internalEditorUtility_type = typeof(InternalEditorUtility);
			PropertyInfo sorting_layers_property = internalEditorUtility_type.GetPropertyInfo(
			  "sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
			return (string[])sorting_layers_property.GetValue(null, new object[0]);
		}

		// Get the sorting layer UniqueIds
		public static int[] GetUniqueIDs()
		{
			System.Type internalEditorUtility_type = typeof(InternalEditorUtility);
			PropertyInfo sorting_layers_property = internalEditorUtility_type.GetPropertyInfo(
			  "sortingLayerUniqueIDs", BindingFlags.Static | BindingFlags.NonPublic);
			return (int[])sorting_layers_property.GetValue(null, new object[0]);
		}

	}
}
#endif