using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

namespace CsCat
{
  /// <summary>
  /// 修改自 https://github.com/tangzx/ABSystem  的打包处理AssetTarget.cs
  /// </summary>
  public class AssetBuildInfo : IComparable<AssetBuildInfo>
  {
    /// <summary>
    /// 目标Object
    /// </summary>
    public Object asset;

    /// <summary>
    /// 文件路径
    /// </summary>
    public FileInfo fileInfo;

    /// <summary>
    /// 相对于Assets文件夹的目录
    /// </summary>
    public string assetPath;

    /// <summary>
    /// 导出类型
    /// </summary>
    public AssetBundleExportType exportType = AssetBundleExportType.Asset;

    private bool beforeExportProcess;

    private bool _isAnalyzed;

    public string assetBundle_path;

    ///看AssetBuildInfo处理流程说明图（项目中搜索AssetBuildInfo处理流程说明图）
    /// <summary>
    /// 本节点的存在"直接"依赖(子节点)_dependChildrenSet中的元素
    /// </summary>
    private HashSet<AssetBuildInfo> _dependChildrenSet = new HashSet<AssetBuildInfo>();

    /// <summary>
    /// _dependParentSet(父节点)中元素的存在"直接"需要依赖本节点
    /// </summary>
    private HashSet<AssetBuildInfo> _dependParentSet = new HashSet<AssetBuildInfo>();

    public AssetBuildInfo(Object obj, FileInfo fileInfo, string assetPath)
    {
      this.asset = obj;
      this.fileInfo = fileInfo;
      this.assetPath = assetPath;
      this.assetBundle_path = assetPath;
      int index = assetPath.LastIndexOf(".");
      if (index != -1)
        this.assetBundle_path = assetPath.Substring(0, index);
    }

    public int CompareTo(AssetBuildInfo other)
    {
      return other._dependParentSet.Count.CompareTo(_dependParentSet.Count);
    }

    /// <summary>
    /// 分析引用关系
    /// </summary>
    public void Analyze()
    {
      if (_isAnalyzed)
        return;
      _isAnalyzed = true;

      Object[] dependChildren = EditorUtility.CollectDependencies(new Object[] {asset});
      dependChildren = AssetBuildInfoManager.FilterDependChildren(dependChildren);
      var dependChildrenPaths = from dep in dependChildren
        let obj = dep.GetAssetPath()
        select obj;
      dependChildrenPaths = dependChildrenPaths.Distinct().ToArray();


      //由于先后顺序，最终需要进行Merge合并节点，具体看AssetBuildInfo处理流程说明图
      foreach (var dependChildPath in dependChildrenPaths)
      {
        if (File.Exists(dependChildPath) == false)
          continue;
        FileInfo dependChildFileInfo = new FileInfo(dependChildPath);
        AssetBuildInfo dependChildAssetBuildInfo = AssetBuildInfoManager.Instance.Load(dependChildFileInfo);
        if (dependChildAssetBuildInfo == null)
          continue;

        this.AddDependChild(dependChildAssetBuildInfo); //这部是关键

        dependChildAssetBuildInfo.Analyze();
      }
    }




    private void AddDependChild(AssetBuildInfo child)
    {
      if (child == this || this.ContainsDependChild(child))
        return;
      //双向关联
      _dependChildrenSet.Add(child);
      child._dependParentSet.Add(this);
      //我依赖了这个项，那么依赖我的项不需要"直接"依赖这个项了
      this.ClearDependChildOfThisDependParent(child);
    }

    /// <summary>
    /// 本节点的子节点（子子孙孙节点）中是否包含该target项
    /// </summary>
    /// <param name="target"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    private bool ContainsDependChild(AssetBuildInfo target)
    {
      if (_dependChildrenSet.Contains(target))
        return true;
      //recursive
      foreach (var _dependChild in _dependChildrenSet)
      {
        if (_dependChild.ContainsDependChild(target))
          return true;
      }

      return false;
    }



    /// <summary>
    /// 我依赖了这个项，那么依赖我的项不需要"直接"依赖这个项了
    /// </summary>
    private void ClearDependChildOfThisDependParent(AssetBuildInfo target)
    {
      foreach (var _dependParent in _dependParentSet) //依赖我的项不需要"直接"依赖这个项了
      {
        _dependParent.RemoveDependChild(target);
      }
    }

    /// <summary>
    /// 移除直接孩子节点中的target项（该节点的所有父节点的直接孩子节点中删除target项）
    /// </summary>
    /// <param name="target"></param>
    private void RemoveDependChild(AssetBuildInfo target)
    {
      //解除双向关联
      _dependChildrenSet.Remove(target);
      target._dependParentSet.Remove(this);

      //recursive
      var _dependParentList = new HashSet<AssetBuildInfo>(_dependParentSet);
      foreach (AssetBuildInfo _dependParent in _dependParentList
      ) //我依赖了这个项，那么依赖我的项不需要"直接"依赖这个项了(ClearDependChildOfThisDependParent)
      {
        _dependParent.RemoveDependChild(target);
      }
    }

    private void RemoveAllDependParent()
    {
      var dependParentList = new List<AssetBuildInfo>(_dependParentSet);
      _dependParentSet.Clear();
      foreach (var dependParent in dependParentList)
      {
        dependParent._dependChildrenSet.Remove(this);
      }
    }


    public void Merge()
    {
      if (_dependParentSet.Count > 1) //父节点大于1的节点可能出现冗余，所以要进行合并
      {
        var dependParentList = new List<AssetBuildInfo>(_dependParentSet);
        this.RemoveAllDependParent();
        foreach (var dependParent in dependParentList)
        {
          dependParent.AddDependChild(this);
        }
      }
    }




    /// <summary>
    /// 在导出之前执行
    /// </summary>
    public void BeforeExport()
    {
      if (beforeExportProcess) return;
      beforeExportProcess = true;

      //从最上层开始
      foreach (var dependParent in _dependParentSet)
        dependParent.BeforeExport();


      HashSet<AssetBuildInfo> rootSet = new HashSet<AssetBuildInfo>();
      this.GetRoot(rootSet); //获取与本节点有关联的根节点们，即最终需要被输出的节点
      if (rootSet.Count > 1) //大于1个
        this.exportType = AssetBundleExportType.Standalone; //设置为Standalone，输出该节点
    }

    //获取与本节点有关联的根节点们，即最终需要被输出的节点
    private void GetRoot(HashSet<AssetBuildInfo> rootSet)
    {
      switch (this.exportType)
      {
        case AssetBundleExportType.Standalone:
        case AssetBundleExportType.Root:
          rootSet.Add(this);
          break;
        default:
          foreach (AssetBuildInfo dependParent in _dependParentSet) //搜索父节点
            dependParent.GetRoot(rootSet);
          break;
      }
    }

    public void SetAssetBundlePath()
    {
      if (IsNeedExportAlone())
      {
        var importer = AssetImporter.GetAtPath(assetPath);
        importer.assetBundleName = assetBundle_path;
        importer.assetBundleVariant = BuildConst.AssetBundle_Suffix.Substring(1);
        importer.SaveAndReimport();
      }
    }

    bool IsNeedExportAlone()
    {
      if (this.exportType == AssetBundleExportType.Root || this.exportType == AssetBundleExportType.Standalone)
        return true;
      return false;
    }
  }
}
