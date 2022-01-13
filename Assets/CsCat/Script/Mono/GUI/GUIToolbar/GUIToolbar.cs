#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class GUIToolbar
	{
		private List<GUIContent> buttonGUIContentList;
		private int _selectedIndex = -1;
		private bool[] isHighlighted;

		public int selectedIndex
		{
			get => _selectedIndex;
			set => _selectedIndex = value;
		}

		public Action<GUIToolbar, int, int> onToolSelected;

		public GUIToolbar(List<GUIContent> buttonGUIContentList)
		{
			this.buttonGUIContentList = new List<GUIContent>(buttonGUIContentList);
			isHighlighted = new bool[this.buttonGUIContentList.Count];
		}

		public void SetHighlight(int index, bool isHighlight)
		{
			if (index >= 0 && index < isHighlighted.Length)
				isHighlighted[index] = isHighlight;
		}

		public void DrawGUI(Vector2 position, Vector2 buttonSize, Color? backgroundColor, Color? outlineColor)
		{
			Color backgroundColorValue = backgroundColor.GetValueOrDefault(Color.white);
			Color outlineColorValue = outlineColor.GetValueOrDefault(Color.black);
			using (new GUIColorScope())
			{
				int buttonCount = buttonGUIContentList.Count;
				Rect toolbarRect = new Rect(position.x, position.y, buttonCount * buttonSize.y, buttonSize.y);
				using (new GUILayoutBeginAreaScope(toolbarRect))
				{
					DrawUtil.HandlesDrawSolidRectangleWithOutline(new Rect(Vector2.zero, toolbarRect.size),
						backgroundColorValue,
						outlineColorValue);
					using (new GUILayoutBeginHorizontalScope())
					{
						if (isHighlighted.Length != buttonGUIContentList.Count)
							Array.Resize(ref isHighlighted, buttonGUIContentList.Count);

						int buttonPadding = 4;
						Rect toolButtonRect = new Rect(buttonPadding, buttonPadding,
							toolbarRect.size.y - 2 * buttonPadding,
							toolbarRect.size.y - 2 * buttonPadding);
						for (int index = 0; index < buttonGUIContentList.Count; ++index)
						{
							DrawToolbarButton(toolButtonRect, index);
							toolButtonRect.x = toolButtonRect.xMax + 2 * buttonPadding;
						}
					}
				}
			}
		}

		public void TriggerButton(int index)
		{
			int preIndex = _selectedIndex;
			_selectedIndex = index;
			onToolSelected?.Invoke(this, _selectedIndex, preIndex);
		}

		private void DrawToolbarButton(Rect toolButtonRect, int index)
		{
			int iconPadding = 6;
			Rect toolIconRect = new Rect(toolButtonRect.x + iconPadding, toolButtonRect.y + iconPadding,
				toolButtonRect.size.x - 2 * iconPadding, toolButtonRect.size.y - 2 * iconPadding);

			if (isHighlighted[index])
				GUI.color = GUIToolbarConst.Highlith_Color;
			else
				GUI.color = _selectedIndex == index ? GUIToolbarConst.Active_Color : GUIToolbarConst.Disable_Color;
			if (GUI.Button(toolButtonRect, buttonGUIContentList[index]))
				TriggerButton(index);
			GUI.color = Color.white;
			if (buttonGUIContentList[index].image)
				GUI.DrawTexture(toolIconRect, buttonGUIContentList[index].image);
		}
	}
}

#endif