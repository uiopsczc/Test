using UnityEngine;
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;

namespace CsCat
{
	public partial class TimelinableTrackBase : ICopyable
	{
		[NonSerialized] public GUIToggleTween toggleTween = new GUIToggleTween();
		[NonSerialized] public GUIToggleTween itemInfoLibrary_toggleTween = new GUIToggleTween();
		[NonSerialized] public bool is_itemInfoLibrary_sorted;

		public virtual bool IsItemInfoCanAddToLibrary()
		{
			return true;
		}

		public virtual bool IsCanAddItemInfo()
		{
			return true;
		}

		public virtual void DrawGUISetting()
		{
			name = EditorGUILayout.TextField("track_name", name);
			DrawGUISetting_Detail();
			DrawGUISetting_ItemInfoLibrary();
			DrawGUISetting_ItemInfoes();
		}

		public virtual void DrawGUISetting_Detail()
		{
		}

		public virtual void DrawGUISetting_ItemInfoes()
		{
			List<TimelinableItemInfoBase> display_itemInfo_list = new List<TimelinableItemInfoBase>();
			foreach (var itemInfo in itemInfoes)
			{
				if (itemInfo.isSelected || itemInfo.IsTimeInside(curTime))
					display_itemInfo_list.Add(itemInfo);
			}

			using (new GUILayoutBeginVerticalIndentLevelScope(3))
			{
				for (int i = 0; i < display_itemInfo_list.Count; i++)
				{
					var display_itemInfo = display_itemInfo_list[i];
					display_itemInfo.DrawGUISetting(this);
				}
			}
		}

		public virtual void DrawGUISetting_ItemInfoLibrary()
		{
			Type itemInfoLibrary_type = this.GetFieldInfo("_itemInfoLibrary").FieldType;
			using (new GUILayoutToggleAreaScope(itemInfoLibrary_toggleTween, "Library", () =>
			{
				itemInfoLibrary = (TimelinableItemInfoLibraryBase)EditorGUILayout.ObjectField(itemInfoLibrary,
			itemInfoLibrary_type, false);
				if (GUILayout.Button("create", GUILayout.Width(64)))
				{
					var path = EditorUtility.SaveFilePanel(
				"保存文件到",
				"",
				string.Format("{0}.asset", itemInfoLibrary_type.GetLastName()),
				"asset");
					if (!path.IsNullOrEmpty())
						itemInfoLibrary =
					typeof(ScriptableObjectUtil).InvokeGenericMethod<TimelinableItemInfoLibraryBase>("CreateAsset",
					  new Type[] { itemInfoLibrary_type }, false, path);
				}

				using (new GUIEnabledScope(itemInfoLibrary != null))
				{
					if (GUILayout.Button("save", GUILayout.Width(64)))
					{
						EditorUtility.SetDirty(itemInfoLibrary);
						AssetDatabase.SaveAssets();
						AssetDatabase.Refresh();
					}
				}
			}))
			{
				if (itemInfoLibrary == null || itemInfoLibrary.itemInfoes.IsNullOrEmpty())
					EditorGUILayout.HelpBox("Library is empty", MessageType.Warning);
				else
				{
					is_itemInfoLibrary_sorted = GUILayout.Toggle(is_itemInfoLibrary_sorted, "Sort", "button");
					if (is_itemInfoLibrary_sorted)
						Array.Sort(itemInfoLibrary.itemInfoes, (x, y) => x.name.AlphanumCompareTo(y.name));
					List<int> to_remove_index_list = new List<int>();
					for (int i = 0; i < itemInfoLibrary.itemInfoes.Length; i++)
					{
						TimelinableItemInfoBase itemInfo = itemInfoLibrary.itemInfoes[i];
						using (new GUILayoutBeginHorizontalScope())
						{
							if (GUILayout.Button(itemInfo.name))
							{
								var new_itemInfo = itemInfo.GetType().CreateInstance<TimelinableItemInfoBase>();
								new_itemInfo.CopyFrom(itemInfo);
								new_itemInfo.time = curTime;
								AddItemInfo(new_itemInfo);
							}

							if (GUILayout.Button("-", GUILayout.Width(32)))
							{
								to_remove_index_list.Add(i);
							}
						}
					}

					for (int i = to_remove_index_list.Count - 1; i >= 0; i--)
						itemInfoLibrary.RemoveItemInfo(itemInfoLibrary.itemInfoes[i]);
				}
			}
		}
	}
}
#endif