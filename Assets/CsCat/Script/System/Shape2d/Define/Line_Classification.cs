namespace CsCat
{
  public partial class Line
  {
    public enum LineClassification
    {
      COLLINEAR, //重叠
      SEGMENTS_INTERSECT, //相交
      SEGMENTS_NOT_INTERSECT //不相交
    }
  }
}