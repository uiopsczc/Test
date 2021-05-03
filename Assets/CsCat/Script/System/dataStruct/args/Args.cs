using System;
using System.Text;

namespace CsCat
{
  public class Args
  {
    private object[] args;

    public Args()
    {
    }

    public Args(params object[] args)
    {
      Init(args);
    }

    public Args(object args0, params object[] args)
    {
      Init(args0, args);
    }

    public void Init(params object[] args)
    {
      this.args = args;
    }
    public void Init(object args0, params object[] args)
    {
      int offset = 1;
      object[] _args = new object[args?.Length + offset ?? 0];
      _args[0] = args0;
      if (args != null)
        Array.Copy(args, 0, _args, 1, args.Length);
      this.args = _args;
    }

    public override bool Equals(object obj)
    {
      Args other = (Args)obj;

      if (other == null)
        return false;

      if (this.args == null && other.args == null)
        return true;
      else if (this.args == null && other.args != null)
        return false;
      else if (this.args != null && other.args == null)
        return false;

      if (this.args.Length == other.args.Length)
      {
        for (int i = 0; i < this.args.Length; i++)
        {
          if (!ObjectUtil.Equals(args[i], other.args[i]))
          {
            return false;
          }
        }

        return true;
      }

      return false;
    }

    public override int GetHashCode()
    {
      return ObjectUtil.GetHashCode(args);
    }

    public override string ToString()
    {
      var result = new StringBuilder("(");
      if (this.args == null)
      {
        result.Append(")");
        return result.ToString();
      }
      else
      {
        for (int i = 0; i < args.Length; i++)
        {
          var arg = args[i];
          result.Append(arg);
          if (i != args.Length - 1)
            result.Append(",");
        }
        result.Append(")");
        return result.ToString();
      }
    }

//    public void OnDespawn()
//    {
//      args = null;
//    }
  }
}