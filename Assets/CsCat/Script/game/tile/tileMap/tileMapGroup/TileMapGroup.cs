#if MicroTileMap
#if UNITY_EDITOR
using System.Collections.Generic;
#endif
using UnityEngine;

namespace CsCat
{
[DisallowMultipleComponent]
[ExecuteInEditMode]
public class TileMapGroup : MonoBehaviour
{
  [SerializeField]
  public List<TileMap> tileMap_list = new List<TileMap>();
  [SerializeField]
  private int selected_index = -1;
  [SerializeField, Range(0f, 1f)]
  public float unselected_color_multiplier = 1f;

  public TileMap selected_tileMap
  {
    get { return selected_index >= 0 && selected_index < tileMap_list.Count ? tileMap_list[selected_index] : null; }
    set { selected_index = tileMap_list != null ? tileMap_list.IndexOf(value) : -1; }
  }
}
}
#endif