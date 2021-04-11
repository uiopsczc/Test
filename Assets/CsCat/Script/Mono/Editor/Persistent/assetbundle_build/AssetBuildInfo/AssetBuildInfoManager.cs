using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using Object = UnityEngine.Object;

namespace CsCat
{
  /// <summary>
  /// 自动分析依赖关联，并设置最优依赖的输出assetBunldeName
  /// </summary>
  public class AssetBuildInfoManager : ISingleton
  {
    public static AssetBuildInfoManager Instance
    {
      get { return SingletonFactory.instance.Get<AssetBuildInfoManager>(); }
    }

    private Dictionary<int, AssetBuildInfo> _object2buildInfo = new Dictionary<int, AssetBuildInfo>();
    private Dictionary<string, AssetBuildInfo> _assetPath2buildInfo = new Dictionary<string, AssetBuildInfo>();

    public void Clear()
    {
      _object2buildInfo.Clear();
      _assetPath2buildInfo.Clear();
    }

    public List<AssetBuildInfo> GetAll()
    {
      return new List<AssetBuildInfo>(_object2buildInfo.Values);
    }


    public AssetBuildInfo Load(FileInfo fileInfo)
    {
      return Load(fileInfo, null);
    }

    public AssetBuildInfo Load(FileInfo fileInfo, Type type)
    {
      AssetBuildInfo assetBuildInfo = null;
      string fullPath = fileInfo.FullName();
      int indexEnd = fullPath.IndexEndOf(FilePathConst.ProjectPath);
      if (indexEnd != -1)
      {
        string assetPath = fullPath.Substring(indexEnd + 1);

        if (_assetPath2buildInfo.ContainsKey(assetPath))
        {
          assetBuildInfo = _assetPath2buildInfo[assetPath];
        }
        else
        {
          Object obj = null;
          if (type == null)
            obj = AssetDatabase.LoadMainAssetAtPath(assetPath);
          else
            obj = AssetDatabase.LoadAssetAtPath(assetPath, type);

          if (obj != null)
          {
            int instanceId = obj.GetInstanceID();

            if (_object2buildInfo.ContainsKey(instanceId))
            {
              assetBuildInfo = _object2buildInfo[instanceId];
            }
            else
            {
              assetBuildInfo = new AssetBuildInfo(obj, fileInfo, assetPath);
              _assetPath2buildInfo[assetPath] = assetBuildInfo;
              _object2buildInfo[instanceId] = assetBuildInfo;
            }
          }
        }
      }

      return assetBuildInfo;

    }


    public static Object[] FilterDependChildren(Object[] dependChildren)
    {
      List<Object> tempList = new List<Object>();
      foreach (Object dependChild in dependChildren)
      {
        //不包含脚本对象
        //不包含LightingDataAsset对象
        if (dependChild is MonoScript || dependChild is LightingDataAsset)
          continue;

        //不包含builtin对象
        string path = dependChild.GetAssetPath();
        if (path.StartsWith("Resources"))
          continue;

        tempList.Add(dependChild);
      }

      return tempList.ToArray();
    }


    public void SetAssetBundlePath()
    {
      var all = GetAll();
      foreach (var assetBuildInfo in all)
        assetBuildInfo.Analyze();
      all = GetAll();
      foreach (var assetBuildInfo in all)
        assetBuildInfo.Merge();
      all = GetAll();
      foreach (var assetBuildInfo in all)
        assetBuildInfo.BeforeExport();
      foreach (var assetBuildInfo in all)
        assetBuildInfo.SetAssetBundlePath();
      AssetDatabase.Refresh();
    }

    public void AddRootTarget(string assetFilePath)
    {
      if (File.Exists(assetFilePath) == false)
        return;
      FileInfo fileInfo = new FileInfo(assetFilePath);
      if (fileInfo.Extension.Contains("meta"))
        return;
      AssetBuildInfo assetBuildInfo = Load(fileInfo);
      assetBuildInfo.exportType = AssetBundleExportType.Root;
    }


    public void AddRootTargets(DirectoryInfo bundleDir, string[] partterns = null,
      SearchOption searchOption = SearchOption.AllDirectories)
    {
      if (partterns == null)
        partterns = new string[] {"*.*"};
      foreach (string parttern in partterns)
      {
        FileInfo[] fileInfos = bundleDir.GetFiles(parttern, searchOption);
        foreach (FileInfo fileInfo in fileInfos)
          AddRootTarget(fileInfo.FullName);
      }
    }


  }
}