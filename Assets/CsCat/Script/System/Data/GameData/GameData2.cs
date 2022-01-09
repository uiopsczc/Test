using System.Collections;
using System.IO;
using System.Text;

namespace CsCat
{
	public class GameData2 : ISingleton
	{
		public static GameData2 instance => SingletonFactory.instance.Get<GameData2>();

		private string filePath = SerializeDataConst.SaveFilePathCS2;
		public Hashtable data;

		public void SingleInit()
		{
			var fileInfo = new FileInfo(filePath);
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

			var conentBytes = StdioUtil.ReadFile(filePath);
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
			string userId = GameData2.instance.data["user_id"] as string;
			var dictUser = GameData2.instance.data["dict_user"] as Hashtable;
			var dictUserTmp = GameData2.instance.data["dict_user_tmp"] as Hashtable;
			User user = Client.instance.userFactory.NewDoer(userId) as User;
			user.DoRestore(dictUser, dictUserTmp);
			if (user.mainRole == null)
				user.mainRole = user.AddRole("1");
			Client.instance.mainRole = user.mainRole;
			return user;
		}


		private void SaveUser()
		{
			User user = Client.instance.user;
			Hashtable dictUser = new Hashtable();
			Hashtable dictUserTmp = new Hashtable();
			user.DoSave(dictUser, dictUserTmp);
			string userId = user.GetId();


			Hashtable saveData = new Hashtable();
			saveData["dict_user"] = dictUser;
			saveData["dict_user_tmp"] = dictUserTmp;
			saveData["user_id"] = userId;

			var content = MiniJson.JsonEncode(saveData);
			var contentBytes = Encoding.UTF8.GetBytes(content);
			//contentBytes = CompressUtil.GZipCompress(contentBytes);//Ñ¹Ëõ
			StdioUtil.WriteFile(filePath, contentBytes);
		}
	}
}