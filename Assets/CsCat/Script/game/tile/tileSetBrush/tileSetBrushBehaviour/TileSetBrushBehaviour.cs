#if MicroTileMap
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
//Scene场景中选中的brush
[RequireComponent(typeof(TileMap))]
[ExecuteInEditMode]
public class TileSetBrushBehaviour : MonoBehaviour, ISingleton
{
  public static TileSetBrushBehaviour instance
  {
    get { return SingletonFactory.instance.GetMono<TileSetBrushBehaviour>(); }
  }

  public TileMap tileMap;
  public TileSetBrushPaintMode paint_mode = TileSetBrushPaintMode.Pencil;
  public Vector2 offset;
  public bool is_undo_enabled = true;
  public bool is_dragging;
  public uint[,] brushPattern;
  private Vector2 pressed_position;

  void SingleInit()
  {
    gameObject.hideFlags = HideFlags.HideInHierarchy;
  }


  public static TileSelection CreateTileSelection()
  {
    if (instance.tileMap &&
        (instance.tileMap.GridWidth * instance.tileMap.GridHeight > 1))
    {
      List<uint> selection_tileData_list =
        new List<uint>(instance.tileMap.GridWidth * instance.tileMap.GridHeight);
      for (int grid_y = 0; grid_y < instance.tileMap.GridHeight; ++grid_y)
      {
        for (int grid_x = 0; grid_x < instance.tileMap.GridWidth; ++grid_x)
        {
          selection_tileData_list.Add(instance.tileMap.GetTileData(grid_x,
            instance.tileMap.GridHeight - grid_y - 1));
        }
      }

      return new TileSelection(selection_tileData_list, instance.tileMap.GridWidth);
    }

    return null;
  }


  public static TileSet GetTileSetBrushTileSet()
  {
    if (instance.tileMap)
      return instance.tileMap.tileSet;
    return null;
  }

  public static void SetVisible(bool is_visible)
  {
    if (instance && instance.tileMap)
    {
      instance.tileMap.is_visible = is_visible;
      for (int i = 0; i < instance.transform.childCount; ++i)
        instance.transform.GetChild(i).gameObject.SetActive(is_visible);
    }
  }

  public void FlipV(bool changeFlags = true)
  {
    tileMap.FlipV(changeFlags);
    tileMap.UpdateMeshImmediate();
  }

  public void FlipH(bool changeFlags = true)
  {
    tileMap.FlipH(changeFlags);
    tileMap.UpdateMeshImmediate();
  }

  public void Rot90(bool changeFlags = true)
  {
    int grid_x = TileSetBrushUtil.GetGridX(-offset, tileMap.cell_size);
    int grid_y = TileSetBrushUtil.GetGridY(-offset, tileMap.cell_size);

    offset = -new Vector2(grid_y * tileMap.cell_size.x,
      (tileMap.GridWidth - grid_x - 1) * tileMap.cell_size.y);

    tileMap.Rot90(changeFlags);
    tileMap.UpdateMeshImmediate();
  }

  public void Rot90Back(bool changeFlags = true)
  {
    //NOTE: This is a fast way to rotate back 90º by rotating forward 3 times
    for (int i = 0; i < 3; ++i)
    {
      int gridX = TileSetBrushUtil.GetGridX(-offset, tileMap.cell_size);
      int gridY = TileSetBrushUtil.GetGridY(-offset, tileMap.cell_size);
      offset = -new Vector2(gridY * tileMap.cell_size.x,
        (tileMap.GridWidth - gridX - 1) * tileMap.cell_size.y);
      tileMap.Rot90(changeFlags);
    }

    tileMap.UpdateMeshImmediate();
  }


