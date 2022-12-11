namespace CsCat
{
	public sealed class ByteOrder
	{
		private readonly string _name;
		public static readonly ByteOrder BigEndian = new ByteOrder("BIG_ENDIAN");
		public static readonly ByteOrder LittleEndian = new ByteOrder("LITTLE_ENDIAN");

		private ByteOrder(string name)
		{
			this._name = name;
		}


		public override string ToString()
		{
			return _name;
		}
	}
}