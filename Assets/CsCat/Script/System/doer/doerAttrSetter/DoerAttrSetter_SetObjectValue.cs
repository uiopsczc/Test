namespace CsCat
{
  public partial class DoerAttrSetter
  {
    public void SetObjectValue(Doer doer, string key, object object_value, bool is_add)
    {
      if (SetObjectValue_User(doer, key, object_value, is_add))
        return;

      if (SetObjectValue_Doer(doer, key, object_value, is_add))
        return;

      if ("nil".Equals(object_value))
      {
        doer.Remove<object>(key);
        return;
      }

      if (!is_add)
      {
        doer.Set(key, object_value);
        return;
      }

      // add
      if (object_value is int)
      {
        doer.Add(key, (int) object_value);
        return;
      }

      if (object_value is float)
      {
        doer.Add(key, (float) object_value);
        return;
      }

      if (object_value is string)
      {
        doer.Add(key, (string) object_value);
        return;
      }
    }

    public void SetObjectTmpValue(Doer doer, string key, object object_value, bool is_add)
    {
      if ("nil".Equals(object_value))
      {
        doer.RemoveTmp<object>(key);
        return;
      }

      if (!is_add)
      {
        doer.SetTmp(key, object_value);
        return;
      }

      // add
      if (object_value is int)
      {
        doer.AddTmp(key, (int) object_value);
        return;
      }

      if (object_value is float)
      {
        doer.AddTmp(key, (float) object_value);
        return;
      }

      if (object_value is string)
      {
        doer.AddTmp(key, (string) object_value);
        return;
      }
    }



  }
}