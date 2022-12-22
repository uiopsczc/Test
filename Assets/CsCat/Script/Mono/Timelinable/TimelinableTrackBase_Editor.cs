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
		[NonSerialized] public GUIToggleTween itemInfoLibraryToggleTween = new GUIToggleTween();
		[NonSerialized] public bool isItemInfoLibrarySorted;

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
			name = EditorGUILayout.TextField("trackName", name);
			DrawGUISettingDetail();
			DrawGUISettingItemInfoLibrary();
			DrawGUISetting_ItemInfoes();
		}

		public virtual void DrawGUISettingDetail()
		{
		}

		public virtual void DrawGUISetting_ItemInfoes()
		{
			List<TimelinableItemInfoBase> displayItemInfoList = new List<TimelinableItemInfoBase>();
			for (var i = 0; i < itemInfoes.Length; i++)
			{
				var itemInfo = itemInfoes[i];
				if (itemInfo.isSelected || itemInfo.IsTimeInside(curTime))
					displayItemInfoList.Add(itemInfo);
			}

			using (new GUILayoutBeginVerticalIndentLevelScope(3))
			{
				for (int i = 0; i < displayItemInfoList.Count; i++)
				{
					var displayItemInfo = displayItemInfoList[i];
					displayItemInfo.DrawGUISetting(this);
				}
			}
		}

		public virtual void DrawGUISettingItemInfoLibrary()
		{
			Type itemInfoLibraryType = this.GetFieldInfo("_itemInfoLibrary").FieldType;
			using (new GUILayoutToggleAreaScope(itemInfoLibraryToggleTween, "Library", () =>
			{
				itemInfoLibrary = (TimelinableItemInfoLibraryBase) EditorGUILayout.ObjectField(itemInfoLibrary,
					itemInfoLibraryType, false);
				if (GUILayout.Button("create", GUILayout.Width(64)))
				{
					var path = EditorUtility.SaveFilePanel(
						"保存文件到",
						"",
						string.Format("{0}.asset", itemInfoLibraryType.GetLastName()),
						"asset");
					if (!path.IsNullOrEmpty())
						itemInfoLibrary =
							typeof(ScriptableObjectUtil).InvokeGenericMethod<TimelinableItemInfoLibraryBase>(
								"CreateAsset",
								new Type[] {itemInfoLibraryType}, false, path);
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
					isItemInfoLibrarySorted = GUILayout.Toggle(isItemInfoLibrarySorted, "Sort", "button");
					if (isItemInfoLibrarySorted)
						Array.Sort(itemInfoLibrary.itemInfoes, (x, y) => x.name.AlphanumCompareTo(y.name));
					List<int> toRemoveIndexList = new List<int>();
					for (int i = 0; i < itemInfoLibrary.itemInfoes.Length; i++)
					{
						TimelinableItemInfoBase itemInfo = itemInfoLibrary.itemInfoes[i];
						using (new GUILayoutBeginHorizontalScope())
						{
							if (GUILayout.Button(itemInfo.name))
							{
								var newItemInfo = itemInfo.GetType().CreateInstance<TimelinableItemInfoBase>();
								newItemInfo.CopyFrom(itemInfo);
								newItemInfo.time = curTime;
								AddItemInfo(newItemInfo);
							}

							if (GUILayout.Button("-", GUILayout.Width(32)))
							{
								toRemoveIndexList.Add(i);
							}
						}
					}

					for (int i = toRemoveIndexList.Count - 1; i >= 0; i--)
						itemInfoLibrary.RemoveItemInfo(itemInfoLibrary.itemInfoes[i]);
				}
			}
		}
	}
}
#endif