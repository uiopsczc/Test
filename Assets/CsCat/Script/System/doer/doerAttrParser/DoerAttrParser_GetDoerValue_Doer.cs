using UnityEngine;

namespace CsCat
{
  public partial class DoerAttrParser
  {
    public bool GetDoerValue_Doer(Doer doer, string key, string type_string, out string result)
    {
      bool is_break = false;
      result = null;
      if (doer is Doer)
      {
        if (key.StartsWith("env.") || key.StartsWith("envt."))
        {
          Doer env = doer.GetEnv();
          if (env != null)
          {
            key = key.Substring("env".Length);
            DoerAttrParser doerAttrParser = new DoerAttrParser(env);
            result = doerAttrParser.ParseString(type_string + "u" + key);
            return true;
          }

          result = ConvertValue("", type_string);
          return true;
        }
        else if (key.StartsWith("pos2"))
        {
          key = key.Substring("pos2".Length);
          Vector2 pos2 = doer.GetPos2();
          if (pos2 != Vector2Const.Default)
          {
            if (key.Equals(".x"))
            {
              result = ConvertValue(pos2.x, type_string);
              return true;
            }

            if (key.Equals(".y"))
            {
              result = ConvertValue(pos2.y, type_string);
              return true;
            }

            result = ConvertValue(pos2.ToString(), type_string);
            return true;
          }

          result = ConvertValue("", type_string);
          return true;
        }
        else if (key.StartsWith("pos3"))
        {
          key = key.Substring("pos3".Length);
          Vector3 pos3 = doer.GetPos3();
          if (pos3 != Vector3Const.Default)
          {
            if (key.Equals(".x"))
            {
              result = ConvertValue(pos3.x, type_string);
              return true;
            }

            if (key.Equals(".y"))
            {
              result = ConvertValue(pos3.y, type_string);
              return true;
            }

            if (key.Equals(".z"))
            {
              result = ConvertValue(pos3.z, type_string);
              return true;
            }

            result = ConvertValue(pos3.ToString(), type_string);
            return true;
          }

          result = ConvertValue("", type_string);
          return true;
        }
      }

      return is_break;
    }
  }
}
