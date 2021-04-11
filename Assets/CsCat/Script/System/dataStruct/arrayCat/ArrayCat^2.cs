namespace CsCat
{
  public class ArrayCat<P0, P1> : ArrayCat<P0>
  {
    public P1 data1;

    public ArrayCat(P0 data0, P1 data1) : base(data0)
    {
      this.data1 = data1;
    }
  }
}