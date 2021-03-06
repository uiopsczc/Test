#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  [CustomEditor(typeof(GUIToolbarTest))]
  public class GUIToolbarTestEditor : Editor
  {
    private GUIToolbar guiToolbar;

    List<GUIContent> button_guiContent_list = new List<GUIContent>()
    {
      "A".ToGUIContent(),
      "B".ToGUIContent(),
      "C".ToGUIContent(),
      "D".ToGUIContent(),
    };

    void OnEnable()
    {
      guiToolbar = new GUIToolbar(button_guiContent_list);
      guiToolbar.OnToolSelected += OnToolSelected;
      guiToolbar.TriggerButton(1);
      //    guiToolbar.SetHighlight(3, true);
    }

    void OnDisable()
    {
      guiToolbar.OnToolSelected -= OnToolSelected;
    }

    public void OnToolSelected(GUIToolbar guiToolbar, int selected_index, int pre_selected_index)
    {
      switch (selected_index)
      {
        case 0: //对应button_guiContent_list的"A"
          LogCat.log("A");
          break;
        case 1: //对应button_guiContent_list的"B"
          LogCat.log("B");
          break;
        case 2: //对应button_guiContent_list的"C"
          LogCat.log("C");
          break;
        case 3: //对应button_guiContent_list的"D"
          LogCat.log("D");
          break;
      }
    }

    void OnSceneGUI()
    {
      guiToolbar.DrawGUI(Vector2.zero, new Vector2(40, 40), Color.white, Color.black);
    }
  }
}
#endif