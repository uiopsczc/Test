using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CsCat
{
	public class AssetPathRefManager : ISingleton
	{
		private readonly Dictionary<string, AssetPathRef> _dict = new Dictionary<string, AssetPathRef>(); //key是guid
		private long _refId;


		public static AssetPathRefManager instance => SingletonFactory.instance.Get<AssetPathRefManager>();


		public void SingleInit()
		{
			if (!Application.isEditor) return;
			if (EditorModeConst.IsEditorMode)
				LoadFromPath(AssetPathRefConst.SaveFilePath.WithRootPath(FilePathConst.ProjectPath));
		}

		public void ClearAll()
		{
			_dict.Clear();
			_refId = 0;
			StdioUtil.RemoveFile(new FileInfo(AssetPathRefConst.SaveFilePath.WithRootPath(FilePathConst.ProjectPath)));
		}

		public void LoadFromPath(string path)
		{
			if (File.Exists(path))
				Load(StdioUtil.ReadTextFile(path));
		}

		public void Load(string contentJson)
		{
			_dict.Clear();
			Hashtable jsonDict = MiniJson.JsonDecode(contentJson) as Hashtable;
			_refId = jsonDict.Get<long>("ref_id");
			ArrayList assetPathRefList = jsonDict.Get<ArrayList>("assetPathRef_list");
			for (var i = 0; i < assetPathRefList.Count; i++)
			{
				var assetPathRefDict = (Hashtable) assetPathRefList[i];
				long refId = assetPathRefDict.Get<long>("ref_id");
				string assetPath = assetPathRefDict.Get<string>("assetPath");
				string guid = assetPathRefDict.Get<string>("guid");
				if (refId > this._refId)
					this._refId = refId;
				_dict[guid] = new AssetPathRef(refId, assetPath, guid);
			}
		}

		public void Save()
		{
			Refresh();
			Hashtable jsonDict = new Hashtable();
			jsonDict["ref_id"] = _refId;
			ArrayList assetPathRefList = new ArrayList();
			foreach (var keyValue in _dict)
			{
				var assetPathRef = keyValue.Value;
				Hashtable assetPathRefDict = new Hashtable();
				assetPathRefDict["ref_id"] = assetPathRef.refId;
				assetPathRefDict["assetPath"] = assetPathRef.assetPath;
				assetPathRefDict["guid"] = assetPathRef.guid;
				assetPathRefList.Add(assetPathRefDict);
			}

			jsonDict["assetPathRef_list"] = assetPathRefList;
			string contentJson = MiniJson.JsonEncode(jsonDict);
			StdioUtil.WriteTextFile(AssetPathRefConst.SaveFilePath.WithRootPath(FilePathConst.ProjectPath), contentJson);
		}

		public void Refresh()
		{
			_dict.RemoveByFunc<string, AssetPathRef>((key, value) => !value.Refresh());
		}

		public void Add(string guid)
		{
			if (_dict.ContainsKey(guid))
				_dict[guid].Refresh();
			else
			{
				_refId++;
				_dict[guid] = new AssetPathRef(_refId, null, guid);
			}
		}

		//////////////////////////////////////////////////////////////////////////
		public bool ContainsGuid(string guid)
		{
			return _dict.ContainsKey(guid);
		}

		public string GetAssetPathByGuid(string guid)
		{
			return _dict[guid].assetPath;
		}

		public long GetRefIdByGuid(string guid)
		{
			return _dict[guid].refId;
		}

		/////////////////////////////////////////////////////////////////////////
		public string GetAssetPathByRefId(long refId)
		{
			foreach (var assetPathRef in _dict.Values)
			{
				if (assetPathRef.refId == refId)
					return assetPathRef.assetPath;
			}

			LogCat.error(string.Format("没有找到ref_id:{0}对应的assetPath", refId));
			return null;
		}
	}
}