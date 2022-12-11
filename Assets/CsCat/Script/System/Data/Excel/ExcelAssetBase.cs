using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class ExcelAssetBase
	{
		protected virtual string _path => null;

		private ExcelDatabase _instance;

		public string id;
		public string name;
		public string type_1;
		public string type_2;
		public string class_path_cs;
		public bool can_fold;

		protected ExcelDatabase GetInstance()
		{
			if (_instance == null)
			{
				//      LogCat.LogWarning(path);
				_instance = Resources.Load<ExcelDatabase>(_path);
			}

			return _instance;
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
			return GetIdList().ConvertAll(int.Parse);
		}
	}
}