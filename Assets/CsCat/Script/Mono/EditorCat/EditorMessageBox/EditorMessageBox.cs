#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class EditorMessageBox : EditorWindow
	{
		public string message_title;
		public string content;

		public string button1_text;
		public string button2_text;

		public Action on_button1_callback;
		public Action on_button2_callback;
		public Action on_cancel_callback;


		void OnGUI()
		{
			EditorGUILayout.Space(10);

			// tips
			if (!string.IsNullOrEmpty(message_title))
			{
				EditorGUILayout.LabelField(message_title, GUIStyleConst.LabelBoldMiddleCenterStyle);
				EditorGUILayout.Space(10);
			}

			EditorGUILayout.LabelField(content, GUIStyleConst.Label_MiddleCenter_Style);

			GUILayout.FlexibleSpace();
			// buttons
			using (new GUILayoutBeginHorizontalScope())
			{
				GUILayout.FlexibleSpace();
				if (!string.IsNullOrEmpty(button1_text))
				{
					if (GUILayout.Button(button1_text, GUILayout.Width(64)))
					{
						on_button1_callback?.Invoke();
						on_button1_callback = null;
						Close();
					}
				}

				if (!string.IsNullOrEmpty(button2_text))
				{
					if (GUILayout.Button(button2_text, GUILayout.Width(64)))
					{
						on_button2_callback?.Invoke();
						on_button2_callback = null;
						Close();
					}
				}
			}
		}

		void OnDestroy()
		{
			on_cancel_callback?.Invoke();
		}
	}
}
#endif