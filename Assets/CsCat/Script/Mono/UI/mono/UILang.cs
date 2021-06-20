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

    [TextArea] public string lang_id;

    public void Awake()
    {
      if (!Application.isPlaying)
      {
        if (lang_id.IsNullOrWhiteSpace())
          UpdateLangIdFromTextComponent();
        RefreshUIText();
      }
    }

    public void UpdateLangIdFromTextComponent()
    {
      lang_id = ui_text.text;
    }

    public void RefreshUIText()
    {
      if (!lang_id.IsNullOrWhiteSpace())
        ui_text.text = Lang.GetText(lang_id);
    }
  }
}