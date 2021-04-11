#if MicroTileMap
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
public partial class TileMap : MonoBehaviour
{
  [SerializeField]
  public int min_grid_x;
  [SerializeField]
  public int min_grid_y;
  [SerializeField]
  public int max_grid_x;
  [SerializeField]
  public int max_grid_y;
  [SerializeField]
  public Vector2 cell_size;
  [SerializeField, SortingLayer]
  public int sortingLayer = 0;
  [SerializeField]
  private string sortingLayerName = "Default";
  [SerializeField]
  public int orderInLayer = 0;
  [SerializeField]
  private Color _tintColor = Color.white;
  [SerializeField]
  private bool _is_visible = true;
  [SerializeField]
  public bool is_trigger = false;
  [SerializeField]
  public PhysicMaterial physicMaterial;
  [SerializeField]
  public PhysicsMaterial2D physicMaterial2D;
  [SerializeField]
  private Material _material;
  [SerializeField]
  private TileSet _tileSet;
  [SerializeField]
  public TileMapChunkRendererPropertiesData tileMapChunkRendererProperties = new TileMapChunkRendererPropertiesData();
  [SerializeField]
  public Vector2 parallax_factor = Vector2.one;
  [SerializeField]
  public Bounds tileMapBounds;
  [SerializeField]
  public bool is_pixel_snap;
  [SerializeField]
  public TileMapGroup parent_tileMapGroup;
  [SerializeField]
  private PhysicsMaterial2D physicsMaterial2D;
  [SerializeField]
  public bool is_allow_painting_out_of_bounds = true;
  [SerializeField]
  public bool is_auto_shrink = false;
  [SerializeField, Tooltip("Set to false when painting on big maps to improve performance.")]
  public bool is_enable_undo_while_painting = true;
}
}
#endif