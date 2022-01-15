using System;
using System.Collections.Generic;

namespace CsCat
{
	public class BuildUtil
	{
		public static bool CheckResVersionIsNew(string clientResVersion, string serverResVersion)
		{
			if (clientResVersion == null)
				return true;
			var clientVerList = clientResVersion.Split('.');
			var serverVerList = serverResVersion.Split('.');

			if (clientResVersion.Length >= 3 && serverVerList.Length >= 3)
			{
				var clientV0 = int.Parse(clientVerList[0]);
				var clientV1 = int.Parse(clientVerList[1]);
				var clientV2 = int.Parse(clientVerList[2]);
				var serverV0 = int.Parse(serverVerList[0]);
				var serverV1 = int.Parse(serverVerList[1]);
				var serverV2 = int.Parse(serverVerList[2]);

				if (clientV0 < serverV0)
					return true;
				if (clientV0 > serverV0)
					return false;

				if (clientV1 < serverV1)
					return true;
				if (clientV1 > serverV1)
					return false;

				if (clientV2 < serverV2)
					return true;
				if (clientV2 >= serverV2)
					return false;
			}

			return false;
		}


		public static List<string> GetManifestDiffAssetBundleList(Manifest clientManifest, Manifest serverManifest)
		{
			var differentAssetBundleList = new List<string>();

			if (clientManifest.assetBundleManifest == null)
			{
				differentAssetBundleList.AddRange(serverManifest.GetAllAssetBundlePaths());
				return differentAssetBundleList;
			}

			var serverAssetBundleNames = serverManifest.GetAllAssetBundlePaths();
			var clientAssetBundleNames = clientManifest.GetAllAssetBundlePaths();
			for (var i = 0; i < serverAssetBundleNames.Length; i++)
			{
				var serverAssetBundleName = serverAssetBundleNames[i];
				var index = Array.FindIndex(clientAssetBundleNames,
					element => element.Equals(serverAssetBundleName));
				if (index == -1)
					differentAssetBundleList.Add(serverAssetBundleName);
				else if (!clientManifest.GetAssetBundleHash(clientAssetBundleNames[index])
					.Equals(serverManifest.GetAssetBundleHash(serverAssetBundleName)))
					differentAssetBundleList.Add(serverAssetBundleName);
			}

			return differentAssetBundleList;
		}
	}
}