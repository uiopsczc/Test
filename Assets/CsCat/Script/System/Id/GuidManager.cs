namespace CsCat
{
	public class GuidManager
	{
		private ulong _keyNumber;

		public GuidManager(ulong currentKeyNumber)
		{
			this._keyNumber = currentKeyNumber;
		}

		public GuidManager()
		{
		}

		public string NewGuid(string id = null)
		{
			_keyNumber++;
			return (id.IsNullOrWhiteSpace() ? StringConst.String_Empty : id) + IdConst.Rid_Infix + _keyNumber;
		}
	}
}