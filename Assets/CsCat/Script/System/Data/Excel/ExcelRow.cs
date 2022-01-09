using System;
using System.Collections.Generic;

namespace CsCat
{
	/// <summary>
	/// excel的一行的值
	/// </summary>
	[Serializable]
	public class ExcelRow
	{
		#region field

		public List<ExcelValue> valueList = new List<ExcelValue>();

		#endregion

	}
}