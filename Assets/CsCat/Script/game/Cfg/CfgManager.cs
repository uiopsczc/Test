using System.IO;
using UnityEngine;

namespace CsCat
{
  public class CfgManager :TickObject
  {
    private int total_count;
    private int loaded_count;
    public override void Init()
    {
      base.Init();
      resLoadComponent.GetOrLoadAsset(CfgConst.CfgFilePathes, OnLoadedCfgFilePathes);
    }

    void OnLoadedCfgFilePathes(AssetCat assetCat)
    {
      string file_contnet = assetCat.Get<TextAsset>().text;
      string[] file_pathes = file_contnet.Split('\n');
      loaded_count = 0;
      total_count = file_pathes.Length;
      foreach (var file_path in file_pathes)
        resLoadComponent.GetOrLoadAsset(file_path, OnLoadedCfgFile);
    }

    void OnLoadedCfgFile(AssetCat assetCat)
    {
      string class_name = "CsCat.Cfg"+ Path.GetFileNameWithoutExtension(assetCat.asset_path).UpperFirstLetter();
      string json_contnet = assetCat.Get<TextAsset>().text;
      TypeUtil.GetType(class_name).GetPropertyValue("Instance").InvokeMethod("Parse",false, json_contnet);
      loaded_count++;
    }

    public bool IsLoadFinished => loaded_count >= total_count;
  }
}