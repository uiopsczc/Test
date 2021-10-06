using System.Collections;
using System.IO;
using System.Text;

namespace CsCat
{
  public class GameData2 : ISingleton
  {
    public static GameData2 instance
    {
      get { return SingletonFactory.instance.Get<GameData2>(); }
    }

    private string file_path = SerializeDataConst.Save_File_Path_cs2;
    public Hashtable data;

    public void SingleInit()
    {
      var fileInfo = new FileInfo(file_path);
      if (!fileInfo.Exists)
      {
        var org_data = new Hashtable()
        {
          {"user_id", "user1"},
          {"dict_user_tmp", new Hashtable()},
          {"dict_user", new Hashtable()}
        };
        data = org_data;
        return;
      }

      var conentBytes = StdioUtil.ReadFile(file_path);
      //conentBytes = CompressUtil.GZipDecompress(conentBytes);--½âÑ¹Ëõ
      var content = Encoding.UTF8.GetString(conentBytes);
      data = MiniJson.JsonDecode(content) as Hashtable;
    }

    public void Save()
    {
      SaveUser();
    }

    public User RestoreUser()
    {
      string user_id = GameData2.instance.data["user_id"] as string;
      var dict_user = GameData2.instance.data["dict_user"] as Hashtable;
      var dict_user_tmp = GameData2.instance.data["dict_user_tmp"] as Hashtable;
      User user = Client.instance.userFactory.NewDoer(user_id) as User;
      user.DoRestore(dict_user, dict_user_tmp);
      if (user.main_role == null)
        user.main_role = user.AddRole("1");
      Client.instance.main_role = user.main_role;
      return user;
    }



    private void SaveUser()
    {
      User user = Client.instance.user;
      Hashtable dict_user = new Hashtable();
      Hashtable dict_user_tmp = new Hashtable();
      user.DoSave(dict_user, dict_user_tmp);
      string user_id = user.GetId();


      Hashtable save_data = new Hashtable();
      save_data["dict_user"] = dict_user;
      save_data["dict_user_tmp"] = dict_user_tmp;
      save_data["user_id"] = user_id;

      var content = MiniJson.JsonEncode(save_data);
      var contentBytes = Encoding.UTF8.GetBytes(content);
      //contentBytes = CompressUtil.GZipCompress(contentBytes);//Ñ¹Ëõ
      StdioUtil.WriteFile(file_path, contentBytes);
    }

  }
}