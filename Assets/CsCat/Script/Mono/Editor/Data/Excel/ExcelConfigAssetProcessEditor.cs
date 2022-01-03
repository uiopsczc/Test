using UnityEditor;

namespace CsCat
{
	/// <summary>
	/// Excel导入到unity中的处理
	/// </summary>
	public class ExcelConfigAssetProcessEditor : AssetPostprocessor
	{
		/// <summary>
		/// 只对在FilePathConst.ExcelsPath的Excel处理，且不会对临时打开的文件处理，只有保存的时候才会处理
		/// 输出到FilePathConst.ConfigExcelAssetsPath中
		/// </summary>
		/// <param name="importedAssets"></param>
		/// <param name="deletedAssets"></param>
		/// <param name="movedAssets"></param>
		/// <param name="movedFromAssetPaths"></param>
		static void OnPostprocessAllAssets(
			string[] importedAssets,
			string[] deletedAssets,
			string[] movedAssets,
			string[] movedFromAssetPaths)
		{
			foreach (string path in importedAssets)
			{
				if (!path.Contains("~$") && (path.EndsWith(".xls") || path.EndsWith(".xlsx")))
				{
					//string name = str.Substring(str.LastIndexOf("/")+1, str.LastIndexOf(".")- str.LastIndexOf("/")-1);
					//              LogCat.LogWarning(FilePathConst.ExcelsPath);
					//              LogCat.LogWarning(path + path.IndexOf(FilePathConst.ExcelsPath));
					string absolutePath = FilePathConst.ProjectPath + path;
					//              LogCat.LogWarning(absolutePath);
					//              LogCat.LogWarning(FilePathConst.ExcelsPath);
					//              LogCat.LogWarning(absolutePath.IndexOf(FilePathConst.ExcelsPath));
					if (absolutePath.IndexOf(FilePathConst.ExcelsPath) != -1)
						ExcelConverter.ConvertExcelToAsset(absolutePath, FilePathConst.ExcelAssetsPath);
				}
			}
		}
	}
}