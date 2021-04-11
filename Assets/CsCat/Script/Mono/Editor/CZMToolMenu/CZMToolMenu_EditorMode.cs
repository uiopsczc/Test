using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class CZMToolMenu
  {

    [MenuItem(CZMToolConst.MenuRoot + "/EditorMode/设置为EditorMode")]
    public static void SetToEditorMode()
    {
      EditorModeConst.Is_Editor_Mode = true;
    }
    [MenuItem(CZMToolConst.MenuRoot + "/EditorMode/设置为EditorMode",true)]
    public static bool CanSetToEditorMode()
    {
      return !EditorModeConst.Is_Editor_Mode;
    }

    [MenuItem(CZMToolConst.MenuRoot + "/EditorMode/设置为SimulationMode")]
    public static void SetToSimulationMode()
    {
      EditorModeConst.Is_Editor_Mode = false;
    }
    [MenuItem(CZMToolConst.MenuRoot + "/EditorMode/设置为SimulationMode", true)]
    public static bool CanSetToSimulationMode()
    {
      return EditorModeConst.Is_Editor_Mode;
    }

  }
}