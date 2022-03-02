namespace CsCat
{
	public partial class PropertyComp
	{
		public class Key
		{
			public string key;
			public string subKey;

			public Key(string key, string subKey)
			{
				this.key = key;
				this.subKey = subKey;
			}

			public override bool Equals(object obj)
			{
				Key other = obj as Key;
				if (other == null)
					return false;
				return ObjectUtil.Equals(other.key, key) && ObjectUtil.Equals(other.subKey, subKey);
			}

			public override int GetHashCode()
			{
				return ObjectUtil.GetHashCode(key, subKey);
			}
		}
	}
}