  public TileSetBrushBehaviour GetOrCreateTileSetBrushBehaviour(TileMap tileMap)
  {
    if (this.tileMap == null)
      this.tileMap = GetComponent<TileMap>();
    is_undo_enabled = tileMap.is_enable_undo_while_painting;
    this.tileMap.tileMapColliderType = TileMapColliderType.None;
    bool is_need_refresh =
      this.tileMap.tileSet != tileMap.tileSet ||
      this.tileMap.cell_size != tileMap.cell_size;
    this.tileMap.tileSet = tileMap.tileSet;
    this.tileMap.cell_size = tileMap.cell_size;
    this.tileMap.sortingLayer = tileMap.sortingLayer;
    this.tileMap.orderInLayer = tileMap.orderInLayer;
    this.tileMap.material = tileMap.material;
    this.tileMap.tintColor = tileMap.tintColor * 0.7f;


    //NOTE: dontsave brush give a lot of problems, like duplication of brushes FindObjectsOfType is not finding them
    //brush.m_brushTilemap.Material.hideFlags = HideFlags.DontSave; 
    //brush.gameObject.hideFlags = HideFlags.HideAndDontSave;
    //---
    gameObject.hideFlags = HideFlags.HideInHierarchy;
    if (is_need_refresh)
    {
      offset = Vector2.zero;
      this.tileMap.ClearMap();
      this.tileMap.SetTileData(0, 0,
        TileSetConst
          .TileData_Empty); // setting an empty tile, map bounds increase a tile, needed to draw the brush border
      this.tileMap.UpdateMeshImmediate();
    }

    return this;
  }


  public void FloodFill(TileMap tileMap, Vector2 local_pos)
  {
    if (is_undo_enabled)
    {
#if UNITY_EDITOR
      Undo.RecordObject(tileMap, TileMapConst.Undo_Operation_Name + tileMap.name);
      Undo.RecordObjects(tileMap.GetComponentsInChildren<TileMapChunk>(),
        TileMapConst.Undo_Operation_Name + tileMap.name);
#endif
    }

    tileMap.is_undo_enabled = is_undo_enabled;

    TileMapDrawingUtil.FloodFill(tileMap, local_pos, GetBrushPattern());
    tileMap.UpdateMeshImmediate();

    tileMap.is_undo_enabled = false;
  }


  public uint[,] GetBrushPattern()
  {
    uint[,] brushPattern = new uint[tileMap.GridWidth, tileMap.GridHeight];
    for (int y = tileMap.min_grid_y; y <= tileMap.max_grid_y; ++y)
    for (int x = tileMap.min_grid_x; x <= tileMap.max_grid_x; ++x)
    {
      brushPattern[x - tileMap.min_grid_x, y - tileMap.min_grid_x] =
        tileMap.GetTileData(x, y);
    }

    return brushPattern;
  }

  public void DoPaintPressed(TileMap tileMap, Vector2 local_pos, EventModifiers modifiers = default(EventModifiers))
  {
    LogCat.log(string.Format("DoPaintPressed ({0},{1})", TileMapUtil.GetGridX(tileMap, local_pos), TileMapUtil.GetGridY(tileMap, local_pos)));            
    if (paint_mode == TileSetBrushPaintMode.Pencil)
      Paint(tileMap, local_pos);
    else
    {
      pressed_position = local_pos;
      is_dragging = true;
      offset = Vector2.zero;
      brushPattern = GetBrushPattern();
    }
  }


