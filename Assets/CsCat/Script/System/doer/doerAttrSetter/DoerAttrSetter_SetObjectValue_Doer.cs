namespace CsCat
{
  public partial class DoerAttrSetter
  {
    public bool SetObjectValue_Doer(Doer doer, string key, object object_value, bool is_add)
    {
      bool is_break = false;
      if (doer is Doer)
      {
        if (key.StartsWith("env.") || key.StartsWith("envt.")) //获得属性所在的环境
        {
          Doer env = doer.GetEnv();
          if (env != null)
          {
            key = key.Substring("env".Length);
            DoerAttrSetter attrAttrSetter = new DoerAttrSetter(desc);
            attrAttrSetter.SetU(env);
            attrAttrSetter.SetObject("u" + key, object_value, is_add);
          }

          return true;
        }

        if (key.StartsWith("owner.") || key.StartsWith("ownert.")) //获得属性所在的环境
        {
          Doer owner = doer.GetOwner();
          if (owner != null)
          {
            key = key.Substring("owner".Length);
            DoerAttrSetter attrAttrSetter = new DoerAttrSetter(desc);
            attrAttrSetter.SetU(owner);
            attrAttrSetter.SetObject("u" + key, object_value, is_add);
          }

          return true;
        }
      }

      return is_break;
    }

  }

}