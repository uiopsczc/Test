using UnityEngine;

namespace CsCat
{
  public class WayPoint
  {
    #region field

    public Vector2 position;
    public Cell cell;

    #endregion

    #region ctor

    public WayPoint(Cell cell, Vector2 position)
    {
      this.cell = cell;
      this.position = position;
    }

    #endregion

  }
}
