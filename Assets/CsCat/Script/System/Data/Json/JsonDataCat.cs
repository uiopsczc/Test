namespace CsCat
{
  public class JsonDataCat
  {
    #region field

    /// <summary>
    /// 存数数据
    /// </summary>
    public LinkedHashtable data_dict = new LinkedHashtable();

    public string file_path;

    #endregion


    #region public method

    #region virtual method

    // <summary>
    /// 本地数据保存
    /// </summary>
    public virtual void SaveData()
    {
      StdioUtil.WriteTextFile(file_path, data_dict.ToString2(true));
    }

    public virtual object GetValue(string key)
    {
      return data_dict[key];
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
      Init(file_path);
    }

    protected virtual void InitDataFromOrgFile()
    {
    }

    #endregion

    public void Init(string file_path)
    {
      this.file_path = file_path;
      data_dict.Clear();
      InitDataFromOrgFile();
      string file_content = FileUtilCat.ReadUnityFile(file_path);
      data_dict = MiniJsonLinked.JsonDecode(file_content) as LinkedHashtable;
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
