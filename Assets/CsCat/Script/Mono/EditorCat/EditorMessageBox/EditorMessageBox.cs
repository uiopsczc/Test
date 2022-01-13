#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class EditorMessageBox : EditorWindow
	{
		public string messageTitle;
		public string content;

		public string button1Text;
		public string button2_text;

		public Action onButton1Callback;
		public Action onButton2Callback;
		public Action onCancelCallback;


		void OnGUI()
		{
			EditorGUILayout.Space(10);

			// tips
			if (!string.IsNullOrEmpty(messageTitle))
			{
				EditorGUILayout.LabelField(messageTitle, GUIStyleConst.LabelBoldMiddleCenterStyle);
				EditorGUILayout.Space(10);
			}

			EditorGUILayout.LabelField(content, GUIStyleConst.Label_MiddleCenter_Style);

			GUILayout.FlexibleSpace();
			// buttons
			using (new GUILayoutBeginHorizontalScope())
			{
				GUILayout.FlexibleSpace();
				if (!string.IsNullOrEmpty(button1Text))
				{
					if (GUILayout.Button(button1Text, GUILayout.Width(64)))
					{
						onButton1Callback?.Invoke();
						onButton1Callback = null;
						Close();
					}
				}

				if (!string.IsNullOrEmpty(button2_text))
				{
					if (GUILayout.Button(button2_text, GUILayout.Width(64)))
					{
						onButton2Callback?.Invoke();
						onButton2Callback = null;
						Close();
					}
				}
			}
		}

		void OnDestroy()
		{
			onCancelCallback?.Invoke();
		}
	}
}
#endif