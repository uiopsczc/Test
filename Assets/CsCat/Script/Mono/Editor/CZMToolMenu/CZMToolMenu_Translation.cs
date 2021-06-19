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
    public static void TranslateToLanguage(string language)
    {
      GameData.instance.translationData.language = language;
      GameData.instance.Save();
      ApplyToUIPrefabs();
      AssetDatabase.Refresh();
      AssetDatabase.SaveAssets();
    }

    [MenuItem(CZMToolConst.MenuRoot + "多语言/切换到默认")]
    public static void TranslateToDefault()
    {
      TranslateToLanguage(null);
    }

    [MenuItem(CZMToolConst.MenuRoot + "多语言/切换到英语")]
    public static void TranslateToEnglish()
    {
      TranslateToLanguage("english");
      
    }

    [MenuItem(CZMToolConst.MenuRoot + "多语言/导出所有TanslationId")]
    public static void ExportTranslationIds()
    {
      var file_path = ExportUIText();
      Process.Start(FilePathConst.ProjectPath + "py_tools/translation/" + "Translation.bat");
      Debug.Log(string.Format("多语言/导出所有TanslationId 完成\n{0}", file_path));
    }

    public static string ExportUIText()
    {
      var file_path = FilePathConst.ProjectPath + "py_tools/translation/excel/ui_string.xlsx";
      var check_path = "Assets/Resources/"; //检测的路径
      var all_assetPaths = AssetDatabase.GetAllAssetPaths();

      var all_string_list = new List<string>();
      foreach (var path in all_assetPaths)
      {
        if (!path.StartsWith(check_path)) continue;
        if (!path.EndsWith(".prefab")) continue;
        var obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        var uiTranslations = obj.GetComponentsInChildren<UITranslation>(true);
        foreach (var uiTranslation in uiTranslations)
        {
          var translation_id = uiTranslation.translation_id;
          if (IsAllExcludeChars(translation_id))
            continue;
          all_string_list.Add(translation_id);
        }
      }

      all_string_list = all_string_list.Unique(); //去重
      StdioUtil.RemoveFiles(file_path);
      WriteToExcel(file_path, all_string_list);
      LogCat.log("UI上的Text已经输出到", file_path);
      return file_path;
    }

    private static bool IsAllExcludeChars(string s)
    {
      if (s.IsNullOrWhiteSpace())
        return true;
      s = s.Trim();
      for (var i = 0; i < s.Length - 1; i++)
        if (!TranslationConst.char_exclude_dict.ContainsKey(s[i]))
          return false;
      return true;
    }

    private static void WriteToExcel(string file_path, List<string> data_list)
    {
      using (var fileStream = new FileStream(file_path, FileMode.Create, FileAccess.Write))
      {
        IWorkbook workbook = new XSSFWorkbook();
        var sheet = workbook.CreateSheet("Sheet1");
        for (var i = 0; i < data_list.Count; i++)
        {
          var data = data_list[i];
          var row = sheet.CreateRow(i);
          var cell = row.CreateCell(0);
          cell.SetCellValue(data);
        }

        workbook.Write(fileStream);
      }
    }

    public static void ApplyToUIPrefabs()
    {
      var check_path = "Assets/Resources/"; //检测的路径
      var all_assetPaths = AssetDatabase.GetAllAssetPaths();
      foreach (var path in all_assetPaths)
      {
        if (!path.StartsWith(check_path)) continue;
        if (!path.EndsWith(".prefab")) continue;
        var obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        var uiTranslations = obj.GetComponentsInChildren<UITranslation>(true);
        foreach (var uiTranslation in uiTranslations)
        {
          uiTranslation.Refresh();
        }
      }
    }
  }
}