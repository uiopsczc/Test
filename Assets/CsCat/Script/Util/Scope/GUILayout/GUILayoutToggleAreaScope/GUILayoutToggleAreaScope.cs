#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class GUILayoutToggleAreaScope : IDisposable
	{
		public GUILayoutToggleAreaScope(GUIToggleTween tween, string text, Action action = null) : this(tween, text,
			action,
			EditorStyles.helpBox)
		{
		}

		public GUILayoutToggleAreaScope(GUIToggleTween tween, string text, Action action, GUIStyle style)
		{
			//计算要显示的大小
			Rect lastRect = tween.lastRect;
			lastRect.height = lastRect.height < 29 ? 29 : lastRect.height;
			lastRect.height -= 29;
			// t 时间
			// b 开始值
			// c 增量值  （结束值= 开始值 + 增量值）所以该值应该是（增量值 = 结束值 - 开始值）
			// d 总时长
			float pct = EaseCat.Quad.EaseOut2(0, 1, tween.value);
			lastRect.height *= pct;
			lastRect.height += 29;
			lastRect.height = Mathf.Round(lastRect.height);

			//显示区域布局、开始Area、BeginVertical计算真实大小
			Rect gotLastRect = GUILayoutUtility.GetRect(new GUIContent(), style, GUILayout.Height(lastRect.height));
			GUILayout.BeginArea(lastRect, style);
			Rect newRect = EditorGUILayout.BeginVertical();

			if (Event.current.type == EventType.Repaint || Event.current.type == EventType.ScrollWheel)
			{
				//计算真实大小
				newRect.x = gotLastRect.x;
				newRect.y = gotLastRect.y;
				newRect.width = gotLastRect.width;
				newRect.height += style.padding.top + style.padding.bottom;
				tween.lastRect = newRect;

				//计算插值
				if (Event.current.type == EventType.Repaint)
				{
					float deltaTime = Time.realtimeSinceStartup - tween.lastUpdateTime;
					tween.lastUpdateTime = Time.realtimeSinceStartup;
					tween.value = Mathf.Clamp01(tween.isOpened
						? tween.value + deltaTime / GUILayoutToggleAreaScopeConst.Tween_Duration
						: tween.value - deltaTime / GUILayoutToggleAreaScopeConst.Tween_Duration);
				}
			}

			using (new GUILayoutBeginHorizontalScope())
			{
				using (new GUIBackgroundColorScope(Color.gray.SetA(0.5f)))
				{
					if (GUILayout.Button(
						string.Format("{0}{1}", tween.isOpened ? "▼" : "►", text),
						GUIStyleConst.LabelBoldMiddleLeftStyle))
						tween.isOpened = !tween.isOpened;
				}

				action?.Invoke();
			}

			GUILayout.Space(11);
		}

		public void Dispose()
		{
			EditorGUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}
}
#endif