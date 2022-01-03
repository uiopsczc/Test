using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	[CustomEditor(typeof(UILang))]
	public class UILangInspector : Editor
	{
		private UILang __uiLang;

		private UILang _uiLang
		{
			get
			{
				if (__uiLang == null)
					__uiLang = target as UILang;
				return __uiLang;
			}
		}

		public override void OnInspectorGUI()
		{
			using (new EditorGUILayoutBeginHorizontalScope())
			{
				EditorGUILayout.LabelField("langId:", GUILayout.Width(40));
				_uiLang.langId = EditorGUILayout.TextArea(_uiLang.langId, GUILayout.Height(50));
			}


			if (GUILayout.Button("将TextComponent中的内容作为langId"))
				_uiLang.UpdateLangIdFromTextComponent();
			if (GUILayout.Button("更新预设的文字为对应的语言"))
				_uiLang.RefreshUIText();
		}
	}
}