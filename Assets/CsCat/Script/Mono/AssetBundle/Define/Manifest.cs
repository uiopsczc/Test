using UnityEngine;

namespace CsCat
{
	public class Manifest
	{
		private readonly string[] _emptyStringArray = { };
		private byte[] _manifestBytes;

		public Manifest()
		{
			assetBundleName = BuildConst.ManifestBundle_Path;
		}

		public AssetBundleManifest assetBundleManifest { get; private set; }

		public string assetBundleName { get; protected set; }

		public int assetBundleCount =>
			assetBundleManifest == null ? 0 : assetBundleManifest.GetAllAssetBundles().Length;


		public void LoadFromAssetBundle(AssetBundle assetBundle)
		{
			assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>(BuildConst.Manifest_Path);
		}

		public void SaveBytes(byte[] bytes)
		{
			_manifestBytes = bytes;
		}

		public void SaveToDisk()
		{
			var path = assetBundleName.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
			StdioUtil.WriteFile(path, _manifestBytes);
		}

		public Hash128 GetAssetBundleHash(string assetBundleName)
		{
			return assetBundleManifest == null ? default : assetBundleManifest.GetAssetBundleHash(assetBundleName);
		}

		public string[] GetAllAssetBundlePaths()
		{
			return assetBundleManifest == null ? _emptyStringArray : assetBundleManifest.GetAllAssetBundles();
		}

		public string[] GetAllAssetBundlePathsWithVariant()
		{
			return assetBundleManifest == null
				? _emptyStringArray
				: assetBundleManifest.GetAllAssetBundlesWithVariant();
		}

		public string[] GetAllDependencies(string assetBundleName)
		{
			return assetBundleManifest == null
				? _emptyStringArray
				: assetBundleManifest.GetAllDependencies(assetBundleName);
		}

		public string[] GetDirectDependencies(string assetBundleName)
		{
			return assetBundleManifest == null
				? _emptyStringArray
				: assetBundleManifest.GetDirectDependencies(assetBundleName);
		}
	}
}