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
		public LinkedDictionary<string, LinkedDictionary<string, string>> data_dict =
		  new LinkedDictionary<string, LinkedDictionary<string, string>>();

		/// <summary>
		/// 作为键的列名  即以（key）结尾的列名
		/// </summary>
		public List<string> key_column_name_list = new List<string>();

		#endregion

		#region private

		/// <summary>
		/// 数据的行数
		/// </summary>
		int data_row_count;

		/// <summary>
		/// 文件路径
		/// </summary>
		private string _file_path;

		#endregion

		#endregion

		#region property

		public virtual string file_path
		{
			get { return this._file_path; }
		}

		#endregion

		#region virtual method

		/// <summary>
		/// 获取对应列，对应行的值
		/// </summary>
		/// <param name="column_name"></param>
		/// <param name="row_keys"></param>
		/// <returns></returns>
		protected virtual string GetValue(string column_name, params string[] row_keys)
		{
			string row_key = GetKeys(row_keys);
			if (data_dict.ContainsKey(row_key) && data_dict[row_key].ContainsKey(column_name))
				return data_dict[row_key][column_name].Trim();
			return null;
		}

		/// <summary>
		/// 刷新
		/// </summary>
		public virtual void Refresh()
		{
			Init(file_path);
		}

		#endregion

		#region public method

		public void Init(string file_path)
		{
			this._file_path = file_path;
			data_dict.Clear();
			key_column_name_list.Clear();
			string content = FileUtilCat.ReadUnityFile(file_path);

			//读取每一行的内容
			//window用"\r\n"
			string[] lines = content.Split("\r\n".ToCharArray());
			//string[] lineArray = textAsset.text.Split("\r".ToCharArray());
			data_row_count = lines.Length - 1;

			string[] column_names = lines[0].Split(',');

			for (int i = 0; i < column_names.Length; i++)
			{
				string column_name = column_names[i];
				int key_index = GetKeyIndex(column_name);
				if (key_index != -1)
				{
					column_name = column_name.Substring(0, key_index);
					key_column_name_list.Add(column_name);
					column_names[i] = column_name;
				}
			}

			for (int i = 1; i < lines.Length; i++)
			{
				string[] row_contents = lines[i].Split(',');
				if (row_contents[0] == "\n" || row_contents[0] == "")
				{
					data_row_count = data_row_count - 1;
					continue;
				}

				LinkedDictionary<string, string> line_data_dict = new LinkedDictionary<string, string>();
				string keys = "";

				for (int j = 0; j < column_names.Length; j++)
				{
					bool isKey = key_column_name_list.Contains(column_names[j]);
					line_data_dict[column_names[j]] = row_contents[j];
					if (isKey)
					{
						if (keys == "")
							keys = row_contents[j];
						else
							keys += "_" + row_contents;
					}
				}

				data_dict[keys] = line_data_dict;
			}

		}

		/// <summary>
		/// 行数
		/// </summary>
		/// <returns></returns>
		public int GetDataRowCount()
		{
			return data_row_count;
		}

		/// <summary>
		/// 获取键值 以keys[] 返回key[0]_key[1]_key[2].....
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		public string GetKeys(params string[] keys)
		{
			string key = "";
			foreach (string r in keys)
			{
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
			if (data_dict != null)
			{
				string path = CsvConst.BasePath +
							  (this.file_path.Contains(".csv") ? file_path : string.Format("{0}.csv", this.file_path));
				List<string> column_name_list = GetColumnNames();
				string column_name = "";
				foreach (string column in column_name_list)
				{
					if (key_column_name_list.Contains(column))
						column_name += column + "(key)" + ",";
					else
						column_name += column + ",";
				}

				column_name = column_name.Substring(0, column_name.Length - 1); //去掉最后一个逗号
				StdioUtil.WriteTextFile(path, column_name, true);


				foreach (LinkedDictionary<string, string> line_data_dict in data_dict.Values)
				{
					string row_value = "";
					foreach (string column in column_name_list)
					{
						if (line_data_dict.ContainsKey(column))
							row_value += line_data_dict[column] + ",";
						else
							row_value += ",";
					}

					row_value = row_value.Substring(0, row_value.Length - 1); //去掉最后一个逗号
					StdioUtil.WriteTextFile(path, row_value, true, true);
				}

				LogCat.LogWarning("Save Finish");
			}
		}

		#endregion

		#region private method

		private int GetKeyIndex(string colum_name)
		{
			string column_name_low = colum_name.ToLower();
			foreach (string key_symbol in CsvConst.Key_Symbol_List)
			{
				int index = column_name_low.IndexOf(key_symbol);
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
			List<string> column_name_list = new List<string>();
			foreach (LinkedDictionary<string, string> line_data_dict in data_dict.Values)
			{
				foreach (string key in line_data_dict.Keys)
				{
					if (!column_name_list.Contains(key))
						column_name_list.Add(key);
				}
			}

			return column_name_list;
		}

		#endregion



	}
}