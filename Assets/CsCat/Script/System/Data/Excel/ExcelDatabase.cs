using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class ExcelDatabase : ScriptableObject
	{
		#region field

		#region public

		/// <summary>
		/// 表头
		/// </summary>
		[SerializeField] public List<ExcelHeader> headerList = new List<ExcelHeader>();

		[SerializeField] public List<string> idList;

		#endregion

		#region protected

		/// <summary>
		/// 表里面的数据(里面的object是转为对应要转化成的类的数据)
		/// 如TestConfig.asset每行的数据要转化为TestConfig.cs里面的数据
		/// </summary>
		[NonSerialized] public Dictionary<string, object> dataDict = new Dictionary<string, object>();

		#endregion

		#region private

		/// <summary>
		/// 所有的值
		/// </summary>
		[SerializeField] private List<ExcelRow> _valueList;

		[NonSerialized] private Dictionary<string, ExcelRow> _dataSourceItemListDict;

		#endregion

		#endregion

		#region public method

		/// <summary>
		/// 获取或者添加一行
		/// T要转化到的类型
		/// 如TestConfig.asset每行的数据要转化为TestConfig.cs里面的数据
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public T GetRow<T>(string id) where T : class
		{
			if (!this.dataDict.TryGetValue(id, out var value))
			{
				value = this.CreateRowInstance<T>(id);
				if (value != null)
					this.dataDict[id] = value;
			}

			T t = value as T;
			if (t == null)
				LogCat.LogWarningFormat("{0} GetRow failed with id:{1}", typeof(T), id);
			return t;
		}

		/// <summary>
		/// 设置assetData
		/// </summary>
		/// <param name="data"></param>
		public void SetAssetData(LinkedDictionary<string, ExcelRow> data)
		{
			this.dataDict = new Dictionary<string, object>();
			this._dataSourceItemListDict = null;
			this.idList = new List<string>();
			this._valueList = new List<ExcelRow>();
			foreach (var kv in data)
			{
				var key = kv.Key;
				ExcelRow row = kv.Value;
				this.idList.Add(key);
				this._valueList.Add(row);
			}
		}

		/// <summary>
		/// 获取指定index的key
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public string GetId(int index)
		{
			return this.idList[index];
		}

		/// <summary>
		/// 获取id的index
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public int GetIndexOfId(string id)
		{
			for (int i = 0; i < this.idList.Count; i++)
			{
				if (this.idList[i].Equals(id))
					return i;
			}

			return -1;
		}

		/// <summary>
		/// 获取指定row行指定column列的数据
		/// row column  index base on 0
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <param name="columnIndex"></param>
		/// <returns></returns>
		public string GetValue(int rowIndex, int columnIndex)
		{
			return this._valueList[rowIndex].valueList[columnIndex].value;
		}

		/// <summary>
		/// T要转化到的类型
		/// 如TestConfig.asset每行的数据要转化为TestConfig.cs里面的数据 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id"></param>
		/// <returns></returns>
		public T CreateRowInstance<T>(string id) where T : class
		{
			if (this.idList.Count != this._valueList.Count) //数量不等，报错
			{
				LogCat.LogErrorFormat("AssetDataSource OnAfterDeserialize Failed! keys.Count:{0}, values.Count:{1}",
				  idList.Count, this._valueList.Count);
				return default(T);
			}

			if (this._dataSourceItemListDict == null)
			{
				this._dataSourceItemListDict = new Dictionary<string, ExcelRow>();
				for (int i = 0; i < this.idList.Count; i++)
				{
					this._dataSourceItemListDict[this.idList[i]] = this._valueList[i];
				}
			}

			if (this._dataSourceItemListDict.TryGetValue(id, out var excelRow))
			{
				var result = Activator.CreateInstance(typeof(T), true) as T; //最终的数据
				Dictionary<string, MemberAccessor> accessorDict =
				  MemberAccessorPool.instance.GetAccessors(typeof(T), BindingFlagsConst.Instance);
				for (int j = 0; j < this.headerList.Count; j++) //循环每一列
				{
					ExcelHeader excelHeader = this.headerList[j];
					object value = ExcelDatabaseUtil.Convert(excelRow.valueList[j].value, excelHeader.type); //转化对对应列所对应的类型的数据
					if (accessorDict.TryGetValue(excelHeader.name, out var memberAccessor))
					{
						try
						{
							memberAccessor.SetValue(result, value); //将值设置到result中
						}
						catch
						{
							LogCat.LogErrorFormat("The value \"{0}\" is {1} in config. Please check the type you defined. ",
							  excelHeader.name, excelHeader.type);
						}
					}
				}

				return result;
			}

			return default;
		}

		#endregion



	}
}

