using UnityEngine;

namespace CsCat
{
  public class CatmullRomPoint
  {
    public Vector3 position;
    public float created_time;

    public CatmullRomPoint(Vector3 position, float created_time)
    {
      this.position = position;
      this.created_time = created_time;
    }
  }
}