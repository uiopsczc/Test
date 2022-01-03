using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu
	{
		public static void ToLanguage(string language)
		{
			GameData.instance.langData.language = language;
			GameData.instance.Save();
			ApplyToUIPrefabs();
			AssetDatabase.Refresh();
			AssetDatabase.SaveAssets();
			Lang.isInited = false;
		}

		[MenuItem(CZMToolConst.Menu_Root + "多语言/切换到默认")]
		public static void ToLangDefault()
		{
			ToLanguage(null);
		}

		[MenuItem(CZMToolConst.Menu_Root + "多语言/切换到默认", true)]
		public static bool CanToLangDefault()
		{
			return GameData.instance.langData.language != null;
		}

		[MenuItem(CZMToolConst.Menu_Root + "多语言/切换到英语")]
		public static void ToLangEnglish()
		{
			ToLanguage("english");
		}

		[MenuItem(CZMToolConst.Menu_Root + "多语言/切换到英语", true)]
		public static bool CanToLangEnglish()
		{
			return !"english".Equals(GameData.instance.langData.language);
		}

		[MenuItem(CZMToolConst.Menu_Root + "多语言/导出所有LangId")]
		public static void ExportLangIds()
		{
			var filePath = ExportUIText();
			Process.Start(FilePathConst.ProjectPath + "py_tools/lang/" + "Lang.bat");
			Debug.Log(string.Format("多语言/导出所有LnagId 完成\n{0}", filePath));
		}

		public static string ExportUIText()
		{
			var filePath = FilePathConst.ProjectPath + "py_tools/lang/excel/ui_string.xlsx";
			var checkPath = "Assets/Resources/"; //检测的路径
			var allAssetPaths = AssetDatabase.GetAllAssetPaths();

			var allStringList = new List<string>();
			foreach (var path in allAssetPaths)
			{
				if (!path.StartsWith(checkPath)) continue;
				if (!path.EndsWith(".prefab")) continue;
				var obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
				var uiLangs = obj.GetComponentsInChildren<UILang>(true);
				foreach (var uiLang in uiLangs)
				{
					var langId = uiLang.langId;
					if (IsAllExcludeChars(langId))
						continue;
					allStringList.Add(langId);
				}
			}

			allStringList.Unique(); //去重
			StdioUtil.RemoveFiles(filePath);
			WriteToExcel(filePath, allStringList);
			LogCat.log("UI上的Text已经输出到", filePath);
			return filePath;
		}

		private static bool IsAllExcludeChars(string s)
		{
			if (s.IsNullOrWhiteSpace())
				return true;
			s = s.Trim();
			for (var i = 0; i < s.Length - 1; i++)
				if (!LangConst.CharExcludeDict.ContainsKey(s[i]))
					return false;
			return true;
		}

		private static void WriteToExcel(string filePath, List<string> dataList)
		{
			using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				IWorkbook workbook = new XSSFWorkbook();
				var sheet = workbook.CreateSheet("Sheet1");
				for (var i = 0; i < dataList.Count; i++)
				{
					var data = dataList[i];
					var row = sheet.CreateRow(i);
					var cell = row.CreateCell(0);
					cell.SetCellValue(data);
				}

				workbook.Write(fileStream);
			}
		}

		public static void ApplyToUIPrefabs()
		{
			var checkPath = "Assets/Resources/"; //检测的路径
			var allAssetPaths = AssetDatabase.GetAllAssetPaths();
			foreach (var path in allAssetPaths)
			{
				if (!path.StartsWith(checkPath)) continue;
				if (!path.EndsWith(".prefab")) continue;
				var obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
				var uiLangs = obj.GetComponentsInChildren<UILang>(true);
				foreach (var uiLang in uiLangs)
				{
					uiLang.RefreshUIText();
				}
			}
		}
	}
}