  public void Paint(TileMap tileMap, Vector2 local_pos, bool skipEmptyTiles = false)
  {
    int min_grid_x = this.tileMap.min_grid_x;
    int min_grid_y = this.tileMap.min_grid_y;
    int max_grid_x = this.tileMap.max_grid_x;
    int max_grid_y = this.tileMap.max_grid_y;

    if (is_undo_enabled)
    {
#if UNITY_EDITOR
      Undo.RecordObject(tileMap, TileMapConst.Undo_Operation_Name + tileMap.name);
      Undo.RecordObjects(tileMap.GetComponentsInChildren<TileMapChunk>(),
        TileMapConst.Undo_Operation_Name + tileMap.name);
#endif
    }

    tileMap.is_undo_enabled = is_undo_enabled;
    int target_grid_y = TileSetBrushUtil.GetGridY(local_pos, tileMap.cell_size);
    bool is_do_paint_empty =
      this.tileMap.GridWidth == 1 && this.tileMap.GridHeight == 1 // don't copy empty tiles
      || brushPattern != null && brushPattern.GetLength(0) == 1 &&
      brushPattern.GetLength(1) == 1; // unless the brush size is one
    is_do_paint_empty &= !skipEmptyTiles;

    

    for (int grid_y = min_grid_y; grid_y <= max_grid_y; ++grid_y, ++target_grid_y)
    {
      int target_grid_x = TileSetBrushUtil.GetGridX(local_pos, tileMap.cell_size);
      for (int grid_x = min_grid_x; grid_x <= max_grid_x; ++grid_x, ++target_grid_x)
      {
        uint tileData = this.tileMap.GetTileData(grid_x, grid_y);
        if (
          is_do_paint_empty ||
          tileData != TileSetConst.TileData_Empty
        )
        {
          tileMap.SetTileData(target_grid_x, target_grid_y, tileData);
        }
      }
    }

    tileMap.UpdateMeshImmediate();
    tileMap.is_undo_enabled = false;
  }

  public void Erase(TileMap tileMap, Vector2 local_pos)
  {
    int minGridX = this.tileMap.min_grid_x;
    int minGridY = this.tileMap.min_grid_y;
    int maxGridX = this.tileMap.max_grid_x;
    int maxGridY = this.tileMap.max_grid_y;

    if (is_undo_enabled)
    {
#if UNITY_EDITOR
      Undo.RecordObject(tileMap, TileMapConst.Undo_Operation_Name + tileMap.name);
      Undo.RecordObjects(tileMap.GetComponentsInChildren<TileMapChunk>(),
        TileMapConst.Undo_Operation_Name + tileMap.name);
#endif
    }

    tileMap.is_undo_enabled = is_undo_enabled;
    int dstGy = TileSetBrushUtil.GetGridY(local_pos, tileMap.cell_size);
    for (int gridY = minGridY; gridY <= maxGridY; ++gridY, ++dstGy)
    {
      int dstGx = TileSetBrushUtil.GetGridY(local_pos, tileMap.cell_size);
      for (int gridX = minGridX; gridX <= maxGridX; ++gridX, ++dstGx)
      {
        tileMap.SetTileData(dstGx, dstGy, TileSetConst.TileData_Empty);
      }
    }

    tileMap.UpdateMeshImmediate();
    tileMap.is_undo_enabled = false;
  }


  public void DoPaintReleased(TileMap tileMap, Vector2 local_pos, EventModifiers modifiers = default(EventModifiers))
  {
    //Debug.Log("DoPaintReleased (" + TilemapUtils.GetGridX(tilemap, localPos) + "," + TilemapUtils.GetGridY(tilemap, localPos) + ")");
    if (paint_mode != TileSetBrushPaintMode.Pencil)
    {
      Vector2 pressedPos = TileSetBrushUtil.GetSnappedPosition(pressed_position,this.tileMap.cell_size) + this.tileMap.cell_size / 2f;
      Paint(tileMap, pressedPos + (Vector2) this.tileMap.tileMapBounds.min, true);
      pressed_position = local_pos;
      this.tileMap.ClearMap();
      for (int y = 0; y < brushPattern.GetLength(1); ++y)
      for (int x = 0; x < brushPattern.GetLength(0); ++x)
      {
        this.tileMap.SetTileData(x, y, brushPattern[x, y]);
      }

      this.tileMap.UpdateMesh();
      is_dragging = false;
    }
  }


