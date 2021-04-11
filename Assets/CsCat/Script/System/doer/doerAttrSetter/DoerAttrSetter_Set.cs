namespace CsCat
{
  public partial class DoerAttrSetter
  {
    public void Set(string key, string value_expression, bool is_add)
    {
      if (this.doerAttrParser != null && key.IndexOf("{") != -1)
        key = doerAttrParser.ParseString(key);

      object object_value = doerAttrParser.Parse(value_expression);
      SetObject(key, object_value, is_add);
    }

    public void SetObject(string key, object object_value, bool is_add)
    {
      if (key.StartsWith("u.")) //主动对象属性
      {
        key = key.Substring("u.".Length);
        if (u != null)
          SetObjectValue(u, key, object_value, is_add);
        return;
      }

      if (key.StartsWith("ut.")) //主动对象临时属性
      {
        key = key.Substring("ut.".Length);
        if (u != null)
          SetObjectTmpValue(u, key, object_value, is_add);
        return;
      }

      if (key.StartsWith("o.")) //被动对象属性
      {
        key = key.Substring("o.".Length);
        if (u != null)
          SetObjectValue(o, key, object_value, is_add);
        return;
      }

      if (key.StartsWith("ot.")) //被动对象临时属性
      {
        key = key.Substring("ot.".Length);
        if (u != null)
          SetObjectTmpValue(o, key, object_value, is_add);
        return;
      }

      if (key.StartsWith("e.")) //中间对象属性
      {
        key = key.Substring("e.".Length);
        if (u != null)
          SetObjectValue(e, key, object_value, is_add);
        return;
      }

      if (key.StartsWith("et.")) //中间对象临时属性
      {
        key = key.Substring("et.".Length);
        if (u != null)
          SetObjectTmpValue(e, key, object_value, is_add);
        return;
      }


      if (key.StartsWith("m.")) // 当前或中间对象
      {
        key = key.Substring("m.".Length);
        if (m != null)
        {
          if (object_value is int)
          {
            if (is_add)
              m[key] = m.Get<int>(key) + object_value.To<int>();
            else
              m[key] = object_value.To<int>();
          }

          if (object_value is float)
          {
            if (is_add)
              m[key] = m.Get<float>(key) + object_value.To<float>();
            else
              m[key] = object_value.To<float>();
          }

          if (object_value is bool)
            m[key] = object_value.To<bool>();
          if (object_value is string)
          {
            if (object_value.Equals("nil"))
              m.Remove(key);
            else if (is_add)
              m[key] = m.GetOrAddDefault(key, () => "") + object_value.To<string>();
            else
              m[key] = object_value;
          }
        }

        return;
      }

      if (u != null)
        SetObject("u." + key, object_value, is_add);
      else if (o != null)
        SetObject("o." + key, object_value, is_add);
      else if (e != null)
        SetObject("e." + key, object_value, is_add);
      else if (m != null)
        SetObject("m." + key, object_value, is_add);
    }

  }
}