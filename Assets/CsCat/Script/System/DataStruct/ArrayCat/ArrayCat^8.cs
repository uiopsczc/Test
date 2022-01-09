namespace CsCat
{
	public class ArrayCat<P0, P1, P2, P3, P4, P5, P6, P7> : ArrayCat<P0, P1, P2, P3, P4, P5, P6>
	{
		public P7 data7;

		public ArrayCat(P0 data0, P1 data1, P2 data2, P3 data3, P4 data4, P5 data5, P6 data6, P7 data7) : base(data0,
			data1,
			data2, data3, data4, data5, data6)
		{
			this.data7 = data7;
		}
	}
}