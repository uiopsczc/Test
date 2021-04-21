namespace CsCat
{
  public class DOTweenId
  {
    public string prefix;
    public object owner;

    public DOTweenId(object owner, string prefix)
    {
      this.owner = owner;
      this.prefix = prefix;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is DOTweenId))
        return false;
      DOTweenId other = (DOTweenId)obj;
      if (ObjectUtil.Equals(this.prefix, other.prefix) && ObjectUtil.Equals(this.owner, other.owner))
        return true;
      else
        return false;
    }

    public override int GetHashCode()
    {
      return ObjectUtil.GetHashCode(owner, prefix);
    }


  }
}
