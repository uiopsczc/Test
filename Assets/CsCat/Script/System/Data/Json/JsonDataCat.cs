namespace CsCat
{
	public class JsonDataCat
	{
		#region field

		/// <summary>
		/// 存数数据
		/// </summary>
		public LinkedHashtable dataDict = new LinkedHashtable();

		public string filePath;

		#endregion


		#region public method

		#region virtual method

		// <summary>
		/// 本地数据保存
		/// </summary>
		public virtual void SaveData()
		{
			StdioUtil.WriteTextFile(filePath, dataDict.ToString2(true));
		}

		public virtual object GetValue(string key)
		{
			return dataDict[key];
		}

		public virtual T GetValue<T>(string key)
		{
			return (T)GetValue(key);
		}

		public virtual object GetValue(int key)
		{
			return GetValue(key.ToString());
		}

		public virtual T GetValue<T>(int key)
		{
			return (T)GetValue(key);
		}

		public virtual void Refresh()
		{
			Init(filePath);
		}

		protected virtual void InitDataFromOrgFile()
		{
		}

		#endregion

		public void Init(string filePath)
		{
			this.filePath = filePath;
			dataDict.Clear();
			InitDataFromOrgFile();
			string fileContent = FileUtilCat.ReadUnityFile(filePath);
			dataDict = MiniJsonLinked.JsonDecode(fileContent) as LinkedHashtable;
		}

		public LinkedHashtable GetRow(string key)
		{
			return GetValue<LinkedHashtable>(key);
		}

		public LinkedHashtable GetRow(int key)
		{
			return GetValue<LinkedHashtable>(key);
		}

		#endregion








	}
}
