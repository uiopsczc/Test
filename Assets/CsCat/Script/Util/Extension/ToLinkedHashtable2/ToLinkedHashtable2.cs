using System.Collections;

namespace CsCat
{
  public static class IsToLinkedHashtable2
  {
    public static object ToLinkedHashtable2(object o)
    {
      switch (o)
      {
          case ICollection collection:
              return collection.ToLinkedHashtable2();
          case IToLinkedHashtable2 hashtable2:
              return hashtable2.ToLinkedHashtable2();
          default:
              return o;
      }
    }
  }
}