using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class EditorIconTextureEditorWindow : EditorWindow
	{
		private Vector2 scrollPosition;

		void OnGUI()
		{
			using (new GUILayoutBeginScrollViewScope(ref scrollPosition))
			{
				//内置图标
				int columnCount = 20;
				using (new GUILayoutBeginHorizontalScope())
				{
					for (int i = 0; i < EnumUtil.GetCount<EditorIconTextureType>(); ++i)
					{
						if (i > 0 && i % columnCount == 0)
						{
							GUILayout.EndHorizontal();
							GUILayout.BeginHorizontal();
						}

						if (GUILayout.Button(EditorIconTexture.GetSystem((EditorIconTextureType) i),
							GUILayout.Width(50), GUILayout.Height(36)))
						{
							string displayValue = string.Format("EditorIconTextureType.{0}\n{1}",
								EnumUtil.GetName<EditorIconTextureType>(i),
								EditorIconTextureConst.IconTextureNames[i]
							);
							this.ShowNotificationAndLog(displayValue);
						}
					}
				}
			}
		}
	}
}