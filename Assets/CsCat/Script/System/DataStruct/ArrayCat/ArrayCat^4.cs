namespace CsCat
{
	public class ArrayCat<P0, P1, P2, P3> : ArrayCat<P0, P1, P2>
	{
		public P3 data3;

		public ArrayCat(P0 data0, P1 data1, P2 data2, P3 data3) : base(data0, data1, data2)
		{
			this.data3 = data3;
		}
	}
}