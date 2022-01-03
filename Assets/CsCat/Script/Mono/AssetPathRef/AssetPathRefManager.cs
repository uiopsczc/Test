using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CsCat
{
	public class AssetPathRefManager : ISingleton
	{
		private Dictionary<string, AssetPathRef> dict = new Dictionary<string, AssetPathRef>(); //key是guid
		private long ref_id;


		public static AssetPathRefManager instance => SingletonFactory.instance.Get<AssetPathRefManager>();


		public void SingleInit()
		{
			if (!Application.isEditor) return;
			if (EditorModeConst.IsEditorMode)
				LoadFromPath(AssetPathRefConst.SaveFilePath.WithRootPath(FilePathConst.ProjectPath));
		}

		public void ClearAll()
		{
			dict.Clear();
			ref_id = 0;
			StdioUtil.RemoveFile(new FileInfo(AssetPathRefConst.SaveFilePath.WithRootPath(FilePathConst.ProjectPath)));
		}

		public void LoadFromPath(string path)
		{
			if (File.Exists(path))
				Load(StdioUtil.ReadTextFile(path));
		}

		public void Load(string content_json)
		{
			dict.Clear();
			Hashtable json_dict = MiniJson.JsonDecode(content_json) as Hashtable;
			ref_id = json_dict.Get<long>("ref_id");
			ArrayList assetPathRef_list = json_dict.Get<ArrayList>("assetPathRef_list");
			foreach (Hashtable assetPathRef_dict in assetPathRef_list)
			{
				long ref_id = assetPathRef_dict.Get<long>("ref_id");
				string assetPath = assetPathRef_dict.Get<string>("assetPath");
				string guid = assetPathRef_dict.Get<string>("guid");
				if (ref_id > this.ref_id)
					this.ref_id = ref_id;
				dict[guid] = new AssetPathRef(ref_id, assetPath, guid);
			}
		}

		public void Save()
		{
			Refresh();
			Hashtable json_dict = new Hashtable();
			json_dict["ref_id"] = ref_id;
			ArrayList assetPathRef_list = new ArrayList();
			foreach (AssetPathRef assetPathRef in dict.Values)
			{
				Hashtable assetPathRef_dict = new Hashtable();
				assetPathRef_dict["ref_id"] = assetPathRef.ref_id;
				assetPathRef_dict["assetPath"] = assetPathRef.asset_path;
				assetPathRef_dict["guid"] = assetPathRef.guid;
				assetPathRef_list.Add(assetPathRef_dict);
			}

			json_dict["assetPathRef_list"] = assetPathRef_list;
			string content_json = MiniJson.JsonEncode(json_dict);
			StdioUtil.WriteTextFile(AssetPathRefConst.SaveFilePath.WithRootPath(FilePathConst.ProjectPath), content_json);
		}

		public void Refresh()
		{
			dict.RemoveByFunc<string, AssetPathRef>((key, value) => !value.Refresh());
		}

		public void Add(string guid)
		{
			if (dict.ContainsKey(guid))
				dict[guid].Refresh();
			else
			{
				ref_id++;
				dict[guid] = new AssetPathRef(ref_id, null, guid);
			}
		}

		//////////////////////////////////////////////////////////////////////////
		public bool ContainsGuid(string guid)
		{
			return dict.ContainsKey(guid);
		}

		public string GetAssetPathByGuid(string guid)
		{
			return dict[guid].asset_path;
		}

		public long GetRefIdByGuid(string guid)
		{
			return dict[guid].ref_id;
		}

		/////////////////////////////////////////////////////////////////////////
		public string GetAssetPathByRefId(long ref_id)
		{
			foreach (var assetPathRef in dict.Values)
			{
				if (assetPathRef.ref_id == ref_id)
					return assetPathRef.asset_path;
			}

			LogCat.error(string.Format("没有找到ref_id:{0}对应的assetPath", ref_id));
			return null;
		}
	}
}