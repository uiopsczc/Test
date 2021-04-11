using System;

namespace CsCat
{
  [Serializable]
  public class ValueParse
  {
    public string assemble_name;

    [NonSerialized] public object tmp;

    public string type_name;
    public string value;

    public ValueParse()
    {
    }

    public ValueParse(object tmp)
    {
      this.tmp = tmp;
    }

    public object Parse()
    {
      if (tmp != null)
        return tmp;

      var target_type = TypeUtil.GetType(type_name, assemble_name);
      foreach (var hashtable in ValueParseUtil.GetValueParseList())
      {
        var type = (Type) hashtable["type"];
        var parseFunc = (Delegate) hashtable["parseFunc"];
        if (type == target_type)
          return parseFunc.DynamicInvoke(value);
      }

      return null;
    }

    public T Parse<T>()
    {
      return (T) Parse();
    }
  }
}