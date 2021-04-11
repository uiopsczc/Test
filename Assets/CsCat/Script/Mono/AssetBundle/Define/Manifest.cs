using UnityEngine;

namespace CsCat
{
  public class Manifest
  {
    private readonly string[] empty_string_array = { };
    private byte[] manifest_bytes;

    public Manifest()
    {
      assetBundle_name = BuildConst.ManifestBundle_Path;
    }

    public AssetBundleManifest assetBundleManifest { get; private set; }

    public string assetBundle_name { get; protected set; }

    public int assetBundle_count => assetBundleManifest == null ? 0 : assetBundleManifest.GetAllAssetBundles().Length;


    public void LoadFromAssetbundle(AssetBundle assetBundle)
    {
      assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>(BuildConst.Manifest_Path);
    }

    public void SaveBytes(byte[] bytes)
    {
      manifest_bytes = bytes;
    }

    public void SaveToDisk()
    {
      var path = assetBundle_name.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
      StdioUtil.WriteFile(path, manifest_bytes);
    }

    public Hash128 GetAssetBundleHash(string assetBundle_name)
    {
      return assetBundleManifest == null ? default : assetBundleManifest.GetAssetBundleHash(assetBundle_name);
    }

    public string[] GetAllAssetBundlePaths()
    {
      return assetBundleManifest == null ? empty_string_array : assetBundleManifest.GetAllAssetBundles();
    }

    public string[] GetAllAssetBundlePathsWithVariant()
    {
      return assetBundleManifest == null ? empty_string_array : assetBundleManifest.GetAllAssetBundlesWithVariant();
    }

    public string[] GetAllDependencies(string assetBundle_name)
    {
      return assetBundleManifest == null
        ? empty_string_array
        : assetBundleManifest.GetAllDependencies(assetBundle_name);
    }

    public string[] GetDirectDependencies(string assetBundle_name)
    {
      return assetBundleManifest == null
        ? empty_string_array
        : assetBundleManifest.GetDirectDependencies(assetBundle_name);
    }
  }
}