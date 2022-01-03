namespace CsCat
{
	public partial class Polygon
	{
		public enum Classification
		{
			Contain, //包含
			Contained, //被包含
			Intersect, //相交
			Isolate //不相交,也不相互包含,没有交集，相互独立
		}
	}
}