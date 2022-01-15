using System.IO;
using UnityEngine;

namespace CsCat
{
	public class CfgManager : TickObject
	{
		private int totalCount;
		private int loadedCount;
		public override void Init()
		{
			base.Init();
			resLoadComponent.GetOrLoadAsset(CfgConst.CfgFilePaths, OnLoadedCfgFilePaths);
		}

		void OnLoadedCfgFilePaths(AssetCat assetCat)
		{
			string fileContent = assetCat.Get<TextAsset>().text;
			fileContent = fileContent.Replace("\r", "");
			string[] filePaths = fileContent.Split('\n');
			loadedCount = 0;
			totalCount = filePaths.Length;
			for (var i = 0; i < filePaths.Length; i++)
			{
				var filePath = filePaths[i];
				resLoadComponent.GetOrLoadAsset(filePath, OnLoadedCfgFile);
			}
		}

		void OnLoadedCfgFile(AssetCat assetCat)
		{
			string className = "CsCat.Cfg" + Path.GetFileNameWithoutExtension(assetCat.assetPath).UpperFirstLetter();
			string jsonContent = assetCat.Get<TextAsset>().text;
			TypeUtil.GetType(className).GetPropertyValue("Instance").InvokeMethod("Parse", false, jsonContent);
			loadedCount++;
		}

		public bool IsLoadFinished => loadedCount >= totalCount;
	}
}