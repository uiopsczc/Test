using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEngine;

namespace CsCat
{
  public class ExcelUtil
  {
    #region GetSheetColLen

    public static int GetSheetColLen(ISheet sheet)
    {
      var num = 0;
      var lastRowNum = sheet.LastRowNum;
      for (var i = 0; i <= lastRowNum; i++)
      {
        var row = sheet.GetRow(i);
        if (row != null) num = Math.Max(num, row.LastCellNum);
      }

      return num;
    }

    #endregion

    public static ISheet GetSheet(string filePath, string targetSheetName)
    {
      var fileInfo = new FileInfo(filePath);
      if (!fileInfo.Exists)
      {
        Debug.LogErrorFormat("file: {0} not exist!", filePath);
        return null;
      }

      IWorkbook workbook = null;
      using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
      {
        if (".xls".Equals(fileInfo.Extension))
        {
          workbook = new HSSFWorkbook(fileStream);
        }
        else
        {
          if (".xlsx".Equals(fileInfo.Extension)) workbook = new XSSFWorkbook(fileStream);
        }

        fileStream.Close();
      }

      if (workbook != null)
        for (var i = 0; i < workbook.NumberOfSheets; i++)
        {
          var sheetAt = workbook.GetSheetAt(i);
          var sheetName = sheetAt.SheetName;
          if (sheetName.Equals(targetSheetName))
            return sheetAt;
        }

      return null;
    }


    #region GetCellValue

    public static string GetCellValue(ICell cell)
    {
      switch (cell.CellType)
      {
        case CellType.Numeric:
          return cell.NumericCellValue.ToString();
        case CellType.String:
          return cell.StringCellValue;
        case CellType.Formula:
          return GetFormulaCellValue(cell);
        case CellType.Boolean:
          return cell.BooleanCellValue.ToString();
      }

      return string.Empty;
    }

    public static string GetFormulaCellValue(ICell cell)
    {
      switch (cell.CachedFormulaResultType)
      {
        case CellType.Numeric:
          return cell.NumericCellValue.ToString();
        case CellType.String:
          return cell.StringCellValue;
        case CellType.Formula:
          return cell.StringCellValue;
        case CellType.Boolean:
          return cell.BooleanCellValue.ToString();
      }

      return string.Empty;
    }

    #endregion

    #region  GetSheetHeaders

    /// <summary>
    ///   获取表头
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="HeaderNames"></param>
    /// <param name="headerRowNum"></param>
    /// <returns></returns>
    public static List<string> GetSheetHeaderNames(ISheet sheet, int headerRowNum)
    {
      var headerNames = new List<string>();
      var heardRow = sheet.GetRow(headerRowNum);
      for (var i = 0; i < heardRow.LastCellNum; i++)
      {
        var cell = heardRow.GetCell(i);
        if (cell == null || GetCellValue(cell).Trim().IsNullOrWhiteSpace())
          continue;
        var cellValue = GetCellValue(cell);
        headerNames.Add(cellValue.Replace(" ", "_"));
      }

      return headerNames;
    }

    public static bool IsColumnValid(ISheet sheet, int column_index)
    {
      var heardRow = sheet.GetRow(ExcelConst.Header_Name_Row_Index);
      var cell = heardRow.GetCell(column_index);
      if (cell == null || GetCellValue(cell).Trim().IsNullOrWhiteSpace())
        return false;
      return true;
    }

    public static List<ExcelDataType> GetSheetHeaderTypes(ISheet sheet, int headerTypeRowNum)
    {
      var list = new List<ExcelDataType>();
      var row = sheet.GetRow(headerTypeRowNum);
      for (var i = 0; i < (int) row.LastCellNum; i++)
      {
        var cell = row.GetCell(i);
        if (cell == null || GetCellValue(cell).Trim().IsNullOrWhiteSpace())
        {
          continue;
        }

        var item = ExcelDatabaseUtil.String2DataType(GetCellValue(cell).Trim());
        list.Add(item);
      }

      return list;
    }

    #endregion
  }
}