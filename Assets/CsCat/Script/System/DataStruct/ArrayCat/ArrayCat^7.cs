namespace CsCat
{
	public class ArrayCat<P0, P1, P2, P3, P4, P5, P6> : ArrayCat<P0, P1, P2, P3, P4, P5>
	{
		public P6 data6;

		public ArrayCat(P0 data0, P1 data1, P2 data2, P3 data3, P4 data4, P5 data5, P6 data6) : base(data0, data1, data2,
		  data3, data4, data5)
		{
			this.data6 = data6;
		}
	}
}