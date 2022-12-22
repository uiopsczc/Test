using System;
using System.Collections.Generic;

namespace CsCat
{
	public class AssetBundleMap
	{
		public Dictionary<string, long> dict = new Dictionary<string, long>();
		private string _fileContent;

		public void SaveToDisk()
		{
			var path = BuildConst.AssetBundleMap_File_Name.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
			StdioUtil.WriteTextFile(path, _fileContent);
		}

		public long GetAssetBundleBytes(string assetBundleName)
		{
			return this.dict[assetBundleName];
		}


		public void Initialize(string content)
		{
			if (content.IsNullOrWhiteSpace())
			{
				LogCat.LogError("AssetBundleMap empty!!");
				return;
			}

			_fileContent = content;
			content = content.Replace("\r\n", "\n");
			var mapList = content.Split('\n');
			for (var i = 0; i < mapList.Length; i++)
			{
				var map = mapList[i];
				if (map.IsNullOrWhiteSpace())
					continue;

				var splits = map.Split(new[] {StringConst.String_Comma}, StringSplitOptions.None);
				if (splits.Length < 2)
				{
					LogCat.LogError("splitArr length < 2 : " + map);
					continue;
				}

				string assetBundleName = splits[0];
				long bytes = splits[1].To<long>();
				dict[assetBundleName] = bytes;
			}
		}

	}
}