using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsCat
{
	public class SerializeData<T> where T : SerializeData<T>, new()
	{
		public static T _instance;
		private readonly List<PropObserver> dataList = new List<PropObserver>();

		public static T instance
		{
			get
			{
				if (_instance == null)
				{
					bool isNewCreate = false;
					_instance = Load(ref isNewCreate);
					if (isNewCreate)
						_instance.OnNewCreate();
					_instance.OnLoaded();
				}

				return _instance;
			}
		}

		protected virtual void AddDataList()
		{
		}

		protected void AddToDataList(PropObserver data)
		{
			dataList.Add(data);
		}

		protected virtual void OnNewCreate()
		{
			for (var i = 0; i < dataList.Count; i++)
			{
				var data = dataList[i];
				data.OnNewCreate();
			}
		}

		protected virtual void OnLoaded()
		{
			for (var i = 0; i < dataList.Count; i++)
			{
				var data = dataList[i];
				data.OnLoaded();
			}
		}

		protected void RemoveAllListeners()
		{
			for (var i = 0; i < dataList.Count; i++)
			{
				var data = dataList[i];
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
				var conentBytes = StdioUtil.ReadFile(SerializeDataConst.SaveFilePathCS);
				//conentBytes = CompressUtil.GZipDecompress(conentBytes);--解压缩
				var content = Encoding.UTF8.GetString(conentBytes);
				data = JsonSerializer.Deserialize(content) as T;
			}

			data.AddDataList();
			return data;
		}
	}
}