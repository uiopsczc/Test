using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class ExcelAssetBase
	{
		protected virtual string path => null;

		private ExcelDatabase instance;

		public string id;
		public string name;
		public string type_1;
		public string type_2;
		public string class_path_cs;
		public bool can_fold;

		protected ExcelDatabase GetInstance()
		{
			if (instance == null)
			{
				//      LogCat.LogWarning(path);
				instance = Resources.Load<ExcelDatabase>(path);
			}

			return instance;
		}

		public ExcelAssetBase()
		{
			GetInstance();
		}

		public T GetData<T>(string id) where T : ExcelAssetBase
		{
			return GetInstance().GetRow<T>(id);
		}

		public T GetData<T>(int id) where T : ExcelAssetBase
		{
			return GetData<T>(id.ToString());
		}

		public List<string> GetIdList()
		{
			return GetInstance().idList;
		}

		public List<int> GetIdListAsInt()
		{
			return GetIdList().ConvertAll(t => int.Parse(t));
		}
	}
}