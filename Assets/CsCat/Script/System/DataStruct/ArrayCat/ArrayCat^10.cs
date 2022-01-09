namespace CsCat
{
	public class ArrayCat<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9> : ArrayCat<P0, P1, P2, P3, P4, P5, P6, P7, P8>
	{
		public P9 data9;

		public ArrayCat(P0 data0, P1 data1, P2 data2, P3 data3, P4 data4, P5 data5, P6 data6, P7 data7, P8 data8, P9 data9)
		  : base(data0, data1, data2, data3, data4, data5, data6, data7, data8)
		{
			this.data9 = data9;
		}


	}
}