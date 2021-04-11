using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class ExcelConverter
  {
    #region 检查excel是不是有效的路径

    private static bool CheckExcelPathIsValid(string path)
    {
      return true;
    }

    #endregion

    #region 将excel转为asset

    /// <summary>
    ///   先将输入文件备份，然后再对该备份文件转为asset，处理完成或出错后删除该备份文件
    /// </summary>
    /// <param name="inputPath">输入路径</param>
    /// <param name="outputBase">输出路径</param>
    public static void ConvertExcelToAsset(string inputPath, string outputBase)
    {
      if (inputPath.Contains("__bak__")) //是否是备份文件
        return;
      var fileInfo = new FileInfo(inputPath); //文件是否存在
      if (!fileInfo.Exists)
      {
        Debug.LogErrorFormat("file:{0} not exist!" + inputPath);
        return;
      }

      var inputFullFileNameWithoutSuffix = fileInfo.FullNameWithoutSuffix(); //输入文件的名称【没有后缀】
      var bakInputFilePath =
        string.Format("{0}.__bak__{1}", inputFullFileNameWithoutSuffix, fileInfo.Suffix()); //对输入的文件进行备份
      try
      {
        fileInfo.CopyTo(bakInputFilePath, true); //对输入的文件进行备份
        //using (var bakFileStream = new FileStream(bakInputFilePath, FileMode.Open, FileAccess.Read))
        using (var bakFileStream = new FileStream(bakInputFilePath, FileMode.Open))
        {
          IWorkbook workbook = null;
          if (".xls".Equals(fileInfo.Extension))
          {
            workbook = new HSSFWorkbook(bakFileStream);
          }
          else
          {
            if (".xlsx".Equals(fileInfo.Extension))
            {
              workbook = new XSSFWorkbook(bakFileStream);
            }
          }

          if (workbook != null)
          {
            //ISheet sheetAt = workbook.GetSheetAt(i);
            var sheetAt = workbook.GetSheetAt(0);
            var sheetName = sheetAt.SheetName;
            //                        if (sheetName.Contains("Sheet"))//sheetName包含Sheet的忽略,因为excels新建的表默认是以sheet开头的
            //                            continue;
            //                        if (sheetName.Contains("!"))//sheetName包含!的忽略
            //                            continue;
            string dir = inputFullFileNameWithoutSuffix.Replace("\\", "/").DirPath();
            string name = fileInfo.NameWithoutSuffix();
            name = name.Substring(name.IndexOf("-") + 1);
            outputBase = dir + name;
            outputBase =
              outputBase.Replace(FilePathConst.ExcelsPath,
                FilePathConst.ExcelAssetsPath); //输出到outputBase的子路径要与原来的子路径一致
            outputBase = outputBase.Replace(FilePathConst.ProjectPath, "");
            WriteToAsset(sheetAt, fileInfo.Name, string.Format("{0}.asset", outputBase));
          }
        }
      }
      catch (Exception e)
      {
        Debug.LogException(e);
      }
      finally
      {
        if (File.Exists(bakInputFilePath)) //删除备份文件
          File.Delete(bakInputFilePath);
      }
    }

    /// <summary>
    ///   检查表是否按要求填写
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="dataStartRowNum"></param>
    /// <returns></returns>
    public static bool CheckSheetIsValid(ISheet sheet, string fileName, int dataStartRowNum)
    {
      var headerRow = ExcelConst.Header_Name_Row_Index;
      if (sheet.LastRowNum < dataStartRowNum || sheet.GetRow(headerRow) == null)
      {
        Debug.LogErrorFormat("表格: [{0}]必须大于等于{1}行", fileName, dataStartRowNum);
        return false;
      }

      var headRow = sheet.GetRow(headerRow); //表头
      var hasIdColumn = false;
      for (var i = 0; i < headRow.LastCellNum; i++)
      {
        var cell = headRow.GetCell(i);
        var cellValue = cell == null ? "" : ExcelUtil.GetCellValue(cell);
        if (cellValue.ToLower().Equals(ExcelConst.Id_Text))
        {
          hasIdColumn = true;
          break;
        }

//      if (string.IsNullOrEmpty(cellValue))
//      {
//        Debug.LogErrorFormat("表头字段名字不能为空，所在列：{0}/{1}", i + 1, fileName);
//        return false;
//      }
      }

      if (!hasIdColumn)
      {
        Debug.LogErrorFormat("表格: [{0}]第{1}行没有包含id字段", fileName, dataStartRowNum);
        return false;
      }

      return true;
    }

    private static void WriteToAsset(ISheet sheet, string fileName, string outputPath)
    {
      if (!CheckSheetIsValid(sheet, fileName, ExcelConst.Start_Data_Row_Index)) return;
      try
      {
        var rowEnumerator = sheet.GetRowEnumerator();
        var sheetColLen = ExcelUtil.GetSheetColLen(sheet);
        var headerNames = ExcelUtil.GetSheetHeaderNames(sheet, ExcelConst.Header_Name_Row_Index);
//      if (headerNames.Count > sheetColLen)
//      {
//        Debug.LogErrorFormat("表格: [{0}] 名字那行的数据长度不对,names.Count:{1},   colLength:{2}", sheet.SheetName,
//          headerNames.Count, sheetColLen);
//      }
//      else
        {
          var sheetTypes = ExcelUtil.GetSheetHeaderTypes(sheet, ExcelConst.Header_Name_Type_Row_Index);
//        if (sheetTypes.Count > sheetColLen)
//        {
//          Debug.LogErrorFormat("表格: [{0}] 类型那行的数据长度不对", sheet.SheetName);
//        }
//        else
          {
            var excelDatabase = AssetDatabase.LoadAssetAtPath<ExcelDatabase>(outputPath);
            if (excelDatabase == null)
              excelDatabase = ScriptableObjectUtil.CreateAsset<ExcelDatabase>(outputPath);

            var linkedDictionary = new LinkedDictionary<string, ExcelRow>();
            excelDatabase.header_list.Clear();
            var headers = excelDatabase.header_list;
            int cur_ignore_count = 0;
            for (var i = 0; i < sheetColLen; i++)
            {
              //            LogCat.LogError(headerNames[i] + "  " + sheetTypes[i]);
              if (!ExcelUtil.IsColumnValid(sheet, i))
              {
                cur_ignore_count++;
                continue;
              }
              else
                headers.Add(new ExcelHeader
                {
                  name = headerNames[i - cur_ignore_count],
                  type = sheetTypes[i - cur_ignore_count]
                });
            }

            List<string> to_remove_list = new List<string>(); //多语言表处理
            //预先处理不符合的cell类型
            while (rowEnumerator.MoveNext())
            {
              var row = rowEnumerator.Current as IRow;

              if (row.RowNum >= ExcelConst.Start_Data_Row_Index)
              {
                if (row.LastCellNum == -1)
                  continue;
                bool has_translation = false; //多语言表处理
                var excelValueList = new ExcelRow();
                string id = "";
                cur_ignore_count = 0;
                for (var j = 0; j < sheetColLen; j++)
                {
                  if (!ExcelUtil.IsColumnValid(sheet, j))
                  {
                    cur_ignore_count++;
                    continue;
                  }

                  var errorTips = string.Format("表格: [{0}] 第{1}行，第{2}列数据格式不对!", row.RowNum + 1, sheet.SheetName, j + 1);
                  var cell = j < (int) row.LastCellNum ? row.GetCell(j) : null;
                  var sCellValue = cell == null ? "" : ExcelUtil.GetCellValue(cell);
                  var excelHeader = headers[j - cur_ignore_count];
                  var cellType = excelHeader.type;
                  var intValue = 0;
                  switch (cellType)
                  {
                    case ExcelDataType.INT:
                      if (!string.IsNullOrEmpty(sCellValue) && !int.TryParse(sCellValue, out intValue))
                        Debug.LogErrorFormat("表格: [{0}] 第{1}行，第{2}列数据格式不对!", row.RowNum + 1, sheet.SheetName, j + 1);
                      break;
                    case ExcelDataType.FLOAT:
                      var floatValue = 0f;
                      if (!string.IsNullOrEmpty(sCellValue) && !float.TryParse(sCellValue, out floatValue))
                        Debug.LogWarning(errorTips);
                      break;
                    case ExcelDataType.VECTOR3:
                      var sVector3 = sCellValue.Split(',');
                      if (sVector3 == null || sVector3.Length != 3) Debug.LogWarning(errorTips);
                      foreach (var v in sVector3)
                      {
                        var num5 = 0f;
                        if (!float.TryParse(v, out num5)) Debug.LogWarning(errorTips);
                      }

                      break;
                    case ExcelDataType.BOOLEAN:
                      var boolValue = false;
                      if (!string.IsNullOrEmpty(sCellValue) && !bool.TryParse(sCellValue, out boolValue))
                        Debug.LogWarning(errorTips);
                      break;
                  }

                  if (excelHeader.name.ToLower().Equals(ExcelConst.Id_Text))
                    id = sCellValue;
                  else
                  {
                    //多语言表处理
                    if (fileName.Contains("D 多语言表-Translation.xlsx"))
                    {
                      if (!sCellValue.IsNullOrWhiteSpace())
                      {
                        has_translation = true;
                      }
                    }
                  }

                  var excelValue = new ExcelValue();
                  excelValue.value = sCellValue;
                  excelValueList.value_list.Add(excelValue);
                }

                //多语言表处理,对没有翻译的key不用写asset中，以减少数据
                if (fileName.Contains("D 多语言表-Translation.xlsx"))
                {
                  if (has_translation == false)
                    to_remove_list.Add(id);
                }

                if (excelValueList.value_list.Count > 0)
                {
                  linkedDictionary[id] = excelValueList;
                }
              }
            }

            foreach (var to_remove_id in to_remove_list)
              linkedDictionary.Remove(to_remove_id);
            excelDatabase.SetAssetData(linkedDictionary);
            EditorUtility.SetDirty(excelDatabase);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.LogFormat("WriteTable: {0} count:{1} to {2}", fileName, linkedDictionary.Count, outputPath);
          }
        }
      }
      catch (Exception e)
      {
        Debug.LogException(e);
      }
    }

    #endregion
  }
}