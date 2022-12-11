using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsCat
{
	public class SerializeData<T> where T : SerializeData<T>, new()
	{
		public static T _instance;
		private readonly List<PropObserver> _dataList = new List<PropObserver>();

		public static T instance
		{
			get
			{
				if (_instance == null)
				{
					bool isNewCreate = false;
					_instance = Load(ref isNewCreate);
					if (isNewCreate)
						_instance._OnNewCreate();
					_instance._OnLoaded();
				}

				return _instance;
			}
		}

		protected virtual void _AddDataList()
		{
		}

		protected void _AddToDataList(PropObserver data)
		{
			_dataList.Add(data);
		}

		protected virtual void _OnNewCreate()
		{
			for (var i = 0; i < _dataList.Count; i++)
			{
				var data = _dataList[i];
				data.OnNewCreate();
			}
		}

		protected virtual void _OnLoaded()
		{
			for (var i = 0; i < _dataList.Count; i++)
			{
				var data = _dataList[i];
				data.OnLoaded();
			}
		}

		protected void _RemoveAllListeners()
		{
			for (var i = 0; i < _dataList.Count; i++)
			{
				var data = _dataList[i];
				data.RemoveAllListeners();
			}
		}

		public virtual void Save()
		{
			var content = JsonSerializer.Serialize(this);
			var contentBytes = Encoding.UTF8.GetBytes(content);
			//contentBytes = CompressUtil.GZipCompress(contentBytes);//压缩
			StdioUtil.WriteFile(SerializeDataConst.SaveFilePathCS, contentBytes);
		}

		public static T Load(ref bool isNewCreate)
		{
			T data;
			if (!File.Exists(SerializeDataConst.SaveFilePathCS))
			{
				data = new T();
				isNewCreate = true;
			}
			else
			{
				var contentBytes = StdioUtil.ReadFile(SerializeDataConst.SaveFilePathCS);
				//contentBytes = CompressUtil.GZipDecompress(contentBytes);--解压缩
				var content = Encoding.UTF8.GetString(contentBytes);
				data = JsonSerializer.Deserialize(content) as T;
			}

			data._AddDataList();
			return data;
		}
	}
}