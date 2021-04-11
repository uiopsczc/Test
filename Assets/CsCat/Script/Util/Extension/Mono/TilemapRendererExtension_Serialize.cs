using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CsCat
{
  public static partial class TilemapRendererExtension
  {
    public static Hashtable GetSerializeHashtable(this TilemapRenderer self)
    {
      Hashtable hashtable = new Hashtable();
      hashtable["mode"] = (int) self.mode;
      hashtable["detectChunkCullingBounds"] = (int) self.detectChunkCullingBounds;
      hashtable["sortOrder"] = (int) self.sortOrder;
      hashtable["sortingOrder"] = self.sortingOrder;
      hashtable["maskInteraction"] = (int) self.maskInteraction;
      hashtable.Trim();
      return hashtable;
    }

    public static void LoadSerializeHashtable(this TilemapRenderer self, Hashtable hashtable)
    {
      self.mode = hashtable.Get<int>("mode").ToEnum<TilemapRenderer.Mode>();
      self.detectChunkCullingBounds =
        hashtable.Get<int>("detectChunkCullingBounds").ToEnum<TilemapRenderer.DetectChunkCullingBounds>();
      self.sortOrder = hashtable.Get<int>("sortOrder").ToEnum<TilemapRenderer.SortOrder>();
      self.sortingOrder = hashtable.Get<int>("sortingOrder");
      self.maskInteraction = hashtable.Get<int>("maskInteraction").ToEnum<SpriteMaskInteraction>();
    }
  }
}