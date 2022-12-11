using System.Collections.Generic;

namespace CsCat
{
	public class CsvData
	{
		#region field

		#region public

		/// <summary>
		/// 存数数据
		/// </summary>
		public LinkedDictionary<string, LinkedDictionary<string, string>> dataDict =
		  new LinkedDictionary<string, LinkedDictionary<string, string>>();

		/// <summary>
		/// 作为键的列名  即以（key）结尾的列名
		/// </summary>
		public List<string> keyColumnNameList = new List<string>();

		#endregion

		#region private

		/// <summary>
		/// 数据的行数
		/// </summary>
		int _dataRowCount;

		/// <summary>
		/// 文件路径
		/// </summary>
		private string _filePath;

		#endregion

		#endregion

		#region property

		public virtual string filePath => this._filePath;

		#endregion

		#region virtual method

		/// <summary>
		/// 获取对应列，对应行的值
		/// </summary>
		/// <param name="columnName"></param>
		/// <param name="rowKeys"></param>
		/// <returns></returns>
		protected virtual string GetValue(string columnName, params string[] rowKeys)
		{
			string rowKey = GetKeys(rowKeys);
			if (dataDict.ContainsKey(rowKey) && dataDict[rowKey].ContainsKey(columnName))
				return dataDict[rowKey][columnName].Trim();
			return null;
		}

		/// <summary>
		/// 刷新
		/// </summary>
		public virtual void Refresh()
		{
			Init(filePath);
		}

		#endregion

		#region public method

		public void Init(string filePath)
		{
			this._filePath = filePath;
			dataDict.Clear();
			keyColumnNameList.Clear();
			string content = FileUtilCat.ReadUnityFile(filePath);

			//读取每一行的内容
			//window用"\r\n"
			string[] lines = content.Split("\r\n".ToCharArray());
			//string[] lineArray = textAsset.text.Split("\r".ToCharArray());
			_dataRowCount = lines.Length - 1;

			string[] columnNames = lines[0].Split(',');

			for (int i = 0; i < columnNames.Length; i++)
			{
				string columnName = columnNames[i];
				int keyIndex = GetKeyIndex(columnName);
				if (keyIndex != -1)
				{
					columnName = columnName.Substring(0, keyIndex);
					keyColumnNameList.Add(columnName);
					columnNames[i] = columnName;
				}
			}

			for (int i = 1; i < lines.Length; i++)
			{
				string[] rowContents = lines[i].Split(',');
				if (rowContents[0] == "\n" || rowContents[0] == "")
				{
					_dataRowCount = _dataRowCount - 1;
					continue;
				}

				LinkedDictionary<string, string> lineDataDict = new LinkedDictionary<string, string>();
				string keys = "";

				for (int j = 0; j < columnNames.Length; j++)
				{
					bool isKey = keyColumnNameList.Contains(columnNames[j]);
					lineDataDict[columnNames[j]] = rowContents[j];
					if (isKey)
					{
						if (keys == "")
							keys = rowContents[j];
						else
							keys += "_" + rowContents;
					}
				}

				dataDict[keys] = lineDataDict;
			}

		}

		/// <summary>
		/// 行数
		/// </summary>
		/// <returns></returns>
		public int GetDataRowCount()
		{
			return _dataRowCount;
		}

		/// <summary>
		/// 获取键值 以keys[] 返回key[0]_key[1]_key[2].....
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		public string GetKeys(params string[] keys)
		{
			string key = "";
			for (var i = 0; i < keys.Length; i++)
			{
				string r = keys[i];
				if (key.IsNullOrWhiteSpace())
					key = r;
				else
					key += "_" + r;
			}

			return key;
		}

		/// <summary>
		/// 保存
		/// </summary>
		public void Save()
		{
			if (dataDict != null)
			{
				string path = CsvConst.BasePath +
							  (this.filePath.Contains(".csv") ? filePath : string.Format("{0}.csv", this.filePath));
				List<string> columnNameList = GetColumnNames();
				string columnName = "";
				for (var i = 0; i < columnNameList.Count; i++)
				{
					string column = columnNameList[i];
					if (keyColumnNameList.Contains(column))
						columnName += column + "(key)" + ",";
					else
						columnName += column + ",";
				}

				columnName = columnName.Substring(0, columnName.Length - 1); //去掉最后一个逗号
				StdioUtil.WriteTextFile(path, columnName, true);


				foreach (var kv in dataDict)
				{
					LinkedDictionary<string, string> lineDataDict = kv.Value;
					string rowValue = "";
					for (var i = 0; i < columnNameList.Count; i++)
					{
						string column = columnNameList[i];
						if (lineDataDict.ContainsKey(column))
							rowValue += lineDataDict[column] + ",";
						else
							rowValue += ",";
					}

					rowValue = rowValue.Substring(0, rowValue.Length - 1); //去掉最后一个逗号
					StdioUtil.WriteTextFile(path, rowValue, true, true);
				}

				LogCat.LogWarning("Save Finish");
			}
		}

		#endregion

		#region private method

		private int GetKeyIndex(string columnName)
		{
			string columnNameLow = columnName.ToLower();
			for (var i = 0; i < CsvConst.Key_Symbol_List.Count; i++)
			{
				string keySymbol = CsvConst.Key_Symbol_List[i];
				int index = columnNameLow.IndexOf(keySymbol);
				if (index != -1)
					return index;
			}

			return -1;
		}

		/// <summary>
		/// 获取列名
		/// </summary>
		/// <returns></returns>
		List<string> GetColumnNames()
		{
			List<string> columnNameList = new List<string>();
			foreach (var kv in dataDict)
			{
				LinkedDictionary<string, string> lineDataDict = kv.Value;
				foreach (var kv2 in lineDataDict)
				{
					var key2 = kv2.Key;
					if (!columnNameList.Contains(key2))
						columnNameList.Add(key2);
				}
			}

			return columnNameList;
		}

		#endregion



	}
}