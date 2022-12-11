using UnityEngine;

namespace CsCat
{
	public class ExcelDatabaseUtil
	{
		/// <summary>
		///   string转为对应的ExcelDataType类型
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ExcelDataType String2DataType(string value)
		{
			if (value.IsNullOrWhiteSpace())
				return ExcelDataType.STRING;
			value = value.ToUpper();
			value = value.Replace("[]", "_ARRAY"); //替换如int[]为INT_ARRAY
			//替换dict<string,string>  to  dict_string_string
			value = value.Replace("<", "_").Replace(">", "").Replace(",", "_");
			return value.ToEnum<ExcelDataType>();
		}

		//  /// <summary>
		//  ///   ExcelDataType转为对应Type的类型
		//  /// </summary>
		//  /// <param name="type"></param>
		//  /// <returns></returns>
		//  public static Type DataType2Type(ExcelDataType type)
		//  {
		//    switch (type)
		//    {
		//      case ExcelDataType.INT:
		//        return typeof(int);
		//      case ExcelDataType.FLOAT:
		//        return typeof(float);
		//      case ExcelDataType.Lang:
		//        return typeof(string);
		//      case ExcelDataType.VECTOR3:
		//        return typeof(Vector3);
		//      case ExcelDataType.BOOLEAN:
		//        return typeof(bool);
		//      case ExcelDataType.INT_ARRAY:
		//        return typeof(int[]);
		//      case ExcelDataType.FLOAT_ARRAY:
		//        return typeof(float[]);
		//      case ExcelDataType.BOOLEAN_ARRAY:
		//        return typeof(bool[]);
		//      case ExcelDataType.STRING_ARRAY:
		//        return typeof(string[]);
		//      case ExcelDataType.DICT_STRING_INT:
		//        return typeof(Dictionary<string,int>);
		//      case ExcelDataType.DICT_STRING_FLOAT:
		//        return typeof(Dictionary<string, float>);
		//      case ExcelDataType.DICT_STRING_BOOLEAN:
		//        return typeof(Dictionary<string, bool>);
		//      case ExcelDataType.DICT_STRING_STRING:
		//        return typeof(Dictionary<string, string>);
		//      case ExcelDataType.DICT_INT_INT:
		//        return typeof(Dictionary<int, int>);
		//      case ExcelDataType.DICT_INT_FLOAT:
		//        return typeof(Dictionary<int, float>);
		//      case ExcelDataType.DICT_INT_BOOLEAN:
		//        return typeof(Dictionary<int, bool>);
		//      case ExcelDataType.DICT_INT_STRING:
		//        return typeof(Dictionary<int, string>);
		//    }
		//
		//    return typeof(string);
		//  }

		/// <summary>
		///   将content转换为对应的列所对应的类型【type】的数据
		/// </summary>
		/// <param name="content"></param>
		/// <param name="headerType"></param>
		/// <returns></returns>
		public static object Convert(string content, ExcelDataType headerType)
		{
			object result = null;
			switch (headerType)
			{
				case ExcelDataType.INT:
				{
					int.TryParse(content, out var intValue);
					result = intValue;
					return result;
				}
				case ExcelDataType.LANG:
				{
					result = Lang.GetText(content);
					return result;
				}
				case ExcelDataType.FLOAT:
				{
					float.TryParse(content, out var floatValue);
					result = floatValue;
					return result;
				}
				case ExcelDataType.VECTOR3:
				{
					result = Vector3.zero;
					if (content.IsNullOrWhiteSpace())
						return result;
					var contents = content.Split(',');
					if (contents.Length == 3)
					{
						float.TryParse(contents[0], out var x);
						float.TryParse(contents[1], out var y);
						float.TryParse(contents[2], out var z);
						result = new Vector3(x, y, z);
						return result;
					}

					return result;
				}

				case ExcelDataType.BOOLEAN:
				{
					var boolValue = false;
					if (content.IsNullOrWhiteSpace())
						return false;
					bool.TryParse(content, out boolValue);
					result = boolValue;
					return result;
				}

				case ExcelDataType.INT_ARRAY:
					result = content.ToList<int>().ToArray();
					return result;
				case ExcelDataType.FLOAT_ARRAY:
					result = content.ToList<float>().ToArray();
					return result;
				case ExcelDataType.BOOLEAN_ARRAY:
					result = content.ToList<bool>().ToArray();
					return result;
				case ExcelDataType.STRING_ARRAY:
					result = content.ToList<string>().ToArray();
					return result;
				case ExcelDataType.DICT_STRING_INT:
					return content.ToDictionary<string, int>();
				case ExcelDataType.DICT_STRING_FLOAT:
					return content.ToDictionary<string, float>();
				case ExcelDataType.DICT_STRING_BOOLEAN:
					return content.ToDictionary<string, bool>();
				case ExcelDataType.DICT_STRING_STRING:
					return content.ToDictionary<string, string>();
			}

			result = content;
			return result;
		}
	}
}