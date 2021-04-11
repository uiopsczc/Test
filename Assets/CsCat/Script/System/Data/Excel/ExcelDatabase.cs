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
    [SerializeField] public List<ExcelHeader> header_list = new List<ExcelHeader>();

    [SerializeField] public List<string> id_list;

    #endregion

    #region protected

    /// <summary>
    /// 表里面的数据(里面的object是转为对应要转化成的类的数据)
    /// 如TestConfig.asset每行的数据要转化为TestConfig.cs里面的数据
    /// </summary>
    [NonSerialized] public Dictionary<string, object> data_dict = new Dictionary<string, object>();

    #endregion

    #region private

    /// <summary>
    /// 所有的值
    /// </summary>
    [SerializeField] private List<ExcelRow> value_list;

    [NonSerialized] private Dictionary<string, ExcelRow> dataSource_itemList_dict;

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
      object value = null;
      if (!this.data_dict.TryGetValue(id, out value))
      {
        value = this.CreateRowInstance<T>(id);
        if (value != null)
        {
          this.data_dict[id] = value;
        }
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
      this.data_dict = new Dictionary<string, object>();
      this.dataSource_itemList_dict = null;
      this.id_list = new List<string>();
      this.value_list = new List<ExcelRow>();
      foreach (var key in data.Keys)
      {
        ExcelRow row = data[key];
        this.id_list.Add(key);
        this.value_list.Add(row);
      }
    }

    /// <summary>
    /// 获取指定index的key
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetId(int index)
    {
      return this.id_list[index];
    }

    /// <summary>
    /// 获取id的index
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int GetIndexOfId(string id)
    {
      for (int i = 0; i < this.id_list.Count; i++)
      {
        if (this.id_list[i].Equals(id))
        {
          return i;
        }
      }

      return -1;
    }

    /// <summary>
    /// 获取指定row行指定column列的数据
    /// row column  index base on 0
    /// </summary>
    /// <param name="row_index"></param>
    /// <param name="column_index"></param>
    /// <returns></returns>
    public string GetValue(int row_index, int column_index)
    {
      return this.value_list[row_index].value_list[column_index].value;
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
      if (this.id_list.Count != this.value_list.Count) //数量不等，报错
      {
        LogCat.LogErrorFormat("AssetDataSource OnAfterDeserialize Failed! keys.Count:{0}, values.Count:{1}",
          id_list.Count, this.value_list.Count);
        return default(T);
      }

      if (this.dataSource_itemList_dict == null)
      {
        this.dataSource_itemList_dict = new Dictionary<string, ExcelRow>();
        for (int i = 0; i < this.id_list.Count; i++)
        {
          this.dataSource_itemList_dict[this.id_list[i]] = this.value_list[i];
        }
      }

      ExcelRow excelRow = null;
      T result = default(T); //最终的数据
      if (this.dataSource_itemList_dict.TryGetValue(id, out excelRow))
      {
        result = Activator.CreateInstance(typeof(T), true) as T;
        Dictionary<string, MemberAccessor> accessor_dict =
          MemberAccessorPool.instance.GetAccessors(typeof(T), BindingFlagsConst.Instance);
        for (int j = 0; j < this.header_list.Count; j++) //循环每一列
        {
          ExcelHeader excelHeader = this.header_list[j];
          object value = ExcelDatabaseUtil.Convert(excelRow.value_list[j].value, excelHeader.type); //转化对对应列所对应的类型的数据
          MemberAccessor memberAccessor = null;
          if (accessor_dict.TryGetValue(excelHeader.name, out memberAccessor))
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

      return default(T);
    }

    #endregion



  }
}

