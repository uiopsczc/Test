using System;

namespace CsCat
{
	public class CommonUtil
	{
		public static object ConvertType(string value, Type type)
		{
			//LogCat.LogWarning(type);
			if (type == typeof(bool))
				return bool.Parse(value.ToLower());
			if (type == typeof(int))
				return int.Parse(value);
			if (type == typeof(short))
				return short.Parse(value);
			if (type == typeof(float))
				return float.Parse(value);
			if (type == typeof(double))
				return double.Parse(value);
			if (type == typeof(char))
				return char.Parse(value);
			return value;
		}
	}
}