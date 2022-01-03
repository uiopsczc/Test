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
			if (!(obj is DOTweenId other))
				return false;
			return ObjectUtil.Equals(this.prefix, other.prefix) && ObjectUtil.Equals(this.owner, other.owner);
		}

		public override int GetHashCode()
		{
			return ObjectUtil.GetHashCode(owner, prefix);
		}
	}
}