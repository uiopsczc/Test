namespace CsCat
{
	/// <summary>
	///   Excel支持的数据类型，新增的在后面加
	/// </summary>
	public enum ExcelDataType
	{
		INT = 1,
		FLOAT,
		STRING,
		VECTOR3,
		LANG, //多语言字段
		BOOLEAN,
		INT_ARRAY,
		FLOAT_ARRAY,
		BOOLEAN_ARRAY,
		STRING_ARRAY,
		DICT_STRING_INT,
		DICT_STRING_FLOAT,
		DICT_STRING_STRING,
		DICT_STRING_BOOLEAN,
		DICT_INT_INT,
		DICT_INT_FLOAT,
		DICT_INT_STRING,
		DICT_INT_BOOLEAN
	}
}