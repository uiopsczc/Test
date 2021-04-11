namespace CsCat
{
  public partial class PropertyComp
  {
    public class Key
    {
      public string key;
      public string sub_key;

      public Key(string key, string sub_key)
      {
        this.key = key;
        this.sub_key = sub_key;
      }

      public override bool Equals(object obj)
      {
        Key other = obj as Key;
        if (other == null)
          return false;
        if (ObjectUtil.Equals(other.key, key) && ObjectUtil.Equals(other.sub_key, sub_key))
          return true;
        return false;
      }

      public override int GetHashCode()
      {
        return ObjectUtil.GetHashCode(key, sub_key);
      }
    }
  }
}
