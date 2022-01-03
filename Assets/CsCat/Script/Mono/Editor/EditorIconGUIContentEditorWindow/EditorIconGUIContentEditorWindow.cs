using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class EditorIconGUIContentEditorWindow : EditorWindow
	{
		private Vector2 scrollPosition;

		void OnGUI()
		{
			using (new GUILayoutBeginScrollViewScope(ref scrollPosition))
			{
				//鼠标放在按钮上的样式
				foreach (var mouseCursor in EnumUtil.GetValues<MouseCursor>())
				{
					var displayValue = string.Format("{0}.{1}", typeof(MouseCursor).GetLastName(),
						mouseCursor.ToString());
					if (GUILayout.Button(displayValue))
						this.ShowNotificationAndLog(displayValue);
					EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), mouseCursor);
					GUILayout.Space(10);
				}

				//内置图标
				int columnCount = 20;
				using (new GUILayoutBeginHorizontalScope())
				{
					for (int i = 0; i < EnumUtil.GetCount<EditorIconGUIContentType>(); ++i)
					{
						if (i > 0 && i % columnCount == 0)
						{
							GUILayout.EndHorizontal();
							GUILayout.BeginHorizontal();
						}

						var guiContent = EditorIconGUIContent.GetSystem((EditorIconGUIContentType) i);
						if (GUILayout.Button(guiContent,
							GUILayout.Width(50), GUILayout.Height(36)))
						{
							string displayValue = string.Format("EditorIconGUIContentType.{0}\n{1}",
								EnumUtil.GetName<EditorIconGUIContentType>(i),
								EditorIconGUIContentConst.IconGUIContentNames[i]
							);
							this.ShowNotificationAndLog(displayValue);
						}
					}
				}
			}
		}
	}
}