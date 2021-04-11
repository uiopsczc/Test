namespace CsCat
{
  public partial class Polygon
  {
    public enum Classification
    {
      CONTAIN, //包含
      CONTAINED, //被包含
      INTERSECT, //相交
      ISOLATE //不相交,也不相互包含,没有交集，相互独立
    }
  }
}