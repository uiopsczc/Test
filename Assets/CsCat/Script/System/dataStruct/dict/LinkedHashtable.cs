using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CsCat
{
  public partial class LinkedHashtable : Hashtable, IToString2
  {
    #region field

    public ArrayList key_list = new ArrayList();

    #endregion



    #region property

    public override ICollection Keys
    {
      get { return key_list; }
    }

    public override ICollection Values
    {
      get
      {
        ArrayList list = new ArrayList();
        foreach (object key in Keys)
          list.Add(base[key]);
        return list;
      }
    }

    public new object this[object key]
    {
      get { return base[key]; }
      set { Put(key, value); }
    }

    #endregion

    #region override method

    public override void Add(object key, object value)
    {
      key_list.Add(key);
      base.Add(key, value);
    }

    public override void Clear()
    {
      key_list.Clear();
      base.Clear();
    }

    public override void Remove(object key)
    {
      key_list.Remove(key);
      base.Remove(key);
    }

    public override IDictionaryEnumerator GetEnumerator()
    {
      DictionaryEnumerator enumerator = new DictionaryEnumerator(key_list, (ArrayList) Values);
      return enumerator;
    }

    #endregion

    #region public method

    public void Put(object key, object value)
    {

      if (this.ContainsKey(key))
      {
        //删除原来的
        key_list.Remove(key);
      }

      //然后放到最后
      base[key] = value;
      key_list.Add(key);
    }

    public string ToString2(bool is_fill_string_with_double_quote = false)
    {
      bool first = true;
      StringBuilder sb = new StringBuilder();
      sb.Append("{");
      foreach (object key in key_list)
      {
        if (first)
          first = false;
        else
          sb.Append(",");
        sb.Append(key.ToString2(is_fill_string_with_double_quote));
        sb.Append(":");
        object value = base[key];
        sb.Append(value.ToString2(is_fill_string_with_double_quote));
      }

      sb.Append("}");
      return sb.ToString();
    }

    public new LinkedHashtable Clone() //深clone
    {
      MemoryStream stream = new MemoryStream();
      BinaryFormatter formatter = new BinaryFormatter();
      formatter.Serialize(stream, this);
      stream.Position = 0;
      return formatter.Deserialize(stream) as LinkedHashtable;
    }

    #endregion


  }
}

