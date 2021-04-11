namespace CsCat
{
  public enum PointClassification
  {
    LEFT_SIDE, //在线段的左边
    RIGHT_SIDE, //在线段的右边
    ON_LINE, //不在线段上，但在线段的延长线上
    ON_SEGMENT //在线段上
  }
}