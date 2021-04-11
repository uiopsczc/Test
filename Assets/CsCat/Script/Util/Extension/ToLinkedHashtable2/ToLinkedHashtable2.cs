using System.Collections;

namespace CsCat
{
  public static class IsToLinkedHashtable2
  {
    public static object ToLinkedHashtable2(object o)
    {
      if (o is ICollection)
        return ((ICollection) o).ToLinkedHashtable2();
      if (o is IToLinkedHashtable2)
        return ((IToLinkedHashtable2) o).ToLinkedHashtable2();
      return o;
    }
  }
}