using System;

namespace CsCat
{
	/// <summary>
	/// excel的行列的对应的值
	/// </summary>
	[Serializable]
	public class ExcelValue
	{
		#region field

		public string value;

		#endregion

		#region public method

		public override string ToString()
		{
			return this.value ?? base.ToString();
		}

		#endregion

	}
}