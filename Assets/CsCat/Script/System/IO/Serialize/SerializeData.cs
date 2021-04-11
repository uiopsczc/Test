using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsCat
{
  public class SerializeData<T> where T : SerializeData<T>, new()
  {
    public static T _instance;
    private readonly List<PropObserver> data_list = new List<PropObserver>();

    public static T instance
    {
      get
      {
        if (_instance == null)
        {
          bool is_new_create = false;
          _instance = Load(ref is_new_create);
          if (is_new_create)
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
      data_list.Add(data);
    }

    protected virtual void OnNewCreate()
    {
      data_list.ForEach(data => data.OnNewCreate());
    }

    protected virtual void OnLoaded()
    {
      data_list.ForEach(data => data.OnLoaded());
    }

    protected void RemoveAllListeners()
    {
      data_list.ForEach(data => data.RemoveAllListeners());
    }

    public virtual void Save()
    {
      var content = JsonSerializer.Serialize(this);
      var contentBytes = Encoding.UTF8.GetBytes(content);
      //contentBytes = CompressUtil.GZipCompress(contentBytes);//压缩
      StdioUtil.WriteFile(SerializeDataConst.Save_File_Path_cs, contentBytes);
    }

    public static T Load(ref bool is_new_create)
    {
      T data;
      if (!File.Exists(SerializeDataConst.Save_File_Path_cs))
      {
        data = new T();
        is_new_create = true;
      }
      else
      {
        var conentBytes = StdioUtil.ReadFile(SerializeDataConst.Save_File_Path_cs);
        //conentBytes = CompressUtil.GZipDecompress(conentBytes);--解压缩
        var content = Encoding.UTF8.GetString(conentBytes);
        data = JsonSerializer.Deserialize(content) as T;
      }

      data.AddDataList();
      return data;
    }
  }
}