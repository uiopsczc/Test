using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	[RequireComponent(typeof(Text))]
	[ExecuteInEditMode]
	public class UILang : MonoBehaviour
	{
		private Text _ui_text;

		private Text ui_text
		{
			get
			{
				if (_ui_text == null)
					_ui_text = this.GetComponent<Text>();
				return _ui_text;
			}
		}

		[TextArea] public string langId;

		public void Awake()
		{
			if (!Application.isPlaying)
			{
				if (langId.IsNullOrWhiteSpace())
					UpdateLangIdFromTextComponent();
				RefreshUIText();
			}
		}

		public void UpdateLangIdFromTextComponent()
		{
			langId = ui_text.text;
		}

		public void RefreshUIText()
		{
			if (!langId.IsNullOrWhiteSpace())
				ui_text.text = Lang.GetText(langId);
		}
	}
}