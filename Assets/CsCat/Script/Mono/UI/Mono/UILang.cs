using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	[RequireComponent(typeof(Text))]
	[ExecuteInEditMode]
	public class UILang : MonoBehaviour
	{
		private Text _uiText;

		private Text uiText
		{
			get
			{
				if (_uiText == null)
					_uiText = this.GetComponent<Text>();
				return _uiText;
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
			langId = uiText.text;
		}

		public void RefreshUIText()
		{
			if (!langId.IsNullOrWhiteSpace())
				uiText.text = Lang.GetText(langId);
		}
	}
}