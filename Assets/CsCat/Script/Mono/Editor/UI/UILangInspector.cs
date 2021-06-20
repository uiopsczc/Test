using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  [CustomEditor(typeof(UILang))]
  public class UILangInspector : Editor
  {
    private UILang _uilang;

    private UILang uilang
    {
      get
      {
        if(_uilang == null)
          _uilang = target as UILang;
        return _uilang;
      }
    }
    public override void OnInspectorGUI()
    {
      using (new EditorGUILayoutBeginHorizontalScope())
      {
        EditorGUILayout.LabelField("lang_id:", GUILayout.Width(40));
        uilang.lang_id = EditorGUILayout.TextArea(uilang.lang_id, GUILayout.Height(50));
      }
      

      if (GUILayout.Button("将TextComponent中的内容作为lang_id"))
        uilang.UpdateLangIdFromTextComponent();
      if (GUILayout.Button("更新预设的文字为对应的语言"))
        uilang.RefreshUIText();
    }
  }
}