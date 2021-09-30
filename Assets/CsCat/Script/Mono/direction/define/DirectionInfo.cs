using UnityEngine;

namespace CsCat
{
  public class DirectionInfo
  {
    public int x;
    public int y;
    public string name;
	  public Vector2Int direction;

    public DirectionInfo(int x, int y, string name)
    {
      this.x = x;
      this.y = y;
	    direction = new Vector2Int(x,y);
			this.name = name;
    }
  }
}




