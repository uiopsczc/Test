namespace CsCat
{
  public sealed class ByteOrder
  {
    #region ctor

    private ByteOrder(string name)
    {
      this.name = name;
    }

    #endregion

    #region override method

    public override string ToString()
    {
      return name;
    }

    #endregion

    #region field

    public static readonly ByteOrder BigEndian = new ByteOrder("BIG_ENDIAN");
    public static readonly ByteOrder LittleEndian = new ByteOrder("LITTLE_ENDIAN");


    private readonly string name;

    #endregion
  }
}