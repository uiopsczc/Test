namespace CsCat
{
  public class BitUtil
  {
    public static bool Contains(int contianer, int value)
    {
      if ((contianer & value) == value)
        return true;
      return false;
    }
  }
}