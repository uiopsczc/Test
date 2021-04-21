using System.Collections;

namespace CsCat
{
  public class HashtableCat : Hashtable
  {
    public T Get<T>(object key)
    {
      return (T)base[key];
    }
  }
}