  public void DoPaintDragged(TileMap tileMap, Vector2 local_pos, EventModifiers modifiers = default(EventModifiers))
  {
    //Debug.Log("DoPaintDragged (" + TilemapUtils.GetGridX(tilemap, localPos) + "," + TilemapUtils.GetGridY(tilemap, localPos) + ")");
    if (paint_mode == TileSetBrushPaintMode.Pencil)
      Paint(tileMap, local_pos);
    else
    {
      if (is_dragging)
      {
        this.tileMap.ClearMap();
        Vector2 brushLocPos = tileMap.transform.InverseTransformPoint(transform.position);
        Vector2 startPos = TileSetBrushUtil.GetSnappedPosition(pressed_position,this.tileMap.cell_size) + this.tileMap.cell_size / 2f -
                           brushLocPos;
        Vector2 endPos = TileSetBrushUtil.GetSnappedPosition(local_pos,this.tileMap.cell_size) + this.tileMap.cell_size / 2f -
                         brushLocPos;
        bool isCtrl = (modifiers & EventModifiers.Control) != 0;
        bool isShift = (modifiers & EventModifiers.Shift) != 0;
        switch (paint_mode)
        {
          case TileSetBrushPaintMode.Line:
            if (isCtrl) TileMapDrawingUtil.DrawLineMirrored(this.tileMap, startPos, endPos, brushPattern);
            else TileMapDrawingUtil.DrawLine(this.tileMap, startPos, endPos, brushPattern);
            break;
          case TileSetBrushPaintMode.Rect:
          case TileSetBrushPaintMode.FilledRect:
          case TileSetBrushPaintMode.Ellipse:
          case TileSetBrushPaintMode.FilledEllipse:
            if (isShift)
            {
              Vector2 vTemp = endPos - startPos;
              float absX = Mathf.Abs(vTemp.x);
              float absY = Mathf.Abs(vTemp.y);
              vTemp.x = (absX > absY) ? vTemp.x : Mathf.Sign(vTemp.x) * absY;
              vTemp.y = Mathf.Sign(vTemp.y) * Mathf.Abs(vTemp.x);
              endPos = startPos + vTemp;
            }

            if (isCtrl) startPos = 2f * startPos - endPos;
            if (paint_mode == TileSetBrushPaintMode.Rect || paint_mode == TileSetBrushPaintMode.FilledRect)
              TileMapDrawingUtil.DrawRect(this.tileMap, startPos, endPos, brushPattern,
                paint_mode == TileSetBrushPaintMode.FilledRect, (modifiers & EventModifiers.Alt) != 0);
            else if (paint_mode == TileSetBrushPaintMode.Ellipse || paint_mode == TileSetBrushPaintMode.FilledEllipse)
              TileMapDrawingUtil.DrawEllipse(this.tileMap, startPos, endPos, brushPattern,
                paint_mode == TileSetBrushPaintMode.FilledEllipse);
            break;
        }

        this.tileMap.UpdateMeshImmediate();
      }
    }
  }


  public void CutRect(TileMap tilemap, int startGridX, int startGridY, int endGridX, int endGridY)
  {
    if (is_undo_enabled)
    {
#if UNITY_EDITOR
      Undo.RecordObject(tilemap, TileMapConst.Undo_Operation_Name + tilemap.name);
      Undo.RecordObjects(tilemap.GetComponentsInChildren<TileMapChunk>(),
        TileMapConst.Undo_Operation_Name + tilemap.name);
#endif
    }

    tilemap.is_undo_enabled = is_undo_enabled;

    for (int gridY = startGridY; gridY <= endGridY; ++gridY)
    {
      for (int gridX = startGridX; gridX <= endGridX; ++gridX)
      {
        tileMap.SetTileData(gridX - startGridX, gridY - startGridY, tilemap.GetTileData(gridX, gridY));
        tilemap.SetTileData(gridX, gridY, TileSetConst.TileData_Empty);
      }
    }

    tileMap.UpdateMeshImmediate();
    tilemap.UpdateMeshImmediate();

    tilemap.is_undo_enabled = false;
  }


  public void CopyRect(TileMap tilemap, int startGridX, int startGridY, int endGridX, int endGridY)
  {
    for (int gridY = startGridY; gridY <= endGridY; ++gridY)
    {
      for (int gridX = startGridX; gridX <= endGridX; ++gridX)
      {
        tileMap.SetTileData(gridX - startGridX, gridY - startGridY, tilemap.GetTileData(gridX, gridY));
      }
    }

    tileMap.UpdateMeshImmediate();
  }
}
}
#endif