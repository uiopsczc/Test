namespace CsCat
{
    public partial class Line
    {
        public enum LineClassification
        {
            Collinear, //重叠
            SegmentsIntersect, //相交
            SegmentsNotIntersect //不相交
        }
    }
}