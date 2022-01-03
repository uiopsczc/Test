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
		public string button2Text;

		public Action onButton1Callback;
		public Action onButton2Callback;


		void OnGUI()
		{
			EditorGUILayout.Space(10);

			// tips
			if (!string.IsNullOrEmpty(messageTitle))
			{
				GUILayout.Label(messageTitle, GUIStyleConst.Label_MiddleCenter_Style);
				EditorGUILayout.Space(10);
			}

			GUILayout.Label(content, GUIStyleConst.LabelBoldMiddleCenterStyle);

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

				if (!string.IsNullOrEmpty(button2Text))
				{
					if (GUILayout.Button(button2Text, GUILayout.Width(64)))
					{
						onButton2Callback?.Invoke();
						onButton2Callback = null;
						Close();
					}
				}
			}
		}
	}
}