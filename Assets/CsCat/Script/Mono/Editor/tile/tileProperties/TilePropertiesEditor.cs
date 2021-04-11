#if MicroTileMap
using UnityEditor;
using UnityEngine;
namespace CsCat
{
public class TilePropertiesEditor : EditorWindow
{
  public static TilePropertiesEditor instance { get { if (!_instance) Show(); return _instance; } }
  private static TilePropertiesEditor _instance;
  [SerializeField]
  TilePropertiesControl tilePropertiesControl = new TilePropertiesControl();
  public static void Show(TileSet tileSet = null)
  {
    _instance = (TilePropertiesEditor)EditorWindow.GetWindow(typeof(TilePropertiesEditor), false, "Tile Properties", true);
    _instance.tilePropertiesControl.tileSet = tileSet;
    if (tileSet == null)
      _instance.OnSelectionChange();
    _instance.wantsMouseMove = true;
  }

  public static void RefreshIfVisible()
  {
    if (_instance)
      _instance.Refresh();
  }

  public void Refresh()
  {
    tilePropertiesControl.tileSet = TileSetEditor.GetSelectedTileSet();
    Repaint();
  }

  public void OnSelectionChange()
  {
    Refresh();
  }

  void OnGUI()
  {
    if (tilePropertiesControl.tileSet == null)
    {
      EditorGUILayout.HelpBox("Ñ¡ÔñÒ»¸ötileset±à¼­", MessageType.Info);
      if (Event.current.type == EventType.Repaint)
        OnSelectionChange();
      Repaint();
      return;
    }

    var selected_tileMap = Selection.activeGameObject ? Selection.activeGameObject.GetComponent<TileMap>() : null;
    if (selected_tileMap && selected_tileMap.tileSet != tilePropertiesControl.tileSet)
      Refresh();
    tilePropertiesControl.Display();
    Repaint();
  }
}
}
#endif