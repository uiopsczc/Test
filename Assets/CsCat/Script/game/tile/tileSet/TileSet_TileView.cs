#if MicroTileMap
using System;
using UnityEngine;

namespace CsCat
{
public partial class TileSet
{
  public void AddTileView(string name, TileSelection tileSelection, int index = -1)
  {
    index = index >= 0 ? Mathf.Min(index, tileView_list.Count) : tileView_list.Count;
    string tileView_name = name;
    tileView_list.Insert(index, new TileView(tileView_name, tileSelection));
  }

  public void RemoveTileView(string name)
  {
    tileView_list.RemoveAll(x => x.name == name);
  }
  //оп├Ч├ч
  public void RenameTileView(string name, string new_name)
  {
    int index = tileView_list.FindIndex(x => x.name == name);
    if (index >= 0)
    {
      TileView tileView = tileView_list[index];
      RemoveTileView(name);
      AddTileView(new_name, tileView.tileSelection, index);
    }
  }

  public void RemoveAllTileViewList()
  {
    tileView_list.Clear();
  }
  public void SortTileViewListByName()
  {
    tileView_list.Sort((a, b) => a.name.CompareTo(b.name));
  }
}
}
#endif