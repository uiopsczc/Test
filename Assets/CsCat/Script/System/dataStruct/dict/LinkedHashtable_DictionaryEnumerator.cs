using System.Collections;

namespace CsCat
{
  public partial class LinkedHashtable
  {
    class DictionaryEnumerator : IDictionaryEnumerator
    {
      #region field

      ArrayList key_list;
      ArrayList value_list;
      int position = -1;

      #endregion

      #region property

      DictionaryEntry IDictionaryEnumerator.Entry
      {
        get
        {
          object key = key_list[position];
          object value = value_list[position];
          return new DictionaryEntry(key, value);
        }
      }

      object IDictionaryEnumerator.Key
      {
        get { return key_list[position]; }
      }

      object IDictionaryEnumerator.Value
      {
        get { return value_list[position]; }
      }

      object IEnumerator.Current
      {
        get
        {
          object key = key_list[position];
          object value = value_list[position];
          return new DictionaryEntry(key, value);
        }
      }

      #endregion

      #region ctor

      public DictionaryEnumerator(ArrayList key_list, ArrayList value_list)
      {
        this.key_list = key_list;
        this.value_list = value_list;
      }

      #endregion

      #region private method

      bool IEnumerator.MoveNext()
      {
        position++;
        return position < key_list.Count;
      }

      void IEnumerator.Reset()
      {
        position = -1;
      }

      #endregion

    }




  }
}

