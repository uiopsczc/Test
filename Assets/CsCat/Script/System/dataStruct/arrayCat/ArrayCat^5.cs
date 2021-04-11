namespace CsCat
{
  public class ArrayCat<P0, P1, P2, P3, P4> : ArrayCat<P0, P1, P2, P3>
  {
    public P4 data4;

    public ArrayCat(P0 data0, P1 data1, P2 data2, P3 data3, P4 data4) : base(data0, data1, data2, data3)
    {
      this.data4 = data4;
    }
  }
}