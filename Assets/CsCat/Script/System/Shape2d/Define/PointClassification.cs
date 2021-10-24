namespace CsCat
{
    public enum PointClassification
    {
        LeftSide, //在线段的左边
        RightSide, //在线段的右边
        OnLine, //不在线段上，但在线段的延长线上
        OnSegment //在线段上
    }
}