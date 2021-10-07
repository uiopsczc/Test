namespace CsCat
{
  public class BitUtil
  {
    public static bool Contains(int container, int value)
    {
        return (container & value) == value;
    }
  }
}