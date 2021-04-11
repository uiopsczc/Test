#if MicroTileMap
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
public partial class TileSet
{
  public int _selected_tileSetBrushId = -1;
  Dictionary<int, TileSetBrush> tileSetBrush_cache_dcit = new Dictionary<int, TileSetBrush>();
  public Action<TileSet, int, int> OnTileSetBrushSelected;
  public int selected_tileSetBrushId
  {
    get { return _selected_tileSetBrushId; }
    set
    {
      int prev_tileSetBrushId = _selected_tileSetBrushId;
      _selected_tileSetBrushId = Mathf.Clamp(value, -1, tile_list.Count - 1);
      _selected_tileSetBrushId =
        _selected_tileSetBrushId & TileSetConst.TileDataMask_TileId; // convert -1 in k_TileId_Empty            

      _selected_tileId = TileSetConst.TileId_Empty;
      _tileSelection = null;
      OnTileSetBrushSelected.InvokeIfNotNull(this, prev_tileSetBrushId, _selected_tileSetBrushId);
    }
  }
  public int tileSetBrush_type_mask { get { return _tileSetBrush_type_mask; } set { _tileSetBrush_type_mask = value; } }
  


  public void AddTileSetBrush(TileSetBrush tileSetBrush)
  {
    if (tileSetBrush.tileSet == this)
    {
      if (!tileSetBrush_list.Exists(x => x.tileSetBrush == tileSetBrush))
      {
        int id = tileSetBrush_list.Count > 0 ? tileSetBrush_list[tileSetBrush_list.Count - 1].id : 1; //NOTE: id 0 is reserved for default brush 
        int max_id = (int)TileSetConst.TileDataMask_TileSetBrushId >> 16;
        if (tileSetBrush_list.Count >= max_id)
          LogCat.error(string.Format(" Max number of brushes reached! {0}" , max_id));
        else
        {
          // find a not used id
          while (tileSetBrush_list.Exists(x => x.id == id))
          {
            ++id;
            if (id > max_id)
              id = 1;
          }
          tileSetBrush_list.Add(new TileSetBrushContainer() { id = id, tileSetBrush = tileSetBrush });
          tileSetBrush_cache_dcit.Clear();
        }
      }
    }
    else
      Debug.LogWarning(string.Format("This brush {0} has a different tileset and will not be added! ", tileSetBrush.name));
  }

  public void RemoveInvalidTileSetBrushList()
  {
    tileSetBrush_list.RemoveAll(x => x.tileSetBrush == null || x.tileSetBrush.tileSet != this);
    tileSetBrush_cache_dcit.Clear();
  }


  public string[] UpdateTileSetBrushTypeArray()
  {
    List<string> out_list = new List<string>();
    for (int i = 0; i < tileSetBrush_list.Count; ++i)
    {
      TileSetBrush tileSetBrush = tileSetBrush_list[i].tileSetBrush;
      if (tileSetBrush)
      {
        string type = tileSetBrush.GetType().Name;
        if (!out_list.Contains(type))
          out_list.Add(type);
      }
    }
    tileSetBrush_type_mask_options = out_list.ToArray();
    return tileSetBrush_type_mask_options;
  }

  public string[] GetTileSetBrushTypeArray()
  {
    if (tileSetBrush_type_mask_options == null || tileSetBrush_type_mask_options.Length == 0)
      UpdateTileSetBrushTypeArray();
    return tileSetBrush_type_mask_options;
  }


  public bool IsTileSetBrushVisibleByTypeMask(TileSetBrush tileSetBrush)
  {
    if (tileSetBrush)
    {
      string[] tileSetBrushTypes = GetTileSetBrushTypeArray();
      if (tileSetBrushTypes != null && tileSetBrushTypes.Length > 0)
      {
        int idx = Array.IndexOf(tileSetBrushTypes, tileSetBrush.GetType().Name);
        return ((1 << idx) & tileSetBrush_type_mask) != 0;
      }
    }
    return false;
  }


  public TileSetBrush FindTileSetBrush(int brushId)
  {
    if (brushId <= 0) return null;

    TileSetBrush tileSetBrush = null;
    if (!tileSetBrush_cache_dcit.TryGetValue(brushId, out tileSetBrush))
    {
      tileSetBrush = FindBrushContainerByBrushId(brushId).tileSetBrush;
      tileSetBrush_cache_dcit[brushId] = tileSetBrush;
      //Debug.Log(" Cache miss! " + tileBrush.name);
    }
    return tileSetBrush;
  }

  private TileSetBrushContainer FindBrushContainerByBrushId(int brushId)
  {
    for (int i = 0; i < tileSetBrush_list.Count; ++i)
      if (tileSetBrush_list[i].id == brushId)
        return tileSetBrush_list[i];
    return default(TileSetBrushContainer);
  }
}
}
#endif